using UnityEngine.UI;
using SO.UI;
using GM;

namespace SO
{
    public class UpdateTextFromPhase : UIPropertyUpdater
    {
        public PhaseVariable currentPhase;
        public Text targetText;

        /// <summary>
        /// Use this to update a text UI element based on the target string variable
        /// </summary>
        public override void Raise()
        {
            targetText.text = currentPhase.value.phaseName;
        }
    }
}