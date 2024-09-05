using Characters;
using Enums;
using UnityEngine;

namespace AnimatorScripts {
    public class SetDeflectQuality : StateMachineBehaviour{
        CharacterDeflectController _deflectController;
        [SerializeField] DeflectQuality targetQuality;
            
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _deflectController ??= animator.GetComponent<CharacterDeflectController>();
            _deflectController.deflectQuality = targetQuality;
        }
    }
}