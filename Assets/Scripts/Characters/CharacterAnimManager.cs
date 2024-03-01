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
    }
}