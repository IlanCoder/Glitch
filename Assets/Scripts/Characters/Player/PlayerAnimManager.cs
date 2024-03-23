using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player {
    
    public class PlayerAnimManager : CharacterAnimManager {
        readonly protected Dictionary<string, int> PlayerAnimationHashes = new Dictionary<string, int>() {
            { "Dodge_F", Animator.StringToHash("Dodge_F") },
            { "Dodge_B", Animator.StringToHash("Dodge_B") },
            { "Jump_Start", Animator.StringToHash("Jump_Start") }
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

        public void Revive() {
            PlayTargetAnimation(Animator.StringToHash("Movement"), true);
        }
    }
}