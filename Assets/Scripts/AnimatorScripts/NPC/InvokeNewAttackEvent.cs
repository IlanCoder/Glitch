using Characters;
using Characters.NPC;
using UnityEngine;
using UnityEngine.Events;

namespace AnimatorScripts.NPC {
    public class InvokeNewAttackEvent : StateMachineBehaviour {
        public static UnityEvent AttackStarted = new UnityEvent();
        public static UnityEvent AttackFinished = new UnityEvent();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            AttackStarted.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            AttackFinished.Invoke();
        }
    }
}