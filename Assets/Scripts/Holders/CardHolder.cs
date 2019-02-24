using UnityEngine;

namespace GM
{
    // Class for tracking actual cards in the game and store their grid.
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolder : ScriptableObject
    {
        public SO.TransformVariable handGrid;
        public SO.TransformVariable resourcesGrid;
        public SO.TransformVariable downGrid;
        public SO.TransformVariable battleLine;

        [System.NonSerialized]
        public PlayerHolder playerHolder;

        public void LoadPlayer(PlayerHolder playerHolder, PlayerStatsUI playerStatsUI)
        {
            if (playerHolder == null) return;

            this.playerHolder = playerHolder;
            playerHolder.currentHolder = this;

            foreach (var cardInstance in playerHolder.cardsDown)
            {
                if (!playerHolder.attackingCards.Contains(cardInstance))
                {
                    var cardTransform = cardInstance.cardViz.gameObject.transform;
                    Settings.SetParentForCard(cardTransform, downGrid.value.transform,
                                              cardTransform.localPosition,
                                              cardTransform.localEulerAngles);
                }
            }

            foreach (var cardInstance in playerHolder.handCards)
            {
                if (cardInstance.cardViz != null)
                {
                    Settings.SetParentForCard(cardInstance.cardViz.gameObject.transform,
                                              handGrid.value.transform);
                }
            }

            playerHolder.resourcesList.ForEach(resourceHolder =>
                Settings.SetParentForCard(resourceHolder.cardObject.transform,
                                          resourcesGrid.value.transform));

            playerHolder.attackingCards.ForEach(cardInstance => SetCardOnBattleLine(cardInstance));

            playerHolder.statsUI = playerStatsUI;
            playerHolder.LoadPlayerOnStatsUI();
        }

        public void SetCardOnBattleLine(CardInstance cardInstance)
        {
            var position = cardInstance.cardViz.gameObject.transform.position;

            Settings.SetParentForCard(cardInstance.cardViz.gameObject.transform,
                                      battleLine.value.transform);
            position.y = cardInstance.cardViz.gameObject.transform.position.y;
            position.z = cardInstance.cardViz.gameObject.transform.position.z;
            cardInstance.cardViz.gameObject.transform.position = position;
        }

        public void SetCardDown(CardInstance cardInstance)
        {
            Settings.SetParentForCard(cardInstance.cardViz.gameObject.transform,
                                      downGrid.value.transform);
        }
    }
}
