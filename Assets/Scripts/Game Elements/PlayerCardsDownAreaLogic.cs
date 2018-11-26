using UnityEngine;
using GM.GameElements;

namespace GM
{
    // Concrete logic for cards down on area.
    [CreateAssetMenu(menuName = "Areas/PlayerCardsDownAreaLogic")]
    public class PlayerCardsDownAreaLogic : AreaLogic
    {
        public CardVariable cardVariable;
        public CardType creatureType;
        public CardType resourceType;
        public SO.TransformVariable areaGrid;
        public SO.TransformVariable resourceGrid;
        public GameElementLogic cardDownLogic;

        public override void Execute()
        {
            if (cardVariable.value == null) return;

            if (cardVariable.value.cardViz.card.cardType == creatureType)
            {
                // Transfer card from hand to table.
                Settings.SetParentForCard(cardVariable.value.transform, areaGrid.value.transform);

                // Change down card logic.
                ///cardVariable.value.gameObject.SetActive(true); => Second activation.
                cardVariable.value.currentLogic = cardDownLogic;
                // Place card down.
            }
            else if (cardVariable.value.cardViz.card.cardType == resourceType)
            {
                // Transfer card from hand to table.
                Settings.SetParentForCard(cardVariable.value.transform,
                                          resourceGrid.value.transform);

                // Change down card logic.
                ///cardVariable.value.gameObject.SetActive(true); => Second activation.
                cardVariable.value.currentLogic = cardDownLogic;
            }
        }
    }
}
