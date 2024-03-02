using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimManager : MonoBehaviour {
        CharacterManager _character;
        protected virtual void Awake() {
            _character = GetComponent<CharacterManager>();
        }
        
        public void UpdateMovementParameters(float horizontal, float vertical) {
            _character.animator.SetFloat("Horizontal", horizontal,0.1f, Time.deltaTime);
            _character.animator.SetFloat("Vertical", vertical,0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetAnimation(string targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            _character.isPerformingAction = !cancellableAction;
            _character.animator.applyRootMotion = lockMovement;
            _character.animator.CrossFade(targetAnimation, 0.2f);
            _character.movementLocked = lockMovement;
            _character.rotationLocked = lockRotation;
        }
    }
}