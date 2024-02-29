using System;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(CharacterController))]
    public class CharacterManager : MonoBehaviour {
        [HideInInspector]public CharacterController controller;
        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);
            controller = GetComponent<CharacterController>();
        }

        protected virtual void Update() {
            
        }

        protected virtual void LateUpdate() {
            
        }
    }
}
