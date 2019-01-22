using UnityEngine;

namespace GM
{
    // Class which implements logic for selecting card to attack and send it on the battle line.
    [CreateAssetMenu(menuName = "Actions/Select Cards To Attack")]
    public class SelectCardsToAttack : GameStates.Action
    {
        public override void Execute(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var results = Settings.GetUIObjects();

                foreach (var result in results)
                {
                    var cardInstance = result.gameObject.GetComponentInParent<CardInstance>();
                    var playerHolder = Settings.gameManager.currentPlayer;

                    if (!playerHolder.cardsDown.Contains(cardInstance)) return;

                    if (cardInstance.CanAttack())
                    {
                        playerHolder.attackingCards.Add(cardInstance);
                        playerHolder.currentHolder.SetCardOnBattleLine(cardInstance);
                    }
                }
            }
        }
    }
}
