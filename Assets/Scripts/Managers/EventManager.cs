using UnityEngine;

namespace GM
{
    // Class which controls different events in the actual game.
    public class EventManager : MonoBehaviour
    {
        #region My Calls
        public void CardWasDroppedDown(int cardInstanceId, int playerOwnerId)
        {
            var card = NetworkManager.singleton.GetCard(cardInstanceId, playerOwnerId);
            // Add more logic here.
        }

        public void CardWasPickedUpFromDeck(int cardInstanceId, int playerOwnerId)
        {
            var card = NetworkManager.singleton.GetCard(cardInstanceId, playerOwnerId);
            // Add more logic here.
        }
        #endregion
    }
}
