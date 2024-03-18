using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player {
    
    public class PlayerAnimManager : CharacterAnimManager<PlayerManager> {
        readonly Dictionary<string, int> _playerAnimationHashes = new Dictionary<string, int>() {
            { "Dodge_F", Animator.StringToHash("Dodge_F") },
            { "Dodge_B", Animator.StringToHash("Dodge_B") },
            { "Jump_Start", Animator.StringToHash("Jump_Start") }
        };

        public void PlayDodgeAnimation(bool backStep=false) {
            if (backStep) {
                PlayTargetAnimation(_playerAnimationHashes["Dodge_B"], false);
                return;
            }
            PlayTargetAnimation(_playerAnimationHashes["Dodge_F"], false);
        }

        public void PlayJumpAnimation() {
            PlayTargetAnimation(_playerAnimationHashes["Jump_Start"], false);
        }
    }
}