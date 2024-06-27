using UnityEngine;

namespace Attacks.Player {
    [CreateAssetMenu(fileName = "BasicCombo",menuName = "Attacks/Attack Combo")]
    public class PlayerCombo : ScriptableObject {
        [SerializeField, Range(1, 5)] int comboLength = 1;
        public int ComboLength { get { return comboLength; } }
        [SerializeField] PlayerAttack[] comboAttacks = new PlayerAttack[1];
        
        public PlayerAttack[] ComboAttacks { get { return comboAttacks; } }

        public PlayerAttack GetAttackInfo(int index) {
            return comboAttacks[index];
        }

        public void InstantiateAttacks() {
            PlayerAttack[] tempAttacks = new PlayerAttack[comboAttacks.Length];
            for (int i = 0; i < comboAttacks.Length; i++) {
                tempAttacks[i] = Instantiate(comboAttacks[i]);
            }
            comboAttacks = tempAttacks;
        }

#if UNITY_EDITOR
        void OnValidate() {
            if(comboLength == comboAttacks.Length) return;
            PlayerAttack[] tempInfo = comboAttacks;
            comboAttacks = new PlayerAttack[comboLength];

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