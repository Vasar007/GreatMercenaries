﻿using UnityEngine;

namespace GM
{
    // Concrete implementation of battle phase.
    [CreateAssetMenu(menuName = "Turns/Battle Phase Player")]
    public class BattlePhase : Phase
    {
        public GameStates.State battlePhaseControl;
        public Condition isBattleValid;

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
                forceExit = !isBattleValid.IsValid();

                Settings.gameManager.SetState(forceExit ? null : battlePhaseControl);
                Settings.gameManager.onPhaseChange.Raise();
                isInit = true;
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
