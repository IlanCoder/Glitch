using System;
using UnityEngine;

namespace Characters.Player {
    public class PlayerStatManager : CharacterStatManager<PlayerManager> {
        [Header("Main Attributes")] 
        [SerializeField,Range(1, 99)] protected int vitality = 1;
        [SerializeField,Range(1, 99)] protected int endurance = 1;
        [SerializeField,Range(1, 99)] protected int dexterity = 1;
        [SerializeField,Range(1, 99)] protected int strength = 1;
        [SerializeField,Range(1, 99)] protected int cyber = 1;
        [SerializeField,Range(1, 99)] protected int control = 1;

        void Start() {
            SetMaxStamina(GetStaminaBasedOnLevel());
        }

        int GetStaminaBasedOnLevel() {
            return endurance * 10;
        }
    }
}
