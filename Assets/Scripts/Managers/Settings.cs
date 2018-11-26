﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GM
{
    public class Settings
    {
        public static GameManager gameManager;

        // Need to test on huge amount of data because it would be ineffective.
        private static ResourcesManager _resourcesManager;

        public static ResourcesManager GetResourcesManager()
        {
            // Loads resource manager from assets and return it.
            if (_resourcesManager == null)
            {
                _resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
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

        public static void SetParentForCard(Transform cardTransform, Transform parent)
        {
            // Change parent object and correct transform of the card.
            cardTransform.SetParent(parent);
            cardTransform.localPosition = Vector3.zero;
            cardTransform.localEulerAngles = Vector3.zero;
            cardTransform.localScale = Vector3.one;
        }
    }
}