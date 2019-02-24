using UnityEngine;

namespace GM
{
    // Abstract class for handling card types.
    public abstract class CardType : ScriptableObject
    {
        public string typeName;
        public bool canAttack;
        // We can add some additional logic here:
        ///public typelogic logic;

        public virtual void OnSetType(CardViz cardViz)
        {
            // Set text related to card type.
            var typeElement = Settings.GetResourcesManager().typeElement;
            var type = cardViz.GetProperties(typeElement);
            type.text.text = typeName;
        }

        public bool TypeAllowsForAttack(CardInstance cardInstance)
        {
            // e.g. Flying type can attack even if flatfooted:
            ///bool result = logic.Execute(cardInstance) -> if (cardInstance.isFlatfooted);
            ///cardInstance.isFlatfooted = false;
            ///return true;

            if (canAttack) return true;
            return false;
        }
    }
}