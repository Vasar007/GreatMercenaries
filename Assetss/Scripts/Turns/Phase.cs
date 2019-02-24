using UnityEngine;

namespace GM
{
    // Implementation script of phase logic (context or client in Strategy Pattern).
    public abstract class Phase : ScriptableObject
    {
        public string phaseName;
        public bool forceExit;

        [System.NonSerialized]
        protected bool isInit;

        public abstract bool IsComplete();
        public abstract void OnStartPhase();
        public abstract void OnEndPhase();
    }
}
