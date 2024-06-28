using Attacks.NPC;

namespace Characters.NPC {
    public class NpcAnimOverrider : CharacterAnimOverrider {
        int comboStringIndex = 0;
        
        public void OverrideNextAttack(NpcAttack attack, bool firstInChain = true) {
            if (firstInChain) {
                comboStringIndex = 1;
            } else if(comboStringIndex == 1) {
                comboStringIndex = 2;
            } else {
                comboStringIndex = 1;
            }
            OverrideController[_combosString + comboStringIndex] = attack.AttackAnimation;
            CharacterAnimator.runtimeAnimatorController = OverrideController;
        }
    }
}