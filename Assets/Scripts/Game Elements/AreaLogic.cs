using UnityEngine;

namespace GM
{
    // Logic (strategy) abstract class for areas.
    public abstract class AreaLogic : ScriptableObject
    {
        public abstract void Execute();
    }
}
