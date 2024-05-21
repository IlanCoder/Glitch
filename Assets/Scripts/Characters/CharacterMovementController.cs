using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Characters {
    public class CharacterMovementController : MonoBehaviour {
        private CharacterManager _manager;
        
        [Header("Ground Check")] 
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 1;

        [Header("Gravity Velocities")] 
        [SerializeField] protected float groundedYVel = -20;
        [SerializeField] protected float gravityForce = -9.81f;
        [SerializeField] protected float fallStartVel = -5;
        [SerializeField] protected float fallMaxVel = -50;
        protected Vector3 yVel;
        protected bool fallingVelHasBeenSet;
        protected float inAirTimer;

        protected virtual void Awake() {
            _manager = GetComponent<CharacterManager>();
        }

        void GetYVel() {
            if (_manager.isGrounded) {
                if (yVel.y > 0) return;
                inAirTimer = 0;
                fallingVelHasBeenSet = false;
                yVel.y = groundedYVel;
                return;
            }
            if (!_manager.isJumping && !fallingVelHasBeenSet) {
                yVel.y = fallStartVel;
                fallingVelHasBeenSet = true;
            }
            inAirTimer += Time.deltaTime;
            _manager.AnimController.SetAirTimerFloat(inAirTimer);
            if (yVel.y <= fallMaxVel) return;
            yVel.y += gravityForce * Time.deltaTime;
        }

        public virtual void HandleGravity() {
            GetYVel();
            _manager.characterController.Move(yVel * Time.deltaTime);
        }
        
        public void HandleGroundCheck() {
            bool grounded = Physics.CheckSphere(transform.position, groundCheckSphereRadius, groundLayer);
            if (grounded == _manager.isGrounded) return;
            _manager.isGrounded = grounded;
            _manager.AnimController.SetGroundedBool(_manager.isGrounded);
        }
        
        #if UNITY_EDITOR
        protected void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, groundCheckSphereRadius);
        }
        #endif
    }
}
