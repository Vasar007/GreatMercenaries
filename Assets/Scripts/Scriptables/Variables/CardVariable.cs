using UnityEngine;

namespace GM
{
    // Class for interacting with player and actions.
    [CreateAssetMenu(menuName = "Variables/Card Variable")]
    public class CardVariable : ScriptableObject
    {
        public CardInstance value;

        public void Set(CardInstance cardInstance)
        {
            value = cardInstance;
        }
    }
}
