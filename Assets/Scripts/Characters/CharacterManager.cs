using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterController)), 
     RequireComponent(typeof(Animator)),
     RequireComponent(typeof(CharacterStatsManager)),
     RequireComponent(typeof(CharacterAnimManager)),
     RequireComponent(typeof(CharacterEffectsManager)),
     RequireComponent(typeof(CharacterCombatManager)),
     RequireComponent(typeof(CharacterVFxManager)),
     RequireComponent(typeof(CharacterSFxManager))]
    public class CharacterManager : MonoBehaviour {
        
        [HideInInspector]public CharacterController controller;
        public virtual CharacterStatsManager StatsManager => GetComponent<CharacterStatsManager>();
        public virtual CharacterAnimManager AnimManager => GetComponent<CharacterAnimManager>();
        public virtual CharacterEffectsManager EffectsManager => GetComponent<CharacterEffectsManager>();
        public virtual CharacterCombatManager CombatManager => GetComponent<CharacterCombatManager>();
        public virtual CharacterVFxManager VFxManager => GetComponent<CharacterVFxManager>();
        public virtual CharacterSFxManager SFxManager => GetComponent<CharacterSFxManager>();

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
            AnimManager.PlayDeathAnimation();
        }

        public virtual void ReviveCharacter() {
            
        }
        
        public virtual void HandleLockOn() {
        }
    }
}
