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

        public void LoadPlayer(PlayerHolder playerHolder)
        {
            playerHolder.cardsDown.ForEach(cardInstance =>
                Settings.SetParentForCard(cardInstance.cardViz.gameObject.transform,
                                          downGrid.value.transform));

            playerHolder.handCards.ForEach(cardInstance =>
                Settings.SetParentForCard(cardInstance.cardViz.gameObject.transform,
                                          handGrid.value.transform));

            playerHolder.resourcesList.ForEach(resourceHolder =>
                Settings.SetParentForCard(resourceHolder.cardObject.transform,
                                          resourcesGrid.value.transform));
        }
    }
}
