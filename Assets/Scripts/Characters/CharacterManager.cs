using System;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterController)),RequireComponent(typeof(Animator))]
    public class CharacterManager : MonoBehaviour {
        [HideInInspector]public Animator animator;
        [HideInInspector]public CharacterController controller;

        #region Flags
        [HideInInspector] public bool isPerformingAction;
        [HideInInspector] public bool rotationLocked;
        [HideInInspector] public bool movementLocked;
        [HideInInspector] public bool isSprinting;
        #endregion
        
        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Update() {
            
        }

        protected virtual void LateUpdate() {
            
        }
        
        
    }
}
