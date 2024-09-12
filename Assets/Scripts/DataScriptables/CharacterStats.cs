using System;
using DataContainers;
using UnityEngine;

namespace DataScriptables {
    public abstract class CharacterStats : ScriptableObject {
        [Header("Name")]
        [SerializeField] protected string characterName;
        public string CharacterName {
            get { return characterName; }
            set { characterName = value; }
        }

        [Header("Basic Stats")]
        [SerializeField, Min(1)] protected int maxHp;
        [SerializeField, Min(1)] protected int maxEnergy;

        public int MaxHp {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int MaxEnergy {
            get { return maxEnergy; }
            set { maxEnergy = value; }
        }

        public virtual ArmorStats Armor => null;
    }
}