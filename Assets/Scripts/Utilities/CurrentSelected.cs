using UnityEngine;

namespace GM
{
    // Class which describes game logic for selected card and processes some events.
    public class CurrentSelected : MonoBehaviour
    {
        public CardVariable currentCard;
        public CardViz cardViz;
        private Transform _transform;

        private void Start()
        {
            _transform = this.transform;
            CloseCard();
        }

        private void Update()
        {
            // Set coordinates related to mouse position but first we need to transform position
            // from screen to world.
            var mousePosition = Input.mousePosition;
            _transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane)
            );
            ///_transform.position = new Vector3(_transform.position.x, _transform.position.y, 0);
        }

        public void LoadCard()
        {
            if (currentCard.value == null) return;

            // Change active card on scene.
            currentCard.value.gameObject.SetActive(false);
            cardViz.LoadCard(currentCard.value.cardViz.card);
            cardViz.gameObject.SetActive(true);
        }

        public void CloseCard()
        {
            cardViz.gameObject.SetActive(false);
            // Activate resource holder if we held resource card (and resource holder is
            // deactivated).
            cardViz.resourceHolder.SetActive(true);
        }
    }
}
