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

        public enum AudioType { Master, Music, SFX }

        [SerializeField] private AudioMixerGroup Master;
        [SerializeField] private AudioMixerGroup Music;
        [SerializeField] private AudioMixerGroup SFX;

        public bool resetAudioOnLoad = true;
        public bool dontDestroyOnLoad = true;

        public static BasicAudioManager instance;
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

        public AudioSource PlaySound(AudioType type, AudioClip clip, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(clip, volume);
            source.outputAudioMixerGroup = GetAudioType(type);
            source.Play();
            Destroy(source.gameObject, clip.length);
            return source;
        }

        public AudioSource PlaySound(AudioClip clip)
        {
            return PlaySound(AudioType.Master, clip);
        }

        public AudioSource PlayRandomSound(params AudioClip[] clips)
        {
            return PlayRandomSound(AudioType.Master, clips);
        }

        public AudioSource PlayRandomSound(AudioType type, params AudioClip[] clips)
        {
            AudioSource source = CreateAudioSource(clips[Random.Range(0, clips.Length)]);
            source.outputAudioMixerGroup = GetAudioType(type);
            source.Play();
            Destroy(source.gameObject, source.clip.length);
            return source;
        }

        public AudioSource Play3DSound(AudioType type, AudioClip audio, Vector3 position, float minDistance = 1, float maxDistance = 1000, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(audio, volume);
            source.outputAudioMixerGroup = GetAudioType(type);
            source.transform.position = position;
            source.spatialBlend = 1;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
            source.Play();
            Destroy(source.gameObject, audio.length);
            return source;
        }

        public AudioSource PlayRandom3DSound(AudioType type, AudioClip[] audio, Vector3 position, float minDistance, float maxDistance)
        {
            return Play3DSound(type, audio[Random.Range(0, audio.Length)], position, minDistance, maxDistance);
        }

        public AudioSource PlayLoopingSound(AudioType type, AudioClip audio, float volume = 0.5f)
        {
            AudioSource source = CreateAudioSource(audio, volume);
            source.loop = true;
            source.outputAudioMixerGroup = GetAudioType(type);
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

        public void ChangeAudioProperty(AudioType type, string propertyName, float set)
        {
            AudioMixerGroup group = GetAudioType(type);
            group.audioMixer.SetFloat(propertyName, set);
        }

        public void ChangeAudioVolume(AudioType type, string propertyName, float set)
        {
            ChangeAudioProperty(type, propertyName, Mathf.Log10(set) * 20);
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

        private AudioMixerGroup GetAudioType(AudioType type)
        {
            if (SFX != null && Music != null)
            {
                switch (type)
                {
                    case AudioType.Music:
                        return Music;
                    case AudioType.SFX:
                        return SFX;
                    case AudioType.Master:
                        return Master;
                }
            }
            return null;
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