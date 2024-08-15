using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class InvokeAttackEvents : StateMachineBehaviour {
        CharacterCombatController _combatController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _combatController ??= animator.GetComponent<CharacterCombatController>();
            _combatController.onAttackStarted?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _combatController ??= animator.GetComponent<CharacterCombatController>();
            _combatController.onAttackFinished?.Invoke();
        }
    }
}