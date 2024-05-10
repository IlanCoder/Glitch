using Attacks;

namespace Characters.Player {
    public class PlayerAnimOverrider : CharacterAnimOverrider {
        readonly string _combosString = "Combo_";

        public void OverrideCombos(AttackInfo[] attacks, int overrideStartIndex) {
            for (int i = overrideStartIndex; i < attacks.Length; i++) {
                OverrideController[_combosString + (i + 1)] = attacks[i].AttackAnimation;
            }
            CharacterAnimator.runtimeAnimatorController = OverrideController;
        }
    }
}