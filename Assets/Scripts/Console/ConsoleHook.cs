using UnityEngine;

namespace GM
{
    // Class which provide logic for console output events.
    [CreateAssetMenu(menuName = "Console/Hook")]
    public class ConsoleHook : ScriptableObject
    {
        [System.NonSerialized]
        public ConsoleManager consoleManager;

        public void RegisterEvent(string eventName, Color color)
        {
            consoleManager.RegisterEvent(eventName, color);
        }
    }
}