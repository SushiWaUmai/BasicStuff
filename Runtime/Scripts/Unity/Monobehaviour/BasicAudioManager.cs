using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Basics
{
    public class BasicAudioManager : MonoBehaviour
    {
        #region Variables

        [SerializeField, Tooltip("All Audio Mixer Groups in the Game")]
        private AudioMixerGroup[] audioMixerGroups;

        /// <summary>
        /// Defines whether the AudioManager should reset the audio on scene change
        /// </summary>
        [Tooltip("Defines whether the AudioManager should reset the audio on scene change")]
        public bool resetAudioOnLoad = true;

        [SerializeField, Tooltip("Defines whether the AudioManager should be destroyed on scene change")]
        private bool dontDestroyOnLoad = true;

        private static BasicAudioManager instance;

        /// <summary>
        /// Singleton of the AudioManager
        /// </summary>
        public static BasicAudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    return new GameObject("BasicAudioManager").AddComponent<BasicAudioManager>();
                }
                return instance;
            }
        }

        /// <summary>
        /// All AudioSources created by the AudioManager
        /// </summary>
        public AudioSource[] AudioSources
        {
            get
            {
                AudioSource[] result = new AudioSource[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                    result[i] = transform.GetChild(i).GetComponent<AudioSource>();

                return result;
            }
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (instance != null)
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
            if (resetAudioOnLoad)
                StopAllAudio();
        }

        /// <summary>
        /// Plays a sound effect on a certain AudioMixerGroup
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public AudioSource PlaySound(int audioMixerIndex, AudioClip clip, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(clip, volume);
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            Destroy(source.gameObject, clip.length);
            return source;
        }

        /// <summary>
        /// Plays a sound effect in the default AudioMixerGroup (index 0)
        /// </summary>
        /// <param name="clip"></param>
        /// <returns></returns>
        public AudioSource PlaySound(AudioClip clip)
        {
            return PlaySound(0, clip);
        }

        /// <summary>
        /// Plays a random sound in the default AudioMixerGroup (index 0)
        /// </summary>
        /// <param name="clips"></param>
        /// <returns></returns>
        public AudioSource PlayRandomSound(params AudioClip[] clips)
        {
            return PlayRandomSound(0, clips);
        }

        /// <summary>
        /// Plays a random Sound Effect
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="clips"></param>
        /// <returns></returns>
        public AudioSource PlayRandomSound(int audioMixerIndex, params AudioClip[] clips)
        {
            AudioSource source = CreateAudioSource(clips[Random.Range(0, clips.Length)]);
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            Destroy(source.gameObject, source.clip.length);
            return source;
        }

        /// <summary>
        /// Plays a 3D sound in a certain AudioMixerGroup
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="audio"></param>
        /// <param name="position"></param>
        /// <param name="minDistance"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public AudioSource Play3DSound(int audioMixerIndex, AudioClip audio, Vector3 position, float minDistance = 1, float maxDistance = 1000, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(audio, volume);
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.transform.position = position;
            source.spatialBlend = 1;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
            source.Play();
            Destroy(source.gameObject, audio.length);
            return source;
        }

        /// <summary>
        /// Plays a 3D sound in a certain AudioMixerGroup
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="audio"></param>
        /// <param name="position"></param>
        /// <param name="minDistance"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public AudioSource PlayRandom3DSound(int audioMixerIndex, AudioClip[] audio, Vector3 position, float minDistance, float maxDistance)
        {
            return Play3DSound(audioMixerIndex, audio[Random.Range(0, audio.Length)], position, minDistance, maxDistance);
        }

        /// <summary>
        /// Plays a looping sound on a certain AudioMixerGroup
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="audio"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public AudioSource PlayLoopingSound(int audioMixerIndex, AudioClip audio, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(audio, volume);
            source.loop = true;
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            return source;
        }

        /// <summary>
        /// Plays a looping sound
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public AudioSource PlayLoopingSound(AudioClip audio)
        {
            AudioSource source = CreateAudioSource(audio);
            source.loop = true;
            source.Play();
            return source;
        }

        /// <summary>
        /// Sets a exposed float parameter in the audiomixer
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="propertyName"></param>
        /// <param name="set"></param>
        public void ChangeAudioProperty(int audioMixerIndex, string propertyName, float set)
        {
            audioMixerGroups[audioMixerIndex].audioMixer.SetFloat(propertyName, set);
        }

        /// <summary>
        /// Change the audio volume
        /// </summary>
        /// <param name="audioMixerIndex"></param>
        /// <param name="propertyName"></param>
        /// <param name="set">a number between 0 and 1, the audio gets set in a linear way</param>
        public void ChangeAudioVolume(int audioMixerIndex, string propertyName, float set)
        {
            ChangeAudioProperty(audioMixerIndex, propertyName, Mathf.Log10(set) * 20);
        }

        /// <summary>
        /// Destory all AudioSources created by the AudioManager
        /// </summary>
        public void StopAllAudio()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Pause all AudioSources created by the AudioManager
        /// </summary>
        public void PauseAllAudio()
        {
            foreach (AudioSource s in AudioSources)
                s.Pause();
        }

        /// <summary>
        /// Play all AudioSources created by the AudioManager
        /// </summary>
        public void PlayAllAudio()
        {
            foreach (AudioSource s in AudioSources)
                s.Play();
        }

        private AudioSource CreateAudioSource(AudioClip clip, float volume = 0.5f)
        {
            AudioSource source = new GameObject(clip.name, typeof(AudioSource)).GetComponent<AudioSource>();
            source.transform.parent = transform;
            source.clip = clip;
            source.volume = volume;

            return source;
        }

        /// <summary>
        /// Fades the audio out over time
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public void FadeinAudio(AudioSource audioSource, float time) => StartCoroutine(Fadein(audioSource, time));

        /// <summary>
        /// Fades the audio in over time
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public void FadeoutAudio(AudioSource audioSource, float time) => StartCoroutine(Fadeout(audioSource, time));

        private IEnumerator Fadeout(AudioSource audioSource, float time)
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime / time;
                yield return null;
            }

            Destroy(audioSource.gameObject);
        }

        private IEnumerator Fadein(AudioSource audioSource, float time)
        {
            while (audioSource.volume < 1)
            {
                audioSource.volume += Time.deltaTime / time;
                yield return null;
            }

            Destroy(audioSource.gameObject);
        }

        #endregion
    }
}