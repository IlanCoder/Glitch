using System.Collections.Generic;
using Attacks.NPC;
using UnityEngine;

namespace Characters.NPC {
    public class NpcAnimController : CharacterAnimController {
        #region Animation String Hashes
        readonly protected int RotationYIntHash = Animator.StringToHash("RotationY");
        #endregion
        
        public override void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(HorizontalInputFloatHash, horizontal,0.1f, Time.fixedDeltaTime);
            animator.SetFloat(VerticalInputFloatHash, vertical,0.1f, Time.fixedDeltaTime);
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