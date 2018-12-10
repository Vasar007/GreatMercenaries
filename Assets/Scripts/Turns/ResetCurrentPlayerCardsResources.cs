using UnityEngine;

namespace GM
{
    // Concrete implementation of phase when we reset resources.
    [CreateAssetMenu(menuName = "Turns/Reset Current Player Cards Resources")]
    public class ResetCurrentPlayerCardsResources : Phase
    {
        public override bool IsComplete()
        {
            Settings.gameManager.currentPlayer.MakeAllResourcesCardsUsable();
            return true;
        }
        public override void OnStartPhase()
        {
        }

        public override void OnEndPhase()
        {
        }
    }
}
