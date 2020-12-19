using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics
{
    public class BasicTimeManager : MonoBehaviour
    {
        #region Variables

        private static BasicTimeManager instance;
        public static BasicTimeManager Instance
        {
            get
            {
                if(instance == null)
                {
                    return new GameObject("BasicTimeManager").AddComponent<BasicTimeManager>();
                }
                return instance;
            }
        }

        public bool dontDestroyOnLoad = true;
        public bool stopCorutineOnLoad = false;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
            instance = this;
        }

        private void OnEnable() => SceneManager.sceneLoaded += OnLevelFinishedLoading;
        private void OnDisable() => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        #endregion

        #region Methods

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (stopCorutineOnLoad)
                StopAllCoroutines();
        }

        public Coroutine Wait(float time, Action action) => StartCoroutine(Count(time, action));
        public Coroutine WaitInRealTime(float time, Action action) => StartCoroutine(CountInRealTime(time, action));
        public Coroutine WaitForPhysicsUpdate(Action action) => StartCoroutine(PhysicsUpdateWait(action));
        public Coroutine WaitForEndFrame(Action action) => StartCoroutine(EndFrameWait(action));
        public Coroutine WaitForNextFrame(Action action) => StartCoroutine(SkipFrame(action));

        public Coroutine Wait<T>(float time, T input, Action<T> action) => StartCoroutine(Count(time, input, action));
        public Coroutine WaitInRealTime<T>(float time, T input, Action<T> action) => StartCoroutine(CountInRealTime(time, input, action));
        public Coroutine WaitForPhysicsUpdate<T>(Action<T> action, T input) => StartCoroutine(PhysicsUpdateWait(action, input));
        public Coroutine WaitForEndFrame<T>(Action<T> action, T input) => StartCoroutine(EndFrameWait(action, input));
        public Coroutine WaitForNextFrame<T>(Action<T> action, T input) => StartCoroutine(SkipFrame<T>(action, input));


        private IEnumerator Count(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
        private IEnumerator CountInRealTime(float time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action.Invoke();
        }
        private IEnumerator EndFrameWait(Action action)
        {
            yield return new WaitForEndOfFrame();
            action.Invoke();
        }
        private IEnumerator PhysicsUpdateWait(Action action)
        {
            yield return new WaitForFixedUpdate();
            action.Invoke();
        }
        private IEnumerator SkipFrame(Action action)
        {
            yield return null;
            action.Invoke();
        }

        private IEnumerator Count<T>(float time, T input, Action<T> action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke(input);
        }
        private IEnumerator CountInRealTime<T>(float time, T input, Action<T> action)
        {
            yield return new WaitForSecondsRealtime(time);
            action.Invoke(input);
        }
        private IEnumerator EndFrameWait<T>(Action<T> action, T input)
        {
            yield return new WaitForEndOfFrame();
            action.Invoke(input);
        }
        private IEnumerator PhysicsUpdateWait<T>(Action<T> action, T input)
        {
            yield return new WaitForFixedUpdate();
            action.Invoke(input);
        }
        private IEnumerator SkipFrame<T>(Action<T> action, T input)
        {
            yield return null;
            action.Invoke(input);
        }

        #endregion
    }
}