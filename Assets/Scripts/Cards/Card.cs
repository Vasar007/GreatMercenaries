using System;
using UnityEngine;

namespace GM
{
    // Class for creating instances of cards with all properties.
    [CreateAssetMenu(menuName = "Card")]
    public class Card : ScriptableObject
    {
        public CardType cardType;
        public int cardCost;
        public CardProperties[] properties;

        public CardProperties GetProperty(Element element)
        {
            var index = Array.FindIndex(properties, property => property.element == element);
            return index != -1 ? properties[index] : null;
        }
    }
}
