using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM
{
    // Class for taking contrlov over the sessions between players.
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager singleton;

        public delegate void OnSceneLoaded();
        public OnSceneLoaded onSceneLoaded;

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator LoadLevel(string level)
        {
            yield return SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
            if (onSceneLoaded != null)
            {
                onSceneLoaded();
                onSceneLoaded = null;
            }
        }

        public void LoadGameLevel()
        {
            StartCoroutine("GameScene");
        }

        public void LoadMenu()
        {
            StartCoroutine("MenuScene");
        }
    }
}
