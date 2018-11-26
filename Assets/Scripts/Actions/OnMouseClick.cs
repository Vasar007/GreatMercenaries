using UnityEngine;

namespace GM.GameStates
{
    // Implement mouse click action.
    [CreateAssetMenu(menuName = "Actions/OnMouseClick")]
    public class OnMouseClick : Action
    {
        public override void Execute(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var results = Settings.GetUIObjects();

                foreach (var result in results)
                {
                    var clickable = result.gameObject.GetComponentInParent<IClickable>();
                    if (clickable != null)
                    {
                        clickable.OnClick();
                        break;
                    }
                }
            }
        }
    }
}
