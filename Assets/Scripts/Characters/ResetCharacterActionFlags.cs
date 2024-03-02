using UnityEngine;

namespace Characters {
    public class ResetCharacterActionFlags : StateMachineBehaviour {
        CharacterManager _character;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _character ??= animator.GetComponent<CharacterManager>();
            _character.isPerformingAction = false;
            _character.rotationLocked = false;
            _character.movementLocked = false;
            animator.applyRootMotion = false;  
        }
    }
}
