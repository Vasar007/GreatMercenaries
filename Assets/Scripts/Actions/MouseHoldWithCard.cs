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

        private void ResetActiveCard(State state)
        {
            currentCard.value.gameObject.SetActive(true);
            currentCard.Set(null);

            Settings.gameManager.SetState(state);
            onPlayerControlState.Raise();
        }

        public override void Execute(float deltaTime)
        {
            if (Input.GetMouseButton(0)) return;

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
                ResetActiveCard(playerControlState);
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
                ResetActiveCard(playerBlockState);
            }
        }
    }
}
