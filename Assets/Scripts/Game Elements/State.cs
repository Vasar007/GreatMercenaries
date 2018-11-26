using UnityEngine;

namespace GM.GameStates
{
    [CreateAssetMenu(menuName = "State")]
    public class State : ScriptableObject
    {
        public Action[] actions;

        public void Tick(float delatTime)
        {
            foreach (var action in actions)
            {
                action.Execute(delatTime);
            }
        }
    }
}
