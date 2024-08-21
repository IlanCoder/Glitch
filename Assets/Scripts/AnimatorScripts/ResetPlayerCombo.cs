using Characters;
using Characters.Player;
using UnityEngine;

namespace AnimatorScripts {
    public class ResetPlayerCombo : StateMachineBehaviour{
        PlayerManager _player;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _player ??= animator.GetComponent<PlayerManager>();
            if (!_player) return;
            if (_player.combatController.IsAttacking) _player.combatController.ResetCombo();
        }
    }
}