using System.Collections.Generic;
using Attacks.NPC;
using UnityEngine;

namespace Characters.NPC {
    public class NpcAnimController : CharacterAnimController {
        #region Animation String Hashes
        readonly protected int _rotationYIntHash = Animator.StringToHash("RotationY");
        readonly protected int _continueAttackChainTriggerHash = Animator.StringToHash("ContinueAttackChain");
        
        readonly protected Dictionary<string, int> AttackAnimationHashes = new Dictionary<string, int> {
            { "Combo_1", Animator.StringToHash("Combo_1") },
            { "Combo_2", Animator.StringToHash("Combo_2") },
        };
        #endregion
        
        public override void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(_horizontalInputFloatHash, horizontal,0.1f, Time.fixedDeltaTime);
            animator.SetFloat(_verticalInputFloatHash, vertical,0.1f, Time.fixedDeltaTime);
        }

        public void UpdateRotation(int y) {
            animator.SetInteger(_rotationYIntHash, y);
        }

        public void PlayAttackAnimation(bool firstInChain = true) {
            if (firstInChain) {
                animator.Play(AttackAnimationHashes["Combo_1"]);
                return;
            }
            animator.SetTrigger(_continueAttackChainTriggerHash);
        }

        public bool IsAttackAnimationPlaying(NpcAttack attack) {
            if (animator.GetCurrentAnimatorClipInfo(0).Length <= 0) return true;
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip == attack.AttackAnimation;
        }
    }
}