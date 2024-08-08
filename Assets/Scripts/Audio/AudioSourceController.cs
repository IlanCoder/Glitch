using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceController : MonoBehaviour {
        AudioSource _source;

        void Awake() {
            _source = GetComponent<AudioSource>();
        }

        public virtual void PlaySFx(AudioClip sFx, float volume = 1) {
            _source.volume = volume;
            _source.PlayOneShot(sFx, volume);
        }
        
        public virtual void PlayPitchedSFx(AudioClip sFx, float pitchRange = 0.1f, float volume = 1) {
            _source.pitch = 1 + Random.Range(-pitchRange, pitchRange);
            _source.volume = volume;
            _source.PlayOneShot(sFx);
        }
    }
}