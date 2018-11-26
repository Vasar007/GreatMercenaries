using UnityEngine;

namespace GM.GameStates
{
    // Base class for all actions.
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(float deltaTime);
    }
}
