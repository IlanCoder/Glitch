using UnityEngine;

namespace Characters.NPC {
    public class NpcAnimController : CharacterAnimController {
        public override void UpdateMovementParameters(float horizontal, float vertical) {
            animator.SetFloat(_horizontalInputFloatHash, horizontal,0.1f, Time.fixedDeltaTime);
            animator.SetFloat(_verticalInputFloatHash, vertical,0.1f, Time.fixedDeltaTime);
        }
    }
}