using Characters;
using UnityEngine;

namespace AnimatorScripts {
    public class EnableDeflectCollider : StateMachineBehaviour{
        CharacterDeflectController _deflectController;
            
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _deflectController ??= animator.GetComponent<CharacterDeflectController>();
            _deflectController.EnableDeflectCollider();
        }
    }
}