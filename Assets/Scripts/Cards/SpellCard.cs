using UnityEngine;

namespace GM
{
    // Class for managing instances of one of card types.
    [CreateAssetMenu(menuName = "Cards/Spell")]
    public class SpellCard : CardType
    {
        public override void OnSetType(CardViz cardViz)
        {
            base.OnSetType(cardViz);

            // If it is a spell we do not need to show its stats.
            cardViz.statsHolder.SetActive(false);
        }
    }
}
