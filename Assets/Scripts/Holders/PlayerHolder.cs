using System.Collections.Generic;
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
        public Sprite portrait;
        public Color playerColor;

        [System.NonSerialized]
        public int health = 20; // 20 - Debug constant. TODO: fix this later.

        public PlayerStatsUI statsUI;

        public string[] startingCards;

        public int resourcesPerTurn = 1;
        [System.NonSerialized]
        public int resourcesDroppedThisTurn;

        public bool isHumanPlayer;

        public GameElementLogic handLogic;
        public GameElementLogic downLogic;

        [System.NonSerialized]
        public CardHolder currentHolder;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> attackingCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<ResourceHolder> resourcesList = new List<ResourceHolder>();

        public int ResourcesCunt
        {
            get
            {
                return currentHolder.resourcesGrid.value.GetComponentsInChildren<CardViz>().Length;
            }
        }

        private void OnEnable()
        {
            health = 20; // 20 - Debug constant. TODO: fix this later.
        }

        public void AddResoourceCard(GameObject cardObject)
        {
            var resourceHolder = new ResourceHolder()
            {
                cardObject = cardObject
            };

            resourcesList.Add(resourceHolder);
            ++resourcesDroppedThisTurn;

            Settings.RegisterEvent(username + " drops resources card.");
        }

        public int NonUsedCards()
        {
            // Count all non used resources cards.
            var result = resourcesList.Count(resource => !resource.isUsed);
            return result;
        }

        public bool CanUseCard(CardInstance cardInstance)
        {
            // TODO: fix tries to play cards on enemies battle without special abilities.
            if (!handCards.Contains(cardInstance) || !isHumanPlayer) return false;

            bool result = false;
            var card = cardInstance.cardViz.card;

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

        public void DropCard(CardInstance cardInstance)
        {
            if (handCards.Contains(cardInstance))
            {
                handCards.Remove(cardInstance);
            }

            cardsDown.Add(cardInstance);

            Settings.RegisterEvent(username + " used " + cardInstance.cardViz.card.name + " for " +
                                   cardInstance.cardViz.card.cardCost + " resources.");
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

        public void DoDamage(int value)
        {
            health -= value;
            if (statsUI != null)
            {
                statsUI.UpdateHealth();
            }
        }

        public void LoadPlayerOnStatsUI()
        {
            if (statsUI != null)
            {
                statsUI.playerHolder = this;
                statsUI.UpdateAll();
            }
        }
    }
}
