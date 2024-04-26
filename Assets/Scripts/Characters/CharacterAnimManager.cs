using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimManager : MonoBehaviour {
        protected CharacterManager manager;
        protected Animator animator;

        #region Animation String Hashes
        readonly protected Dictionary<string, int> AnimationHashes = new Dictionary<string, int> {
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

        protected virtual void PlayTargetAttackAnimation(AttackType attackType) {
            manager.isPerformingAction = true;
            animator.applyRootMotion = true;
            manager.movementLocked = true;
            manager.rotationLocked = true;
            switch (attackType) {
                case AttackType.Light:
                    animator.CrossFade(Animator.StringToHash("Light_1"), 0.2f);
                    break;
            }
        }

        public void PlayDeathAnimation() {
            PlayTargetAnimation(AnimationHashes["Death"], false);
        }

        public void PlayStaggerAnimation(float staggerAngle) {
            int hash = staggerAngle switch {
                >= 135 => AnimationHashes["Stagger_F"],
                >= 45 => AnimationHashes["Stagger_R"],
                >= -45 => AnimationHashes["Stagger_B"],
                >= -135 => AnimationHashes["Stagger_L"],
                _ => AnimationHashes["Stagger_F"]
            };
            PlayTargetAnimation(hash, false);
        }
    }
}