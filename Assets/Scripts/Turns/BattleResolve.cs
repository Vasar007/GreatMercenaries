using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    // Concrete implementation of battle resolve.
    [CreateAssetMenu(menuName = "Turns/Battle Resolve")]
    public class BattleResolve : Phase
    {
        public Element attackElement;
        public Element healthElement;

        private BlockInstance GetBlockInstanceOfAttacker(CardInstance cardAttacker,
            Dictionary<CardInstance, BlockInstance> blockInstances)
        {
            BlockInstance result = null;
            blockInstances.TryGetValue(cardAttacker, out result);
            return result;
        }

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

            var blockDictionary = Settings.gameManager.GetBlockInstances();

            // If there are more than 2 players we should specify move order in attack.
            foreach (var cardAttacker in playerHolder.attackingCards)
            {
                var card = cardAttacker.cardViz.card;

                var attackProperty = card.GetProperty(attackElement);
                if (attackProperty == null)
                {
                    Debug.LogError("You are attacking with a card that can't attack!");
                    continue;
                }

                var attackValue = attackProperty.intValue;

                var blockInstance = GetBlockInstanceOfAttacker(cardAttacker, blockDictionary);
                if (blockInstance != null)
                {
                    foreach (var cardBlocker in blockInstance.cardBlockers)
                    {
                        var defenceAttackProperty = card.GetProperty(attackElement);
                        if (defenceAttackProperty == null)
                        {
                            Debug.LogWarning("You are trying to block a card with no attack " +
                                             "element!");
                            continue;
                        }

                        attackValue -= defenceAttackProperty.intValue;
                        if (defenceAttackProperty.intValue <= attackValue)
                        {
                            // Make a card duel.
                            cardBlocker.CardInstanceToGraveyard();
                        }
                    }
                }

                if (attackValue <= 0)
                {
                    attackValue = 0;
                    cardAttacker.CardInstanceToGraveyard();
                }

                playerHolder.DropCard(cardAttacker, false);
                playerHolder.currentHolder.SetCardDown(cardAttacker);
                cardAttacker.SetFlatfooted(true);

                enemyHolder.DoDamage(attackValue);
            }

            Settings.gameManager.ClearBlockInstances();
            playerHolder.attackingCards.Clear();

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
