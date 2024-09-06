using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldManager {
    public class WorldSFxsManager : MonoBehaviour {
        public static WorldSFxsManager Instance;

        [Header("Damage SFXs")]
        [SerializeField] AudioClip[] fleshDamageSFxs;
        
        [Header("Deflect SFXs")]
        [SerializeField] AudioClip perfectDeflectSFx;
        [SerializeField] AudioClip imperfectDeflectSFx;
        
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

        public AudioClip GetDeflectAudioClip(DeflectQuality deflectType) {
            return deflectType switch {
                DeflectQuality.Miss => null,
                DeflectQuality.Imperfect => imperfectDeflectSFx,
                DeflectQuality.Perfect => perfectDeflectSFx,
                _ => throw new ArgumentOutOfRangeException(nameof(deflectType), deflectType, null)
            };
        }
    }
}