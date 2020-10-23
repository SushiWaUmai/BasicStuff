using UnityEngine;
using UnityEngine.Events;

namespace Basics
{
    [RequireComponent(typeof(Collider2D))]
    public class BasicTriggerEvent2D : MonoBehaviour
    {
        public bool triggerOnce;

        public UnityEvent TriggerEnter;
        public UnityEvent TriggerStay;
        public UnityEvent TriggerExit;

        public bool checkForTag;
        public string checkTag;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (checkForTag && collision.CompareTag(checkTag))
            {
                TriggerEnter?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (checkForTag && collision.CompareTag(checkTag))
            {
                TriggerStay?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (checkForTag && collision.CompareTag(checkTag))
            {
                TriggerExit?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }
    }
}