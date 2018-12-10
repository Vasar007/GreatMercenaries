﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GM.GameElements;

namespace GM
{
    // Class for tracking actual player's cards in the hand and on the table.
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public string[] startingCards;
        public SO.TransformVariable handGrid;
        public SO.TransformVariable resourcesGrid;
        public SO.TransformVariable downGrid;

        public int resourcesPerTurn = 1;
        [System.NonSerialized]
        public int resourcesDroppedThisTurn;

        public GameElementLogic handLogic;
        public GameElementLogic downLogic;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
        [System.NonSerialized]
        public List<ResourceHolder> resourcesList = new List<ResourceHolder>();

        public int ResourcesCunt
        {
            get { return resourcesGrid.value.GetComponentsInChildren<CardViz>().Length; }
        }

        public void AddResoourceCard(GameObject cardObject)
        {
            var resourceHolder = new ResourceHolder()
            {
                cardObject = cardObject
            };

            resourcesList.Add(resourceHolder);
            ++resourcesDroppedThisTurn;
        }

        public int NonUsedCards()
        {
            // Count all non used resources cards.
            var result = resourcesList.Count(resource => !resource.isUsed);
            return result;
        }

        public bool CanUseCard(Card card)
        {
            var result = false;

            if (card.cardType is CreatureCard || card.cardType is SpellCard)
            {
                var currentResources = NonUsedCards();
                result = card.cardCost <= currentResources;
            }
            else if (card.cardType is ResourceCard)
            {
                if (resourcesPerTurn - resourcesDroppedThisTurn > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public List<ResourceHolder> GetNonUsedResources()
        {
            return resourcesList.Where(resource => !resource.isUsed).ToList();
        }

        public void MakeAllResourcesCardsUsable()
        {
            resourcesList.ForEach(resource =>
            {
                resource.isUsed = false;
                resource.cardObject.transform.localEulerAngles = Vector3.zero;
            });
            resourcesDroppedThisTurn = 0;
        }

        public void UseResourceCards(int amount)
        {
            var euler = new Vector3(0, 0, 90);
            var nonUsedResources = GetNonUsedResources();

            if (amount > nonUsedResources.Count)
            {
                throw new System.InvalidOperationException(
                    "Resource number is lower than card cost."
                );
            }

            for (int i = 0; i < amount; ++i)
            {
                nonUsedResources[i].isUsed = true;
                nonUsedResources[i].cardObject.transform.localEulerAngles = euler;
            }
        }
    }
}
