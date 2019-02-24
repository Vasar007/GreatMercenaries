using System;
using UnityEngine;

namespace GM
{
    // Class for creating instances of cards with all properties.
    [CreateAssetMenu(menuName = "Card")]
    public class Card : ScriptableObject
    {
        [System.NonSerialized]
        public int instanceId;
        [System.NonSerialized]
        public CardViz cardViz;

        public CardType cardType;
        public int cardCost;
        public CardProperties[] properties;

        public CardProperties GetProperty(Element element)
        {
            return Array.Find(properties, property => property.element == element);
        }
    }
}
