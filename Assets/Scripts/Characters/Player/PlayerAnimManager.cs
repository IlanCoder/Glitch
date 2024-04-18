using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Characters.Player {
    
    public class PlayerAnimManager : CharacterAnimManager {
        readonly protected Dictionary<string, int> PlayerAnimationHashes = new Dictionary<string, int>() {
            { "Dodge_F", Animator.StringToHash("Dodge_F") },
            { "Dodge_B", Animator.StringToHash("Dodge_B") },
            { "Jump_Start", Animator.StringToHash("Jump_Start") },
            { "Equip_Weapon", Animator.StringToHash("Equip_Weapon") }
        };

        public void PlayDodgeAnimation(bool backStep=false) {
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

        public void PlayEquipAnimation() {
            PlayTargetAnimation(PlayerAnimationHashes["Equip_Weapon"], true, false, 
                false);
        }

        public void PlayAttackAnimation(AttackType attackType) {
            PlayTargetAttackAnimation(attackType);
        }
    }
}