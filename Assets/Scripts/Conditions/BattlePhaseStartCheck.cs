using System.Linq;
using UnityEngine;

namespace GM
{
    // Implementation of check if player needs in battle phase or not.
    [CreateAssetMenu(menuName = "Actions/Battle Phase Start Check")]
    public class BattlePhaseStartCheck : Condition
    {
        public override bool IsValid()
        {
            GameManager gameManager = GameManager.singleton;
            PlayerHolder playerHolder = gameManager.currentPlayer;

            // Count all not flatfooted cards.
            var count = playerHolder.cardsDown.Count(
                cardInstance => !cardInstance.isFlatfooted
            );

            return count > 0;
        }
    }
}
