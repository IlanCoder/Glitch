using System;
using Characters;
using Characters.Player;
using UnityEngine;

namespace UI.CombatUI {
    public class LockOnIconController : MonoBehaviour {
        [SerializeField] Camera mainCamera;
        [SerializeField] PlayerManager player;
        
        CharacterManager _targetCharacter;

        void Start() {
            SetControllerListeners();
            gameObject.SetActive(false);
        }

        void SetControllerListeners() {
            player.combatController.onLockOnTargetChange.AddListener(ChangeTarget);
        }

        void ChangeTarget(CharacterManager newTarget) {
            _targetCharacter = newTarget;
            if (!newTarget) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
        }

        void LateUpdate() {
            Vector3 screenPos =
                mainCamera.WorldToScreenPoint(_targetCharacter.CombatController.UILockOnPivotRef.position);
            if (screenPos.z > 0) transform.position = screenPos;
        }
    }
}