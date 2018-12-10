﻿using UnityEngine;
using GM.GameElements;

namespace GM
{
    // Concrete logic for cards down on area.
    [CreateAssetMenu(menuName = "Areas/Player Cards Down AreaLogic")]
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

            Card card = cardVariable.value.cardViz.card;

            if (card.cardType == creatureType)
            {
                var canUse = Settings.gameManager.currentPlayer.CanUseCard(card);
                if (canUse)
                {
                    // Transfer card from hand to table.
                    Settings.DropCreatureCard(cardVariable.value.transform,
                                              areaGrid.value.transform,
                                              card);
                    cardVariable.value.currentLogic = cardDownLogic;
                }

                ///cardVariable.value.gameObject.SetActive(true); => Second activation.

                // Place card down.
            }
            else if (card.cardType == resourceType)
            {
                var canUse = Settings.gameManager.currentPlayer.CanUseCard(card);
                if (canUse)
                {
                    // Transfer card from hand to table.
                    Settings.SetParentForCard(cardVariable.value.transform,
                                          resourceGrid.value.transform);
                    cardVariable.value.currentLogic = cardDownLogic;
                    Settings.gameManager.currentPlayer.AddResoourceCard(
                        cardVariable.value.gameObject
                    );
                }

                ///cardVariable.value.gameObject.SetActive(true); => Second activation.
            }
        }
    }
}
