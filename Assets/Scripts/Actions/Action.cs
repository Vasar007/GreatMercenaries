using UnityEngine;

namespace GM.GameStates
{
    // Base class for all actions which are related with states and time.
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(float deltaTime);
    }
}
