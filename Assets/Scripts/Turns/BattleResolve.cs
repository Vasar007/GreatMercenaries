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

            var playerHolder = Settings.gameManager.currentPlayer;
            var enemyHolder = Settings.gameManager.GetEnemyOf(playerHolder);

            if (playerHolder.attackingCards.Count == 0) return true;

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
                playerHolder.DropCard(attackingCard, false);
                playerHolder.currentHolder.SetCardDown(attackingCard);
                attackingCard.SetFlatfooted(true);

                enemyHolder.DoDamage(attackProperty.intValue);
            }

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
