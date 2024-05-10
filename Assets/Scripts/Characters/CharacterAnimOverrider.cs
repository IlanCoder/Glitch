using UnityEngine;
using UnityEngine.Rendering;

namespace Characters {
    public class CharacterAnimOverrider : MonoBehaviour {
        protected AnimatorOverrideController OverrideController;
        protected Animator CharacterAnimator;
        protected void Awake() {
            CharacterAnimator = GetComponent<Animator>(); 
            OverrideController = new AnimatorOverrideController(CharacterAnimator.runtimeAnimatorController);
        }
    }
}