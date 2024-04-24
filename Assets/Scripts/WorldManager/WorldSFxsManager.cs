using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldManager {
    public class WorldSFxsManager : MonoBehaviour {
        public static WorldSFxsManager Instance;

        [Header("Damage SFXs")]
        [SerializeField] AudioClip[] fleshDamageSFxs;
        
        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        public AudioClip GetRandomDamageAudioClip(DamageSFxType sFxType) {
            AudioClip clip = null;
            switch (sFxType) {
                case DamageSFxType.Flesh:
                    int randIndex = Random.Range(0, fleshDamageSFxs.Length);
                    clip = fleshDamageSFxs[randIndex];
                    break;
            }
            return clip;
        }
    }
}