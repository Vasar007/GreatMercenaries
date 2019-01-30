using UnityEngine;

namespace GM
{
    // Concrete implementation of block phase.
    [CreateAssetMenu(menuName = "Turns/Block Phase")]
    public class BlockPhase : Phase
    {
        public PlayerAction changeActivePlayer;
        public GameStates.State playerControlState;

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }

            return false;
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                var gameManager = Settings.gameManager;
                gameManager.SetState(playerControlState);
                gameManager.onPhaseChange.Raise();
                isInit = true;

                if (gameManager.currentPlayer.attackingCards.Count == 0)
                {
                    forceExit = true;
                    return;
                }

                if (gameManager.playerTwoHolder.playerHolder.isHumanPlayer)
                {
                    gameManager.LoadPlayerOnActive(gameManager.playerTwoHolder.playerHolder);
                }
            }
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }
    }
}
