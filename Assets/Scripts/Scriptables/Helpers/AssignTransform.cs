using UnityEngine;
using SO;

namespace SA
{
    public class AssignTransform : MonoBehaviour
    {
        public TransformVariable transformVariable;

        private void Awake()
        {
            transformVariable.value = this.transform;
            Destroy(this);
        }

    }
}
