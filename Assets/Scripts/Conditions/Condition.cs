using UnityEngine;

namespace GM
{
    // Abstract class which contain method to check some condition.
    public abstract class Condition : ScriptableObject
    {
        public abstract bool IsValid();
    }
}
