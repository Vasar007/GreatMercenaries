using UnityEngine;

namespace GM
{
    // Additional value-stored class for proccessing work with phases.
    [CreateAssetMenu(menuName = "Variables/Phase")]
    public class PhaseVariable : ScriptableObject
    {
        public Phase value;
    }
}
