using Characters;
using Characters.NPC;
using UnityEngine;
using UnityEngine.Events;

namespace AnimatorScripts.NPC {
    public class InvokeNewAttackEvent : StateMachineBehaviour {
        public static UnityEvent AttackStarted = new UnityEvent();
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            AttackStarted.Invoke();
        }
    }
}