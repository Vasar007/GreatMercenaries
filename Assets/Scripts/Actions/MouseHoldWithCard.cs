﻿using UnityEngine;
using GM.GameElements;

namespace GM.GameStates
{
    // Implement state which descrbes mouse hold with card action.
    [CreateAssetMenu(menuName = "Actions/MouseHoldWithCard")]
    public class MouseHoldWithCard : Action
    {
        public CardVariable currentCard;
        public State playerCoontrolState;
        public SO.GameEvent onPlayerControlState;

        public override void Execute(float deltaTime)
        {
            bool mouseIsDown = Input.GetMouseButton(0);

            if (!mouseIsDown)
            {
                var results = Settings.GetUIObjects();

                foreach (var result in results)
                {
                    var areaLogic = result.gameObject.GetComponentInParent<Area>();
                    if (areaLogic != null)
                    {
                        areaLogic.OnDrop();
                        break;
                    }
                }

                currentCard.value.gameObject.SetActive(true);
                currentCard.Set(null);

                Settings.gameManager.SetState(playerCoontrolState);
                onPlayerControlState.Raise();
                return;
            }
        }
    }
}