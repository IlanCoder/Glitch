using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Attacks {
    [CreateAssetMenu(fileName = "BasicCombo",menuName = "Attacks/Attack Combo")]
    public class PlayerCombo : ScriptableObject {
        [SerializeField, Range(1, 5)] int comboLength = 1;
        public int ComboLength { get { return comboLength; } }
        [SerializeField] AttackInfo[] comboAttacks = new AttackInfo[1];
        public AttackInfo[] ComboAttacks { get { return comboAttacks; } }

        public AttackInfo GetAttackInfo(int index) {
            return comboAttacks[index];
        }

#if UNITY_EDITOR
        void OnValidate() {
            if(comboLength == comboAttacks.Length) return;
            AttackInfo[] tempInfo = comboAttacks;
            comboAttacks = new AttackInfo[comboLength];

            if (comboLength < tempInfo.Length) {
                for (int i = 0; i < comboLength; i++) {
                    comboAttacks[i] = tempInfo[i];
                }
                return;
            } 
            for (int i = 0; i < tempInfo.Length; i++) {
                comboAttacks[i] = tempInfo[i];
            }
        }
#endif
    }
}