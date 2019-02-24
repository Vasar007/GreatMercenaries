using UnityEngine;

namespace GM
{
    // Base class for all actions which are related with players.
    public abstract class PlayerAction : ScriptableObject
    {
        public abstract void Execute(PlayerHolder playerHolder);
    }
}
