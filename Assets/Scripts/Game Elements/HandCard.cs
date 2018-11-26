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
            currentCard.Set(cardInstance);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
        }

        public override void OnHighlight(CardInstance cardInstance)
        {
        }
    }
}
