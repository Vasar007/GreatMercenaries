using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GM
{
    // Class which contains settings and common methods.
    public static class Settings
    {
        public static GameManager gameManager;

        // Need to test on huge amount of data because it would be ineffective.
        private static ResourcesManager _resourcesManager;

        private static ConsoleHook _consoleHook;

        public static ResourcesManager GetResourcesManager()
        {
            // Loads resource manager from assets and return it.
            if (_resourcesManager == null)
            {
                _resourcesManager = Resources.Load("Resources Manager") as ResourcesManager;
                _resourcesManager.Init();
            }

            return _resourcesManager;
        }

        public static List<RaycastResult> GetUIObjects()
        {
            // Gets all data on scene with RaycastTarget property which intersect with mouse.
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static void DropCreatureCard(Transform cardTransform, Transform parent,
                                            CardInstance cardInstance)
        {
            cardInstance.isFlatfooted = true;
            // Execute any special card abilities on drop.

            SetParentForCard(cardTransform, parent);
            cardInstance.SetFlatfooted(true);

            gameManager.currentPlayer.UseResourceCards(cardInstance.cardViz.card.cardCost);
            gameManager.currentPlayer.DropCard(cardInstance);
        }

        public static void SetParentForCard(Transform cardTransform, Transform parent,
                                            Vector3? localPosition = null,
                                            Vector3? eulerAngles = null)
        {
            // Change parent object and correct transform of the card.
            cardTransform.SetParent(parent);
            cardTransform.localPosition = localPosition ?? Vector3.zero;
            cardTransform.localEulerAngles = eulerAngles ?? Vector3.zero;
            cardTransform.localScale = Vector3.one;
        }

        public static void SetCardForBlock(Transform cardTransform, Transform parent, int count)
        {
            const int offset = 150; // Calculated offset from scene.

            var blockPosition = Vector3.zero;
            blockPosition.x += offset * count;
            blockPosition.y -= offset * count;
            SetParentForCard(cardTransform, parent, blockPosition);
        }

        public static void RegisterEvent(string eventName, Color? color = null)
        {
            if (_consoleHook == null)
            {
                _consoleHook = Resources.Load("Console Hook") as ConsoleHook;
            }

            var choosenColor = color ?? Color.white;
            _consoleHook.RegisterEvent(eventName, choosenColor);
        }
    }
}