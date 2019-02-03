using System.Collections.Generic;

namespace GM
{
    // Class for tracking multiplayer activities with cards.
    public class MultiplayerHolder
    {
        public int playerOwnerId;
        public Dictionary<int, Card> cards = new Dictionary<int, Card>();

        public void RegisterCard(Card card)
        {
            cards.Add(card.instanceId, card);
        }

        public Card GetCard(int cardInstanceId)
        {
            Card card = null;
            cards.TryGetValue(cardInstanceId, out card);
            return card;
        }
    }
}
