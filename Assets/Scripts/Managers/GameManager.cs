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

        public bool switchPlayer;
        public PlayerStatsUI[] statsUIs;

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
            CreateStartingCards();

            turnText.value = turns[turnIndex].player.username + "'s Turn";
            onTurnChange.Raise();
        }

        private void Update()
        {
            if (switchPlayer)
            {
                switchPlayer = false;
                playerOneHolder.LoadPlayer(allPlayers[0], statsUIs[0]);
                playerTwoHolder.LoadPlayer(allPlayers[1], statsUIs[1]);
            }

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
                if (allPlayers[i].isHumanPlayer)
                {
                    allPlayers[i].currentHolder = playerOneHolder;
                }
                else
                {
                    allPlayers[i].currentHolder = playerTwoHolder;
                }

                if (i < 2)
                {
                    allPlayers[i].statsUI = statsUIs[i];
                    statsUIs[i].playerHolder.LoadPlayerOnStatsUI();
                }
            }
        }

        private void CreateStartingCards()
        {
            ResourcesManager resourcesManager = Settings.GetResourcesManager();

            foreach (var playerHolder in allPlayers)
            { 
                foreach (var card in playerHolder.startingCards)
                {
                    var gameObject = Instantiate(cardPrefab); // as GameObject

                    var cardViz = gameObject.GetComponent<CardViz>();
                    cardViz.LoadCard(resourcesManager.GetCardInstance(card));

                    var cardInstance = gameObject.GetComponent<CardInstance>();
                    cardInstance.currentLogic = playerHolder.handLogic;

                    Settings.SetParentForCard(gameObject.transform,
                                              playerHolder.currentHolder.handGrid.value);
                    playerHolder.handCards.Add(cardInstance);
                }

                Settings.RegisterEvent("Created cards for player " + playerHolder.username + ".",
                                       playerHolder.playerColor);
            }
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
    }
}
