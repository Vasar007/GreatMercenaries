using UnityEngine;

namespace GM
{
    // Class for managing resources like mana for ability to play cards.
    [CreateAssetMenu(menuName = "Cards/Resource")]
    public class ResourceCard : CardType
    {
        public override void OnSetType(CardViz cardViz)
        {
            base.OnSetType(cardViz);

            // If it is a resource card we don't need to show its stats.
            cardViz.statsHolder.SetActive(false);
            cardViz.resourceHolder.SetActive(false);
        }
    }
}