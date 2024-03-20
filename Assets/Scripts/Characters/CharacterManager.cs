using System;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
    public class CharacterManager : MonoBehaviour {
        [HideInInspector]public Animator animator;
        [HideInInspector]public CharacterController controller;

        #region Flags
        [HideInInspector] public bool isPerformingAction;
        [HideInInspector] public bool rotationLocked;
        [HideInInspector] public bool movementLocked;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public bool isJumping;
        [HideInInspector] public bool isGrounded;
        #endregion
        
        protected virtual void Awake() {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Update() {
        }

        protected virtual void LateUpdate() {
        }
    }
}
