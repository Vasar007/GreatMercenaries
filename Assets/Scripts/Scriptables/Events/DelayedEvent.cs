using System.Collections;
using UnityEngine;

namespace SO
{
    public class DelayedEvent : MonoBehaviour
    {
        public GameEvent targetEvent;

        /// <summary>
        /// Use this to raise an event after some time has passe0d
        /// </summary>
        public void Raise(float timer)
        {
            StartCoroutine(DelayedRaise(timer));
        }

        private IEnumerator DelayedRaise(float t)
        {
            yield return new WaitForSeconds(t);
            targetEvent.Raise();
        }
    }
}
