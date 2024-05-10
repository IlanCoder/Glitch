using Characters;
using Characters.Player;
using UnityEngine;

namespace AnimatorScripts {
    public class ResetPlayerCombo : StateMachineBehaviour{
        PlayerManager _player;
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _player ??= animator.GetComponent<PlayerManager>();
            //_player.combatManager.ResetCombo();
        }
    }
}