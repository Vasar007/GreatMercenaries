using System.Collections.Generic;
using UnityEngine;
using GM.GameElements;

namespace GM
{
    // Class for tracking actual player's cards in the hand and on the table.
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public string[] startingCards;
        public SO.TransformVariable handGrid;
        public SO.TransformVariable resourcesGrid;
        public SO.TransformVariable downGrid;

        public GameElementLogic handLogic;
        public GameElementLogic downLogic;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
    }
}
