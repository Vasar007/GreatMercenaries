using UnityEngine;
using GM.GameElements;

namespace GM.GameStates
{
    // Implement state which descrbes mouse hold with card action.
    [CreateAssetMenu(menuName = "Actions/Mouse Hold With Card")]
    public class MouseHoldWithCard : Action
    {
        public CardVariable currentCard;
        public State playerControlState;
        public State playerBlockState;
        public SO.GameEvent onPlayerControlState;
        public Phase blockPhase;

        public override void Execute(float deltaTime)
        {
            bool mouseIsDown = Input.GetMouseButton(0);

            if (!mouseIsDown)
            {
                var results = Settings.GetUIObjects();

                var gameManager = Settings.gameManager;
                if (gameManager.turns[gameManager.turnIndex].currentPhase.value != blockPhase)
                {
                    foreach (var result in results)
                    {
                        var areaLogic = result.gameObject.GetComponentInParent<Area>();
                        if (areaLogic != null)
                        {
                            areaLogic.OnDrop();
                            break;
                        }
                    }

                    currentCard.value.gameObject.SetActive(true);
                    currentCard.Set(null);

                    gameManager.SetState(playerControlState);
                    onPlayerControlState.Raise();
                }
                else
                {
                    foreach (var result in results)
                    {
                        var cardInstance = result.gameObject.GetComponentInParent<CardInstance>();
                        if (cardInstance != null)
                        {
                            int count = 0;
                            if (cardInstance.CanBeBlocked(currentCard.value, ref count))
                            {
                                Settings.SetCardForBlock(currentCard.value.transform,
                                                         cardInstance.transform, count);
                            }
                            break;
                        }
                    }
                    currentCard.value.gameObject.SetActive(true);
                    currentCard.Set(null);

                    gameManager.SetState(playerBlockState);
                    onPlayerControlState.Raise();
                }
                return;
            }
        }
    }
}
