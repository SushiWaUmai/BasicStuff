using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics
{
    public class BasicTimeManager : BasicSingleton<BasicTimeManager>
    {
        #region Variables

        /// <summary>
        /// Defines whether the TimeManager should reset the coroutines on scene change
        /// </summary>
        [Tooltip("Defines whether the TimeManager should reset the coroutines on scene change")]
        public bool stopCoroutinesOnLoad = false;

        #endregion

        #region MonoBehaviour Methods

        private void OnEnable() => SceneManager.sceneLoaded += OnLevelFinishedLoading;
        private void OnDisable() => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        #endregion

        #region Methods

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (stopCoroutinesOnLoad)
                StopAllCoroutines();
        }

        /// <summary>
        /// Waits a certain amount of seconds before calling action
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine Wait(float time, Action action) => StartCoroutine(Count(time, action));

        /// <summary>
        /// Waits a certain amount of real time seconds before calling action
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine WaitInRealTime(float time, Action action) => StartCoroutine(CountInRealTime(time, action));

        /// <summary>
        /// Waits until the next PhysicsUpdate has passed
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine WaitForPhysicsUpdate(Action action) => StartCoroutine(PhysicsUpdateWait(action));

        /// <summary>
        /// Waits until the end of the frame
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine WaitForEndFrame(Action action) => StartCoroutine(EndFrameWait(action));

        /// <summary>
        /// Waits until the next frame
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine WaitForNextFrame(Action action) => StartCoroutine(SkipFrame(action));

        /// <summary>
        /// Waits a certain amount of seconds before calling action
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Coroutine Wait<T>(float time, T input, Action<T> action) => StartCoroutine(Count(time, input, action));

        /// <summary>
        /// Waits a certain amount of real time seconds before calling action
        /// </summary>
        public Coroutine WaitInRealTime<T>(float time, T input, Action<T> action) => StartCoroutine(CountInRealTime(time, input, action));

        /// <summary>
        /// Waits until the next PhysicsUpdate has passed
        /// </summary>
        public Coroutine WaitForPhysicsUpdate<T>(Action<T> action, T input) => StartCoroutine(PhysicsUpdateWait(action, input));

        /// <summary>
        /// Waits until the end of the frame
        /// </summary>
        public Coroutine WaitForEndFrame<T>(Action<T> action, T input) => StartCoroutine(EndFrameWait(action, input));

        /// <summary>
        /// Waits until the next frame
        /// </summary>
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