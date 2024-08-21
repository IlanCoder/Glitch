using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Characters.Player {
    
    public class PlayerAnimController : CharacterAnimController {
        readonly protected Dictionary<string, int> PlayerAnimationHashes = new Dictionary<string, int>() {
            { "Dodge_F", Animator.StringToHash("Dodge_F") },
            { "Dodge_B", Animator.StringToHash("Dodge_B") },
            { "Jump_Start", Animator.StringToHash("Jump_Start") },
        };

        public void PlayDodgeAnimation(bool backStep = false) {
            if (backStep) {
                PlayTargetAnimation(PlayerAnimationHashes["Dodge_B"], false);
                return;
            }
            PlayTargetAnimation(PlayerAnimationHashes["Dodge_F"], false);
        }

        public void PlayJumpAnimation() {
            PlayTargetAnimation(PlayerAnimationHashes["Jump_Start"], false);
        }

        public void PlayReviveAnimation() {
            PlayTargetAnimation(Animator.StringToHash("Movement"), true, false, 
                false);
        }

        public void PlayAttackAnimation(int comboAttackIndex = 0) {
            manager.isPerformingAction = true;
            animator.applyRootMotion = true;
            manager.movementLocked = true;
            manager.rotationLocked = true;
            if (comboAttackIndex == 0) {
                animator.CrossFade(FirstAttackAnimationHash, 0.1f);
                return;
            }
            animator.SetTrigger(ContinueAttackChainTriggerHash);
        }
    }
}