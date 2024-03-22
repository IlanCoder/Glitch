using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;

namespace Characters.Player {
    public class PlayerStatsManager : CharacterStatsManager {
        [Header("Main Attributes")] 
        [SerializeField,Range(1, 99)] protected int vitality = 1;
        [SerializeField,Range(1, 99)] protected int endurance = 1;
        [SerializeField,Range(1, 99)] protected int dexterity = 1;
        [SerializeField,Range(1, 99)] protected int strength = 1;
        [SerializeField,Range(1, 99)] protected int cyber = 1;
        [SerializeField,Range(1, 99)] protected int control = 1;

        #region Attributes Gets
        public int Vitality { get { return vitality; } }
        public int Endurance { get { return endurance; } }
        public int Dexterity { get { return dexterity; } }
        public int Strength { get { return strength; } }
        public int Cyber { get { return cyber; } }
        public int Control { get { return control; } }
  #endregion

        void Start() {
            SetNewLevel();
        }

        void SetStatsBasedOnAttributes() {
            SetMaxStamina(SetStaminaBasedOnLevel());
            SetMaxHp(SetHPBasedOnLevel());
        }
        
        int SetStaminaBasedOnLevel() {
            return endurance switch {
                <= 15 => Mathf.FloorToInt(80 + 25 * (endurance - 1) / 14),
                <= 35 => Mathf.FloorToInt(105 + 25 * (endurance - 15) / 15),
                <= 60 => Mathf.FloorToInt(130 + 25 * (endurance - 30) / 20),
                _ => Mathf.FloorToInt(155 + 15 * (endurance - 59) / 49),
            };
        }

        int SetHPBasedOnLevel() {
            return vitality switch {
                <= 25 => Mathf.FloorToInt(300 + 500 * Mathf.Pow((vitality - 1) / 24f, 1.5f)),
                <= 40 => Mathf.FloorToInt(800 + 650 * Mathf.Pow((vitality - 25) / 15f, 1.1f)),
                <= 60 => Mathf.FloorToInt(1450 + 450 * (1 - Mathf.Pow(1 - (vitality - 40) / 20f, 1.2f))),
                _ => Mathf.FloorToInt(1900 + 200 * (1 - Mathf.Pow(1 - (vitality - 60) / 39f, 1.2f)))
            };
        }

        public void LoadCharacterAttributes(int vit, int end, int dex, int str, int cbr, int ctrl) {
            vitality = vit;
            endurance = end;
            dexterity = dex;
            strength = str;
            cyber = cbr;
            control = ctrl;
            SetStatsBasedOnAttributes();
        }

        public void LoadCurrentStats(int hp, float stamina) {
            LoadCurrentHp(hp);
            LoadCurrentStamina(stamina);
        }
        
        void LoadCurrentHp(int hp) {
            CurrentHp = hp >= MaxHp ? MaxHp : hp;
            onHpChange?.Invoke(CurrentHp);
        }

        void LoadCurrentStamina(float stamina) {
            CurrentStamina = stamina >= MaxStamina ? MaxStamina : stamina;
            onStaminaChange?.Invoke(CurrentStamina);
        }

        #region Editor Funcs
#if UNITY_EDITOR
        void OnValidate() {
            //SetNewLevel();
        }

        [ContextMenu("Set New Level")]
        void SetNewLevel() {
            SetStatsBasedOnAttributes();
            LoadCurrentStats(MaxHp, MaxStamina);
        }
#endif
  #endregion
    }
}
