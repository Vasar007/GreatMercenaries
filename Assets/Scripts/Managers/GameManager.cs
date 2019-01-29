using System;
using System.Collections.Generic;
using UnityEngine;
using GM.GameStates;

namespace GM
{
    // Class for control of game states.
    public class GameManager : MonoBehaviour
    {
        [System.NonSerialized]
        public PlayerHolder[] allPlayers;

        public PlayerHolder currentPlayer;
        public CardHolder playerOneHolder;
        public CardHolder playerTwoHolder;

        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChange;
        public SO.GameEvent onPhaseChange;
        public SO.StringVariable turnText;

        public PlayerStatsUI[] statsUIs;

        private Dictionary<CardInstance, BlockInstance> _blockInstances =
            new Dictionary<CardInstance, BlockInstance>();

        public static GameManager singleton;

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
                    allPlayers[i].currentHolder = playerOneHolder;
                }
                else
                {
                    allPlayers[i].currentHolder = playerTwoHolder;
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
            var index = Array.FindIndex(allPlayers, player => player != playerHolder);
            return index != -1 ? allPlayers[index] : null;
        }

        public void LoadPlayerOnHolder(PlayerHolder playerHolder, CardHolder cardHolder,
                                       PlayerStatsUI playerStatsUI)
        {
            cardHolder.LoadPlayer(playerHolder, playerStatsUI);
        }

        public void LoadPlayerOnActive(PlayerHolder playerHolder)
        {
            var previousPlayer = playerOneHolder.playerHolder;
            LoadPlayerOnHolder(previousPlayer, playerTwoHolder, statsUIs[1]);
            LoadPlayerOnHolder(playerHolder, playerOneHolder, statsUIs[0]);
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

        public void AddBlockInstance(CardInstance cardAttacker, CardInstance cardBlocker)
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

            if (blockInstance.cardBlockers.Contains(cardBlocker))
            {
                blockInstance.cardBlockers.Add(cardBlocker);
            }
        }

        public Dictionary<CardInstance, BlockInstance> GetBlockInstances()
        {
            return _blockInstances;
        }

        public void ClearBlockInstances()
        {
            _blockInstances.Clear();
        }
    }
}
