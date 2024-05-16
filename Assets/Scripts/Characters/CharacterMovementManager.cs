using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Characters {
    public class CharacterMovementManager<T> : MonoBehaviour where T : CharacterManager {
        protected T manager;
        
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
            manager = GetComponent<T>();
        }

        protected void Update() {
            HandleGroundCheck();
            manager.AnimManager.SetGroundedBool(manager.isGrounded);
            GetYVel();
            manager.controller.Move(yVel * Time.deltaTime);
        }

        void GetYVel() {
            if (manager.isGrounded) {
                if (yVel.y > 0) return;
                inAirTimer = 0;
                fallingVelHasBeenSet = false;
                yVel.y = groundedYVel;
                return;
            }
            if (!manager.isJumping && !fallingVelHasBeenSet) {
                yVel.y = fallStartVel;
                fallingVelHasBeenSet = true;
            }
            inAirTimer += Time.deltaTime;
            manager.AnimManager.SetAirTimerFloat(inAirTimer);
            if (yVel.y <= fallMaxVel) return;
            yVel.y += gravityForce * Time.deltaTime;
        }

        void HandleGroundCheck() {
            manager.isGrounded = Physics.CheckSphere(transform.position, groundCheckSphereRadius, groundLayer);
        }
        
        #if UNITY_EDITOR
        protected void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, groundCheckSphereRadius);
        }
        #endif
    }
}
