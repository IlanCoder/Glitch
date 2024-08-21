using Attacks;
using Attacks.Player;
using UnityEngine;

namespace Characters.Player {
    public class PlayerAnimOverrider : CharacterAnimOverrider {
        public void OverrideCombos(PlayerAttack[] attacks, int overrideStartIndex) {
            for (int i = overrideStartIndex; i < attacks.Length; i++) {
                OverrideController[_combosString + (i + 1)] = attacks[i].AttackAnimation;
            }
            CharacterAnimator.runtimeAnimatorController = OverrideController;
        }
    }
}