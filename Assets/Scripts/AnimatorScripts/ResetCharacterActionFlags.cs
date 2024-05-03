using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class ResetCharacterActionFlags : StateMachineBehaviour {
        CharacterManager _character;
        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _character ??= animator.GetComponent<CharacterManager>();
            _character.isPerformingAction = false;
            _character.rotationLocked = false;
            _character.movementLocked = false;
            _character.isJumping = false;
            animator.applyRootMotion = false;
        }
    }
}
