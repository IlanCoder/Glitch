using UnityEngine;
using UnityEngine.Rendering;

namespace Characters {
    public class CharacterAnimOverrider : MonoBehaviour {
        protected AnimatorOverrideController OverrideController;
        protected Animator CharacterAnimator;
        
        protected readonly string _combosString = "Combo_";
        
        protected void Awake() {
            CharacterAnimator = GetComponent<Animator>(); 
            OverrideController = new AnimatorOverrideController(CharacterAnimator.runtimeAnimatorController);
            OverrideController.name = CharacterAnimator.runtimeAnimatorController.name;
        }
    }
}