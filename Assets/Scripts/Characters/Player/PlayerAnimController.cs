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
        
        readonly protected Dictionary<string, int> AttackAnimationHashes = new Dictionary<string, int> {
            { "Combo_1", Animator.StringToHash("Combo_1") },
            { "Combo_2", Animator.StringToHash("Combo_2") },
            { "Combo_3", Animator.StringToHash("Combo_3") },
            { "Combo_4", Animator.StringToHash("Combo_4") },
            { "Combo_5", Animator.StringToHash("Combo_5") },
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

        public void PlayAttackAnimation(int comboAttackIndex) {
            manager.isPerformingAction = true;
            animator.applyRootMotion = true;
            manager.movementLocked = true;
            manager.rotationLocked = true;
            if (comboAttackIndex == 0) {
                animator.CrossFade(AttackAnimationHashes["Combo_1"], 0.1f);
                return;
            }
            animator.Play(AttackAnimationHashes[$"Combo_{comboAttackIndex + 1}"]);
        }
    }
}