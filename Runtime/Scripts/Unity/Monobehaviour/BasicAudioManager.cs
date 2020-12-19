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

        [SerializeField] private AudioMixerGroup[] audioMixerGroups;

        /// <summary>
        /// Defines whether the AudioManager should reset the audio on scene change
        /// </summary>
        [Tooltip("Defines whether the AudioManager should reset the audio on scene change")]
        public bool resetAudioOnLoad = true;

        [SerializeField, Tooltip("Defines whether the AudioManager should be destroyed on scene change")]
        private bool dontDestroyOnLoad = true;

        private static BasicAudioManager instance;
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

        public AudioSource PlaySound(int audioMixerIndex, AudioClip clip, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(clip, volume);
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            Destroy(source.gameObject, clip.length);
            return source;
        }

        public AudioSource PlaySound(AudioClip clip)
        {
            return PlaySound(0, clip);
        }

        public AudioSource PlayRandomSound(params AudioClip[] clips)
        {
            return PlayRandomSound(0, clips);
        }

        public AudioSource PlayRandomSound(int audioMixerIndex, params AudioClip[] clips)
        {
            AudioSource source = CreateAudioSource(clips[Random.Range(0, clips.Length)]);
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            Destroy(source.gameObject, source.clip.length);
            return source;
        }

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

        public AudioSource PlayRandom3DSound(int audioMixerIndex, AudioClip[] audio, Vector3 position, float minDistance, float maxDistance)
        {
            return Play3DSound(audioMixerIndex, audio[Random.Range(0, audio.Length)], position, minDistance, maxDistance);
        }

        public AudioSource PlayLoopingSound(int audioMixerIndex, AudioClip audio, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(audio, volume);
            source.loop = true;
            source.outputAudioMixerGroup = audioMixerGroups[audioMixerIndex];
            source.Play();
            return source;
        }

        public AudioSource PlayLoopingSound(AudioClip audio)
        {
            AudioSource source = CreateAudioSource(audio);
            source.loop = true;
            source.Play();
            return source;
        }

        public void ChangeAudioProperty(int audioMixerIndex, string propertyName, float set)
        {
            audioMixerGroups[audioMixerIndex].audioMixer.SetFloat(propertyName, set);
        }

        public void ChangeAudioVolume(int audioMixerIndex, string propertyName, float set)
        {
            ChangeAudioProperty(audioMixerIndex, propertyName, Mathf.Log10(set) * 20);
        }

        public void StopAllAudio()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void PauseAllAudio()
        {
            foreach (AudioSource s in AudioSources)
                s.Pause();
        }

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

        public void FadeinAudio(AudioSource audioSource, float time) => StartCoroutine(Fadein(audioSource, time));
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

        /// <summary>
        /// Fades the Audio in
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="time"></param>
        /// <returns></returns>
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