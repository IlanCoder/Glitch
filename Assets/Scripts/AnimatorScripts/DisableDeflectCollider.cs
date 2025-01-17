﻿using Characters;
using Enums;
using UnityEngine;

namespace AnimatorScripts {
    public class DisableDeflectCollider : StateMachineBehaviour{
        CharacterDeflectController _deflectController;
            
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _deflectController ??= animator.GetComponent<CharacterDeflectController>();
            _deflectController.DisableDeflectCollider();
            _deflectController.deflectQuality = DeflectQuality.Miss;
        }
    }
}