using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    // Class for containing dynamic resources for cards.
    [CreateAssetMenu(menuName = "Managers/Resource Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public Element typeElement;
        public Card[] allCards;

        private Dictionary<string, Card> _cardsDict = new Dictionary<string, Card>();

        private Card GetCard(string id)
        {
            Card result = null;
            _cardsDict.TryGetValue(id, out result);
            return result;
        }

        public void Init()
        {
            _cardsDict.Clear();
            foreach (var card in allCards)
            {
                _cardsDict.Add(card.name, card);
            }
        }

        public Card GetCardInstance(string id)
        {
            var originalCard = GetCard(id);
            if (originalCard == null) return null;

            var newInstance = Instantiate(originalCard);
            newInstance.name = originalCard.name;
            return newInstance;
        }

    }
}