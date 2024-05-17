using System;
using UnityEngine;
using UnityEngine.Events;

namespace Characters {
    [RequireComponent(typeof(CharacterController)), 
     RequireComponent(typeof(Animator)),
     RequireComponent(typeof(CharacterStatsController)),
     RequireComponent(typeof(CharacterAnimController)),
     RequireComponent(typeof(CharacterEffectsController)),
     RequireComponent(typeof(CharacterCombatController)),
     RequireComponent(typeof(CharacterVFxController)),
     RequireComponent(typeof(CharacterSFxController))]
    public class CharacterManager : MonoBehaviour {
        
        [HideInInspector]public CharacterController controller;
        public virtual CharacterStatsController StatsController => GetComponent<CharacterStatsController>();
        public virtual CharacterAnimController AnimController => GetComponent<CharacterAnimController>();
        public virtual CharacterEffectsController EffectsController => GetComponent<CharacterEffectsController>();
        public virtual CharacterCombatController CombatController => GetComponent<CharacterCombatController>();
        public virtual CharacterVFxController VFxController => GetComponent<CharacterVFxController>();
        public virtual CharacterSFxController SFxController => GetComponent<CharacterSFxController>();

        [HideInInspector] public UnityEvent onCharacterDeath;
        
        #region Flags
        public bool isDead { get; private set; }
        [HideInInspector] public bool isPerformingAction;
        [HideInInspector] public bool rotationLocked;
        [HideInInspector] public bool movementLocked;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public bool isJumping;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public bool isLockedOn;
        #endregion
        
        protected virtual void Awake() {
            controller = GetComponent<CharacterController>();
        }

        protected virtual void Update() {
        }

        protected virtual void LateUpdate() {
        }

        public virtual void HandleDeathEvent() {
            isDead = true;
            onCharacterDeath?.Invoke();
            AnimController.PlayDeathAnimation();
        }

        public virtual void ReviveCharacter() {
            isDead = false;
        }
        
        public virtual void HandleLockOn() {
        }
    }
}
