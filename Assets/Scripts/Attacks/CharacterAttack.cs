using Enums;
using UnityEngine;

namespace Attacks {
    public abstract class CharacterAttack : ScriptableObject {
        [Header("Animation")]
        [SerializeField] protected AnimationClip attackAnimation;

        [Header("Audio")]
        [SerializeField] protected AudioClip swingAudioClip;
        
        [Header("Parameters")] 
        [SerializeField] protected AttackType attackType;
        [SerializeField, Min(0)] protected float motionMultiplier;
        [SerializeField, Min(0)] protected float energyGainMultiplier;
        
        
        public AnimationClip AttackAnimation => attackAnimation;
        public AudioClip SwingAudioClip => swingAudioClip;
        public AttackType AttackType => attackType;
        public float EnergyGainMultiplier => energyGainMultiplier;
        public float MotionCostMultiplier => motionMultiplier;
    }
}