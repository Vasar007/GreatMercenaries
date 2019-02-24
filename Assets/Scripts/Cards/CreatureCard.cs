using UnityEngine;

namespace GM
{
    // Class for managing instances of one of card types.
    [CreateAssetMenu(menuName = "Cards/Creature")]
    public class CreatureCard : CardType
    {
        public override void OnSetType(CardViz cardViz)
        {
            base.OnSetType(cardViz);

            // If it is a creature we need to show its stats.
            cardViz.statsHolder.SetActive(true);
        }
    }
}