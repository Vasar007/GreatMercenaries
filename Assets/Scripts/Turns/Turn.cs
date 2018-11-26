using UnityEngine;

namespace GM
{
    // Class which keeps all phases and goes through this phases with their logic.
    [CreateAssetMenu(menuName = "Turns/Turn")]
    public class Turn : ScriptableObject
    {
        [System.NonSerialized]
        public int index = 0;
        public PlayerHolder player;
        public PhaseVariable currentPhase;
        public Phase[] phases;

        public bool Execute()
        {
            var result = false;

            currentPhase.value = phases[index];
            phases[index].OnStartPhase();

            var phaseIsComplete = phases[index].IsComplete();

            if (phaseIsComplete)
            {
                phases[index].OnEndPhase();

                ++index;
                if (index > phases.Length - 1)
                {
                    index = 0;
                    result = true;
                }
            }

            return result;
        }
    }
}
