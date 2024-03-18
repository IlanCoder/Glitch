using System.Collections.Generic;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimManager<T> : MonoBehaviour where T : CharacterManager {
        protected T manager;

        #region Animation String Hashes

        readonly protected Dictionary<string, int> animationHashes = new Dictionary<string, int>() {
            { "Vertical", Animator.StringToHash("Vertical") },
            { "Horizontal", Animator.StringToHash("Horizontal") }
        };
        #endregion
        
        protected virtual void Awake() {
            manager = GetComponent<T>();
        }
        
        public void UpdateMovementParameters(float horizontal, float vertical) {
            manager.animator.SetFloat(animationHashes["Horizontal"], horizontal,0.1f, Time.deltaTime);
            manager.animator.SetFloat(animationHashes["Vertical"], vertical,0.1f, Time.deltaTime);
        }

        protected virtual void PlayTargetAnimation(int targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            manager.isPerformingAction = !cancellableAction;
            manager.animator.applyRootMotion = lockMovement;
            manager.animator.CrossFade(targetAnimation, 0.2f);
            manager.movementLocked = lockMovement;
            manager.rotationLocked = lockRotation;
        }
    }
}