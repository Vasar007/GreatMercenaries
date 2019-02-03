using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GM
{
    // Class which process network work for the game.
    public class NetworkManager : MonoBehaviour
    {
        private ResourcesManager _resourcesManager;
        private int _cardInstanceIdsCounter;
        private List<MultiplayerHolder> _multiplayerHolders = new List<MultiplayerHolder>();

        public bool isMaster;
        public static NetworkManager singleton;

        private void Awake()
        {
            if (singleton == null)
            {
                _resourcesManager = Resources.Load("Resources Manager") as ResourcesManager;
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region My Calls

        private Card CreateCardForMaster(string cardId)
        {
            var card = _resourcesManager.GetCardInstance(cardId);
            card.instanceId = _cardInstanceIdsCounter;
            ++_cardInstanceIdsCounter;
            return card;
        }

        private Card CreateCardForClient(string cardId, int cardInstanceId)
        {
            var card = _resourcesManager.GetCardInstance(cardId);
            card.instanceId = cardInstanceId;
            return card;
        }

        private void CreateCardClient_Call(string cardId, int cardInstanceId, int photonId)
        {
            var card = CreateCardForClient(cardId, cardInstanceId);
            if (card != null)
            {
                var multiplayerHolder = GetHolder(photonId);
                multiplayerHolder.RegisterCard(card);
            }
        }

        public MultiplayerHolder GetHolder(int photonId)
        {
            return _multiplayerHolders.Find(
                multiplayerHolder => multiplayerHolder.playerOwnerId == photonId
            );
        }

        public Card GetCard(int cardInstanceId, int playerOwnerId)
        {
            var multiplayerHolder = GetHolder(playerOwnerId);
            return multiplayerHolder.GetCard(cardInstanceId);
        }

        // Master only.
        public void PlayerJoined(int photonId, string[] cards)
        {
            var multiplayerHolder = new MultiplayerHolder
            {
                playerOwnerId = photonId
            };
            
            foreach (var cardId in cards)
            {
                var card = CreateCardForMaster(cardId);
                if (card == null) continue;

                multiplayerHolder.RegisterCard(card);
                // RPC to the client.
            }
        }

        #endregion

        #region Photon Callbacks
        #endregion

        #region RPCs
        #endregion
    }
}

