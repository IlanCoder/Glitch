using UnityEngine;

namespace Characters {
    public class ResetCharacterActionFlags : StateMachineBehaviour {
        CharacterManager _character;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _character ??= animator.GetComponent<CharacterManager>();
            _character.isPerformingAction = false;
        }
    }
}
