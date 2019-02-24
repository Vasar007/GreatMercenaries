using UnityEngine;

namespace GM.GameElements
{
    // Abstract class which describes events handled processed by object.
    public abstract class GameElementLogic : ScriptableObject
    {
        public abstract void OnClick(CardInstance cardInstance);

        public abstract void OnHighlight(CardInstance cardInstance);
    }
}
