using System.Linq;
using UnityEngine;

namespace GM
{
    // Class-connector between asset cards and game object on scene. Only for Card Vizual purposes.
    public class CardViz : MonoBehaviour
    {
        public Card card;
        public GameObject statsHolder;
        public GameObject resourceHolder;
        public GameObject typeField;
        public CardVizProperties[] properties;

        public void LoadCard(Card card_)
        {
            if (card_ == null) return;

            // Save initial data.
            card_.cardViz = this;
            card = card_;
            card.cardType.OnSetType(this);

            // No need to change visability of properties because flavor and details are not
            // displayed by default.
            ///CloseAll();

            // Process card properties.
            foreach (var cardProperties in card_.properties)
            {
                var prop = GetProperties(cardProperties.element);
                if (prop == null) continue;

                // Do actions related to element type.
                if (cardProperties.element is ElementInt)
                {
                    prop.text.text = cardProperties.intValue.ToString();
                    ///prop.text.gameObject.SetActive(true);
                }
                else if (cardProperties.element is ElementText)
                {
                    prop.text.text = cardProperties.stringValue;
                    ///prop.text.gameObject.SetActive(true);
                }
                else if (cardProperties.element is ElementImage)
                {
                    prop.image.sprite = cardProperties.sprite;
                    ///prop.image.gameObject.SetActive(true);
                }
            }
        }

        public void CloseAll()
        {
            foreach (var property in properties)
            {
                if (property.image != null)
                {
                    property.image.gameObject.SetActive(false);
                }
                if (property.text != null)
                {
                    if (property.text.gameObject != typeField)
                    {
                       property.text.gameObject.SetActive(false);
                    }
                }
            }
        }

        public CardVizProperties GetProperties(Element element)
        {
            // Find first property with equal element and return it.
            return properties.First(property => property.element == element);
        }
    }
}
