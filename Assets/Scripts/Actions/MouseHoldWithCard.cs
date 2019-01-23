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
        public SO.GameEvent onPlayerControlState;

        public override void Execute(float deltaTime)
        {
            bool mouseIsDown = Input.GetMouseButton(0);

            if (!mouseIsDown)
            {
                var results = Settings.GetUIObjects();

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

                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }
        }
    }
}
