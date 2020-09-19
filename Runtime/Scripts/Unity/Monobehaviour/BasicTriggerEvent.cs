using UnityEngine;
using UnityEngine.Events;

namespace Basics
{
    [RequireComponent(typeof(Collider))]
    public class BasicTriggerEvent : MonoBehaviour
    {
        public bool triggerOnce;
        public UnityEvent TriggerEnter;
        public UnityEvent TriggerStay;
        public UnityEvent TriggerExit;
        public bool checkForTag;
        public string checkTag;

        private void OnTriggerEnter(Collider other)
        {
            if (checkForTag && other.CompareTag(checkTag))
            {
                TriggerEnter?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (checkForTag && other.CompareTag(checkTag))
            {
                TriggerStay?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (checkForTag && other.CompareTag(checkTag))
            {
                TriggerExit?.Invoke();
                if (triggerOnce)
                    gameObject.SetActive(false);
            }
        }
    }
}