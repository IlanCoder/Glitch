using System.Collections.Generic;
using Attacks.NPC;
using UnityEngine;

namespace Characters.NPC {
    public class NpcAnimController : CharacterAnimController {
        #region Animation String Hashes
        readonly protected int RotationYIntHash = Animator.StringToHash("RotationY");
        #endregion
        
        public override void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(_horizontalInputFloatHash, horizontal,0.1f, Time.fixedDeltaTime);
            animator.SetFloat(_verticalInputFloatHash, vertical,0.1f, Time.fixedDeltaTime);
        }

        public void UpdateRotation(int y) {
            animator.SetInteger(RotationYIntHash, y);
        }
        
        public void PlayAttackAnimation(bool firstInChain = true) {
            if (firstInChain) {
                animator.Play(FirstAttackAnimationHash);
                return;
            }
            animator.SetTrigger(ContinueAttackChainTriggerHash);
        }
    }
}