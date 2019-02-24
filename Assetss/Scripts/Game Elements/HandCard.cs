using UnityEngine;

namespace GM.GameElements
{
    // Class which contains game logic on actions for hand card.
    [CreateAssetMenu(menuName = "Game Elements/Player Hand Card")]
    public class HandCard : GameElementLogic
    {
        public SO.GameEvent onCurrentCardSelected;
        public CardVariable currentCard;
        public GameStates.State holdingCard;

        public override void OnClick(CardInstance cardInstance)
        {
            // Maybe further need to change this logic because card can interact with enemy's hand.
            if (!Settings.gameManager.currentPlayer.handCards.Contains(cardInstance)) return;

            currentCard.Set(cardInstance);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
        }

        public override void OnHighlight(CardInstance cardInstance)
        {
        }
    }
}
