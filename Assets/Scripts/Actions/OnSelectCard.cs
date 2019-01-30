using UnityEngine;

namespace GM.GameStates
{
    // Implement on mouse selected event for block phase.
    [CreateAssetMenu(menuName = "Actions/On Select Card")]
    public class OnSelectCard : Action
    {
        public SO.GameEvent onCurrentCardSelected;
        public CardVariable currentCard;
        public State holdingState;

        public override void Execute(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var results = Settings.GetUIObjects();

                foreach (var result in results)
                {
                    var cardInstance = result.gameObject.GetComponentInParent<CardInstance>();
                    if (cardInstance != null)
                    {
                        var gameManager = Settings.gameManager;
                        var enemyHolder = gameManager.GetEnemyOf(gameManager.currentPlayer);
                        if (cardInstance.playerOwner == enemyHolder)
                        {
                            currentCard.Set(cardInstance);
                            gameManager.SetState(holdingState);
                            onCurrentCardSelected.Raise();
                        }

                        return;
                    }
                }
            }
        }
    }
}
