using UnityEngine;

namespace GM
{
    // Action which help with picking card from deck.
    [CreateAssetMenu(menuName = "Actions/Player Actions/Pick Card From Deck")]
    public class PickCardFromDeck : PlayerAction
    {
        public override void Execute(PlayerHolder playerHolder)
        {
            GameManager.singleton.PickNewCardFromDeck(playerHolder);
        }
    }
}
