﻿using UnityEngine;

namespace GM
{
    // Context implementator from strategy pattern (or Client). Need to use for actions.
    public class CardInstance : MonoBehaviour, IClickable
    {
        public CardViz cardViz;
        public GameElements.GameElementLogic currentLogic;

        private void Start()
        {
            cardViz = GetComponent<CardViz>();
        }

        public void OnClick()
        {
            if (currentLogic == null) return;

            currentLogic.OnClick(this);
        }

        public void OnHighlight()
        {
            if (currentLogic == null) return;

            currentLogic.OnHighlight(this);
        }
    }
}
