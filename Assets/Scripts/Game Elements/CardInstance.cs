using UnityEngine;

namespace GM
{
    // Context implementator from strategy pattern (or Client). Need to use for actions.
    public class CardInstance : MonoBehaviour, IClickable
    {
        public PlayerHolder playerOwner;
        public CardViz cardViz;
        public GameElements.GameElementLogic currentLogic;
        public bool isFlatfooted;

        [System.NonSerialized]
        public bool wasUsed; // Need to additional check during block phase.

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

        public bool CanAttack()
        {
            if (isFlatfooted)
            {
                return false;
            }

            if (cardViz.card.cardType.TypeAllowsForAttack(this))
            {
                return true;
            }

            return false;
        }

        public void SetFlatfooted(bool isFlatfooted)
        {
            if (this.isFlatfooted && transform.localEulerAngles == Vector3.zero)
            {
                Debug.LogWarning("Something wrong with flatfooted cards!");
            }

            this.isFlatfooted = isFlatfooted;
            wasUsed = isFlatfooted;
            if (isFlatfooted)
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                transform.localEulerAngles = Vector3.zero;
            }
        }

        public bool CanBeBlocked(CardInstance cardBlocker, ref int count)
        {
            bool result = playerOwner.attackingCards.Contains(this);
            if (result && cardBlocker.CanAttack())
            {
                result = true;

                // We can add additional logic when card is attempted to block.

                if (result)
                { 
                    Settings.gameManager.AddBlockInstance(this, cardBlocker, ref count);
                }
                return result;
            }
            else
            {
                return false;
            }
        }

        public void CardInstanceToGraveyard()
        {
            Settings.gameManager.PutCardToGraveyard(this);
        }
    }
}
