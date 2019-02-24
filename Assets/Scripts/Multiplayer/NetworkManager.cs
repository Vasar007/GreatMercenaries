using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    // Class which process network work for the game.
    public class NetworkManager : Photon.PunBehaviour
    {
        public static NetworkManager singleton;

        private ResourcesManager _resourcesManager;
        private int _cardInstanceIdsCounter;
        private List<MultiplayerHolder> _multiplayerHolders = new List<MultiplayerHolder>();
        private System.Random _random = new System.Random();

        public SO.StringVariable loggerText;
        public SO.GameEvent onLoggerUpdated;
        public SO.GameEvent onConnected;
        public SO.GameEvent onFailedToConnect;
        public SO.GameEvent onWaitingForPlayer;

        public bool isMaster;

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

        private void Start()
        {
            PhotonNetwork.autoCleanUpPlayerObjects = false;
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = false;
            Init();
        }

        private void UpdateStringValue(string value)
        {
            loggerText.value = value;
            onLoggerUpdated.Raise();
        }

        public void Init()
        {
            PhotonNetwork.ConnectUsingSettings("1");
            UpdateStringValue("Connecting");
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefgolkip";
            return new string(Enumerable.Repeat(chars, length).Select(
                s => s[_random.Next(s.Length)]).ToArray()
            );
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

        private void CreateRoom()
        {
            var room = new RoomOptions
            {
                MaxPlayers = 2
            };
            const int roomNameLength = 256;
            PhotonNetwork.CreateRoom(RandomString(roomNameLength), room, TypedLobby.Default);
        }

        private void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void OnPlayGame()
        {
            JoinRandomRoom();
        }

        #endregion

        #region Photon Callbacks

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            UpdateStringValue("Connected");
            onConnected.Raise();
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            base.OnFailedToConnectToPhoton(cause);
            UpdateStringValue("Failed To Connect");
            onFailedToConnect.Raise();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            base.OnPhotonRandomJoinFailed(codeAndMsg);
            CreateRoom();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            isMaster = true;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            UpdateStringValue("Waiting For Player");
            onWaitingForPlayer.Raise();
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            if (isMaster)
            {
                if (PhotonNetwork.playerList.Length > 1)
                {
                    UpdateStringValue("Ready For Game");
                    onWaitingForPlayer.Raise();

                    PhotonNetwork.room.IsOpen = false;
                    ///SessionManager.singleton.LoadGameLevel();
                }
            }
        }

        public override void OnDisconnectedFromPhoton()
        {
            base.OnDisconnectedFromPhoton();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }

        #endregion

        #region RPCs
        #endregion
    }
}

