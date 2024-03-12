using System;
using UnityEngine;

namespace Characters.Player {
    public class PlayerStatManager : CharacterStatManager<PlayerManager> {
        
        void Start() {
            SetMaxStamina(GetStaminaBasedOnLevel());
        }

        int GetStaminaBasedOnLevel() {
            return endurance * 10;
        }
    }
}
