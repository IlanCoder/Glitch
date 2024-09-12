using System;
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
        [SerializeField] protected DamageTypes attackDmgTypes;
        [SerializeField, Min(0.1f)] protected float motionMultiplier;
        [SerializeField, Min(0.1f)] protected float energyGainMultiplier;
        [SerializeField, Min(0.1f)] protected float poiseDamageMultiplier;

        public AnimationClip AttackAnimation => attackAnimation;
        public AudioClip SwingAudioClip => swingAudioClip;
        public AttackType AttackType => attackType;
        public DamageTypes AttackDamageTypes => attackDmgTypes;
        public float EnergyGainMultiplier => energyGainMultiplier;
        public float MotionCostMultiplier => motionMultiplier;
        public float PoiseDamageMultiplier => poiseDamageMultiplier;
    }
}