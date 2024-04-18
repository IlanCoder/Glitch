using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimManager : MonoBehaviour {
        protected CharacterManager manager;

        #region Animation String Hashes
        readonly protected Dictionary<string, int> AnimationHashes = new Dictionary<string, int>() {
            { "Vertical", Animator.StringToHash("Vertical") },
            { "Horizontal", Animator.StringToHash("Horizontal") },
            { "Death", Animator.StringToHash("Death") }
        };
        #endregion
        
        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
        }
        
        public void UpdateMovementParameters(float horizontal, float vertical) {
            manager.animator.SetFloat(AnimationHashes["Horizontal"], horizontal,0.1f, Time.deltaTime);
            manager.animator.SetFloat(AnimationHashes["Vertical"], vertical,0.1f, Time.deltaTime);
        }
        
        protected virtual void PlayTargetAnimation(int targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            manager.isPerformingAction = !cancellableAction;
            manager.animator.applyRootMotion = lockMovement;
            manager.animator.CrossFade(targetAnimation, 0.2f);
            manager.movementLocked = lockMovement;
            manager.rotationLocked = lockRotation;
        }

        protected virtual void PlayTargetAttackAnimation(AttackType attackType) {
            manager.isPerformingAction = true;
            manager.animator.applyRootMotion = true;
            manager.movementLocked = true;
            manager.rotationLocked = true;
            switch (attackType) {
                case AttackType.Light:
                    manager.animator.CrossFade(Animator.StringToHash("Light_1"), 0.2f);
                    break;
            }
        }

        public void PlayDeathAnimation() {
            PlayTargetAnimation(AnimationHashes["Death"], false);
        }
    }
}