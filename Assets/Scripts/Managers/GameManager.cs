using System;
using System.Collections.Generic;
using UnityEngine;
using GM.GameStates;

namespace GM
{
    // Class for control of game states.
    public class GameManager : MonoBehaviour
    {
        public static GameManager singleton;

        [System.NonSerialized]
        public PlayerHolder[] allPlayers;

        public PlayerHolder currentPlayer;
        public CardHolder playerOneCardHolder;
        public CardHolder playerTwoCardHolder;

        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChange;
        public SO.GameEvent onPhaseChange;
        public SO.StringVariable turnText;

        public PlayerStatsUI[] statsUIs;

        public SO.TransformVariable graveyardVariable;

        private List<CardInstance> _graveyardCards = new List<CardInstance>();

        private Dictionary<CardInstance, BlockInstance> _blockInstances =
            new Dictionary<CardInstance, BlockInstance>();

        private void Awake()
        {
            singleton = this;

            allPlayers = new PlayerHolder[turns.Length];
            for (int i = 0; i < allPlayers.Length; ++i)
            {
                allPlayers[i] = turns[i].player;
            }
            currentPlayer = turns[0].player;
        }

        private void Start()
        {
            Settings.gameManager = this;

            SetupPlayers();

            turns[0].OnTurnStart();
            turnText.value = turns[turnIndex].player.username + "'s Turn";
            onTurnChange.Raise();
        }

        private void Update()
        {
            bool isComplete = turns[turnIndex].Execute();
            if (isComplete)
            {
                ++turnIndex;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }

                // The current player has changed here.
                currentPlayer = turns[turnIndex].player;
                turns[turnIndex].OnTurnStart();
                turnText.value = turns[turnIndex].player.username + "'s Turn";
                onTurnChange.Raise();
            }

            // Update current state with delta time.
            if (currentState != null)
            {
                currentState.Tick(Time.deltaTime);
            }
        }

        private void SetupPlayers()
        {
            for (int i = 0; i < allPlayers.Length; ++i)
            {
                allPlayers[i].Init();

                // If we have more than 2 players, need to rework this statement.
                if (i == 0)
                {
                    allPlayers[i].currentHolder = playerOneCardHolder;
                }
                else
                {
                    allPlayers[i].currentHolder = playerTwoCardHolder;
                }
                allPlayers[i].statsUI = statsUIs[i];
                allPlayers[i].currentHolder.LoadPlayer(allPlayers[i], allPlayers[i].statsUI);
            }
        }

        private BlockInstance GetBlockInstanceOfAttacker(CardInstance cardAttacker)
        {
            BlockInstance result = null;
            _blockInstances.TryGetValue(cardAttacker, out result);
            return result;
        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void EndCurrentPhase()
        {
            turns[turnIndex].EndCurrentPhase();

            Settings.RegisterEvent(turns[turnIndex].name + " finished.", currentPlayer.playerColor);
        }

        public PlayerHolder GetEnemyOf(PlayerHolder playerHolder)
        {
            return Array.Find(allPlayers, player => player != playerHolder);
        }

        public void LoadPlayerOnHolder(PlayerHolder playerHolder, CardHolder cardHolder,
                                       PlayerStatsUI playerStatsUI)
        {
            cardHolder.LoadPlayer(playerHolder, playerStatsUI);
        }

        public void LoadPlayerOnActive(PlayerHolder playerHolder)
        {
            var previousPlayer = playerOneCardHolder.playerHolder;
            if (previousPlayer != playerHolder)
            {
                LoadPlayerOnHolder(previousPlayer, playerTwoCardHolder, statsUIs[1]);
            }
            LoadPlayerOnHolder(playerHolder, playerOneCardHolder, statsUIs[0]);
        }

        public void PickNewCardFromDeck(PlayerHolder playerHolder)
        {
            if (playerHolder.allCards.Count == 0)
            {
                Debug.Log("Game Over!");
                return;
            }

            var cardId = playerHolder.allCards[0];
            playerHolder.allCards.RemoveAt(0);

            var resourcesManager = Settings.GetResourcesManager();
            var gameObject = Instantiate(cardPrefab); // as GameObject

            var cardViz = gameObject.GetComponent<CardViz>();
            cardViz.LoadCard(resourcesManager.GetCardInstance(cardId));

            var cardInstance = gameObject.GetComponent<CardInstance>();
            cardInstance.playerOwner = playerHolder;
            cardInstance.currentLogic = playerHolder.handLogic;

            Settings.SetParentForCard(gameObject.transform,
                                      playerHolder.currentHolder.handGrid.value);
            playerHolder.handCards.Add(cardInstance);
        }

        public void AddBlockInstance(CardInstance cardAttacker, CardInstance cardBlocker,
                                     ref int count)
        {
            var blockInstance = GetBlockInstanceOfAttacker(cardAttacker);
            if (blockInstance == null)
            {
                blockInstance = new BlockInstance
                {
                    cardAttacker = cardAttacker
                };
                _blockInstances.Add(cardAttacker, blockInstance);
            }

            if (!blockInstance.cardBlockers.Contains(cardBlocker))
            {
                blockInstance.cardBlockers.Add(cardBlocker);
                cardBlocker.wasUsed = true;
            }

            count = blockInstance.cardBlockers.Count;
        }

        public Dictionary<CardInstance, BlockInstance> GetBlockInstances()
        {
            return _blockInstances;
        }

        public void ClearBlockInstances()
        {
            _blockInstances.Clear();
        }

        public void PutCardToGraveyard(CardInstance cardInstance)
        {
            Debug.Log("Called put to graveyard method.");
            cardInstance.playerOwner.CardToGraveyard(cardInstance);

            _graveyardCards.Add(cardInstance);

            const int offset = 5;
            var position = Vector3.zero;
            position.x -= _graveyardCards.Count * offset;
            position.z = _graveyardCards.Count * offset;

            Settings.SetParentForCard(cardInstance.transform, graveyardVariable.value, position);
        }
    }
}
