using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class ResetPerformingActionFlag : StateMachineBehaviour {
        CharacterManager _character;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _character ??= animator.GetComponent<CharacterManager>();
            _character.isPerformingAction = false;
        }
    }
}