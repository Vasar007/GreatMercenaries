using UnityEngine;

namespace GM.GameElements
{
    // Implementation script of area logic or context (client) which use logic.
    // We can use it on areas.
    public class Area : MonoBehaviour
    {
        public AreaLogic areaLogic;

        public void OnDrop()
        {
            areaLogic.Execute();
        }
    }
}
