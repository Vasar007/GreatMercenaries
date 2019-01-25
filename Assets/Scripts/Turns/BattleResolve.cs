using UnityEngine;

namespace GM
{
    // Concrete implementation of battle resolve.
    [CreateAssetMenu(menuName = "Turns/Battle Resolve")]
    public class BattleResolve : Phase
    {
        public Element attackElement;
        public Element healthElement;

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
        }


        public override void OnStartPhase()
        {
            var playerHolder = Settings.gameManager.currentPlayer;
            var enemyHolder = Settings.gameManager.GetEnemyOf(playerHolder);

            if (playerHolder.attackingCards.Count == 0)
            {
                forceExit = true;
                return;
            }

            // If there are more than 2 players we should specify move order in attack.
            foreach (var attackingCard in playerHolder.attackingCards)
            {
                var card = attackingCard.cardViz.card;

                var attackProperty = card.GetProperty(attackElement);
                if (attackProperty == null)
                {
                    Debug.LogError("You are attacking with a card that can't attack!");
                    continue;
                }

                enemyHolder.DoDamage(attackProperty.intValue);
            }

            // Work done, exit the phase.
            forceExit = true;
        }

        public override void OnEndPhase()
        {
            // Changes later.
        }
    }
}
