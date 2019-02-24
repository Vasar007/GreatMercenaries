using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    // Class which connect logic for console output events and UI.
    public class ConsoleManager : MonoBehaviour
    {
        public Transform consoleGrid;
        public GameObject prefab;
        public ConsoleHook consoleHook;

        private Text[] _textObjects;
        private int _index;

        private void Awake()
        {
            consoleHook.consoleManager = this;
            _textObjects = new Text[5];

            for (int i = 0; i < _textObjects.Length; ++i)
            {
                var gameObject = Instantiate(prefab); // as GameObject
                gameObject.transform.SetParent(consoleGrid);
                gameObject.transform.localScale = Vector3.one;
                _textObjects[i] = gameObject.GetComponent<Text>();
            }
        }

        public void RegisterEvent(string eventName, Color color)
        {
            ++_index;
            if (_index > _textObjects.Length - 1)
            {
                _index = 0;
            }

            _textObjects[_index].text = eventName;
            _textObjects[_index].color = color;
            _textObjects[_index].transform.SetAsLastSibling();
            _textObjects[_index].gameObject.SetActive(true);
        }
    }
}
