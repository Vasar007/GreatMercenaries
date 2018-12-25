using UnityEngine;
using GM.GameStates;

namespace GM
{
    // Implementation of check if player needs in battle phase or not.
    [CreateAssetMenu(menuName = "Actions/Battle Phase Start Check")]
    public class BattlePhaseStartCheck : Condition
    {
        public override bool IsValid()
        {
            GameManager gameManager = GameManager.singleton;
            if (gameManager.currentPlayer.cardsDown.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
