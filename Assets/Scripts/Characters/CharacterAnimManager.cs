using System;
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
            bool lockMovement = true, bool lockRotation = true, bool applyRootMotion = true) {
            _character.animator.applyRootMotion = applyRootMotion;
            _character.animator.CrossFade(targetAnimation, 0.2f);
            _character.isPerformingAction = !cancellableAction;
            _character.movementLocked = lockMovement;
            _character.rotationLocked = lockRotation;
        }
    }
}