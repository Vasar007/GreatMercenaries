using UnityEngine;
using GM.GameStates;

namespace GM
{
    // Class for control of game states.
    public class GameManager : MonoBehaviour
    {
        public PlayerHolder currentPlayer;
        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChange;
        public SO.GameEvent onPhaseChange;
        public SO.StringVariable turnText;

        private void Start()
        {
            Settings.gameManager = this;
            CreateStartingCards();

            turnText.value = turns[turnIndex].player.username + "'s Turn";
            onTurnChange.Raise();
        }

        private void Update()
        {
            var isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                ++turnIndex;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }

                turnText.value = turns[turnIndex].player.username + "'s Turn";
                onTurnChange.Raise();
            }

            // Update current state with delta time.
            if (currentState != null)
            {
                currentState.Tick(Time.deltaTime);
            }
        }

        private void CreateStartingCards()
        {
            ResourcesManager resourcesManager = Settings.GetResourcesManager();

            foreach (var card in currentPlayer.startingCards)
            {
                var gameObject = Instantiate(cardPrefab); // as GameObject

                var cardViz = gameObject.GetComponent<CardViz>();
                cardViz.LoadCard(resourcesManager.GetCardInstance(card));

                var cardInstance = gameObject.GetComponent<CardInstance>();
                cardInstance.currentLogic = currentPlayer.handLogic;

                Settings.SetParentForCard(gameObject.transform, currentPlayer.handGrid.value);
            }
        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void EndCurrentPhase()
        {
            turns[turnIndex].EndCurrentPhase();
        }
    }
}
