using UnityEngine;

namespace GM
{
    // Action which transform card when turn changes.
    [CreateAssetMenu(menuName = "Actions/Player Actions/Reset Flatfooted Cards")]
    public class ResetFlatfootedCards : PlayerAction
    {
        public override void Execute(PlayerHolder playerHolder)
        {
            foreach (var cardInstance in playerHolder.cardsDown)
            {
                if (cardInstance.isFlatfooted)
                {
                    cardInstance.SetFlatfooted(false);
                }
            }
        }
    }
}
