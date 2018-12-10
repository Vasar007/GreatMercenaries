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
    }
}
