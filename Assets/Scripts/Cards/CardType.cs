using UnityEngine;

namespace GM
{
    // Abstract class for handling card types.
    public abstract class CardType : ScriptableObject
    {
        public string typeName;

        public virtual void OnSetType(CardViz cardViz)
        {
            // Set text related to card type.
            var typeElement = Settings.GetResourcesManager().typeElement;
            var type = cardViz.GetProperties(typeElement);
            type.text.text = typeName;
        }
    }
}