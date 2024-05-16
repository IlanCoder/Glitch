using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimController : MonoBehaviour {
        protected CharacterManager manager;
        protected Animator animator;

        #region Animation String Hashes
        readonly protected Dictionary<string, int> DamageAnimationHashes = new Dictionary<string, int> {
            { "Death", Animator.StringToHash("Death") },
            { "Stagger_F", Animator.StringToHash("Stagger_F") },
            { "Stagger_L", Animator.StringToHash("Stagger_L") },
            { "Stagger_B", Animator.StringToHash("Stagger_B") },
            { "Stagger_R", Animator.StringToHash("Stagger_R") },
        };
        readonly int _horizontalInputFloatHash = Animator.StringToHash("Horizontal");
        readonly int _verticalInputFloatHash = Animator.StringToHash("Vertical");
        readonly int _inAirTimerFloatHash = Animator.StringToHash("InAirTimer");
        readonly int _isGroundedBoolHash = Animator.StringToHash("IsGrounded");
        #endregion
        
        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
            animator = GetComponent<Animator>();
        }

        public void SetGroundedBool(bool newVal) {
            animator.SetBool(_isGroundedBoolHash, newVal);
        }

        public void SetAirTimerFloat(float newVal) {
            animator.SetFloat(_inAirTimerFloatHash, newVal);
        }
        
        public void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(_horizontalInputFloatHash, horizontal,0.1f, Time.deltaTime);
            animator.SetFloat(_verticalInputFloatHash, vertical,0.1f, Time.deltaTime);
        }
        
        protected virtual void PlayTargetAnimation(int targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            manager.isPerformingAction = !cancellableAction;
            animator.applyRootMotion = lockMovement;
            animator.CrossFade(targetAnimation, 0.2f);
            manager.movementLocked = lockMovement;
            manager.rotationLocked = lockRotation;
        }

        public void PlayDeathAnimation() {
            PlayTargetAnimation(DamageAnimationHashes["Death"], false);
        }

        public void PlayStaggerAnimation(float staggerAngle) {
            if(manager.isDead) return;
            int hash = staggerAngle switch {
                >= 135 => DamageAnimationHashes["Stagger_F"],
                >= 45 => DamageAnimationHashes["Stagger_R"],
                >= -45 => DamageAnimationHashes["Stagger_B"],
                >= -135 => DamageAnimationHashes["Stagger_L"],
                _ => DamageAnimationHashes["Stagger_F"]
            };
            PlayTargetAnimation(hash, false);
        }

        public void ApplyRootMotion(bool applyRootMotion) {
            animator.applyRootMotion = applyRootMotion;
        }
    }
}