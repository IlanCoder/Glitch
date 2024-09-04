using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class DisableDeflectCollider : StateMachineBehaviour{
        CharacterCombatController _combatController;
            
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _combatController ??= animator.GetComponent<CharacterCombatController>();
            _combatController.DisableDeflectCollider();
        }
    }
}