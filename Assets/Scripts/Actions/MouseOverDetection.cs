using UnityEngine;

namespace GM.GameStates
{
    // Implement mouse over detection action.
    [CreateAssetMenu(menuName = "Actions/MouseOverDetection")]
    public class MouseOverDetection : Action
    {
        public override void Execute(float deltaTime)
        {
            var results = Settings.GetUIObjects();

            foreach (var result in results)
            {
                var clickable = result.gameObject.GetComponentInParent<IClickable>();

                if (clickable != null)
                {
                    clickable.OnHighlight();
                    break;
                }
            }
        }
    }
}
