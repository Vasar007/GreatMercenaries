using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    // Class which interact with GUI and show player's statistics.
    public class PlayerStatsUI : MonoBehaviour
    {
        public PlayerHolder playerHolder;
        public Image playerPortrait;
        public Text health;
        public Text username;

        public void UpdateUsername()
        {
            username.text = playerHolder.username;
            playerPortrait.sprite = playerHolder.portrait;
        }

        public void UpdateHealth()
        {
            health.text = playerHolder.health.ToString();
        }

        public void UpdateAll()
        {
            UpdateUsername();
            UpdateHealth();
        }
    }
}
