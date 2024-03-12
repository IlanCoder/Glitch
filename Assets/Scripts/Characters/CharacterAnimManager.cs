using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterManager))]
    public class CharacterAnimManager<T> : MonoBehaviour where T : CharacterManager {
        protected T manager;

        #region Animation String Hashes
        readonly int _horizontalHash = Animator.StringToHash("Horizontal");
        readonly int _verticalHash = Animator.StringToHash("Vertical");
        #endregion
        
        protected virtual void Awake() {
            manager = GetComponent<T>();
        }
        
        public void UpdateMovementParameters(float horizontal, float vertical) {
            manager.animator.SetFloat(_horizontalHash, horizontal,0.1f, Time.deltaTime);
            manager.animator.SetFloat(_verticalHash, vertical,0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetAnimation(string targetAnimation, bool cancellableAction,
            bool lockMovement = true, bool lockRotation = true) {
            manager.isPerformingAction = !cancellableAction;
            manager.animator.applyRootMotion = lockMovement;
            manager.animator.CrossFade(targetAnimation, 0.2f);
            manager.movementLocked = lockMovement;
            manager.rotationLocked = lockRotation;
        }
    }
}