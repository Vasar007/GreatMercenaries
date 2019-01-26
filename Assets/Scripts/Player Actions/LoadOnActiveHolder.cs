using UnityEngine;

namespace GM
{
    // Action which load active player for local multiplayer.
    [CreateAssetMenu(menuName = "Actions/Player Actions/Load On Active Holder")]
    public class LoadOnActiveHolder : PlayerAction
    {
        public override void Execute(PlayerHolder playerHolder)
        {
            GameManager.singleton.LoadPlayerOnActive(playerHolder);
        }
    }
}
