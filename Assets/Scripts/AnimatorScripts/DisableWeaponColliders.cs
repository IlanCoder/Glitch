using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class DisableWeaponColliders : StateMachineBehaviour{
        CharacterCombatController _combatController;
        
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _combatController ??= animator.GetComponent<CharacterCombatController>();
            _combatController.DisableAttack();
        }
    }
}