using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Characters {
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

        readonly protected Dictionary<string, int> WeaponLayerAnimationHashes = new Dictionary<string, int> {
            { "Equip_Weapon", Animator.StringToHash("Equip_Weapon") },
            { "Deflect_Start", Animator.StringToHash("Deflect_Start") }
        };

        readonly protected int HorizontalInputFloatHash = Animator.StringToHash("Horizontal");
        readonly protected int VerticalInputFloatHash = Animator.StringToHash("Vertical");
        readonly protected int IsGroundedBoolHash = Animator.StringToHash("IsGrounded");
        
        readonly protected int FirstAttackAnimationHash = Animator.StringToHash("Combo_1");
        readonly protected int ContinueAttackChainTriggerHash = Animator.StringToHash("ContinueAttackChain");
        
        readonly protected int DeflectHeldBoolHash = Animator.StringToHash("DeflectHeld");
        readonly protected int DeflectHitTriggerHash = Animator.StringToHash("DeflectHit");
        #endregion
        
        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
            animator = GetComponent<Animator>();
        }
        
        protected void PlayTargetAnimation(int targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            manager.isPerformingAction = !cancellableAction;
            animator.applyRootMotion = lockMovement;
            animator.CrossFade(targetAnimation, 0.2f);
            manager.movementLocked = lockMovement;
            manager.rotationLocked = lockRotation;
        }

        public void SetGroundedBool(bool newVal) {
            animator.SetBool(IsGroundedBoolHash, newVal);
        }

        public virtual void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(HorizontalInputFloatHash, horizontal,0.1f, Time.deltaTime);
            animator.SetFloat(VerticalInputFloatHash, vertical,0.1f, Time.deltaTime);
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
        
        public void PlayEquipAnimation() {
            PlayTargetAnimation(WeaponLayerAnimationHashes["Equip_Weapon"], true, false, 
            false);
        }

        public void SetDeflectHeldBool(bool newVal) {
            animator.SetBool(DeflectHeldBoolHash, newVal);
        }

        public void TriggerDeflectHitAnimation() {
            animator.SetTrigger(DeflectHitTriggerHash);
        }
    }
}