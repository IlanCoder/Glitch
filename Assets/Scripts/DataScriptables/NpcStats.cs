using DataContainers;
using Enums;
using UnityEngine;

namespace DataScriptables {
    [CreateAssetMenu(fileName = "NpcStat",menuName = "Stats/Npc/Basic Npc")]
    public class NpcStats : CharacterStats {
        [Header("Npc Combat Stats")]
        [SerializeField, Min(0)] float energyPerSecond;
        [SerializeField] CombatTeam team;
        [SerializeField] DamageStats damage;
        [SerializeField] ArmorStats armor;

        public float EnergyPerSecond => energyPerSecond;
        public DamageStats Damage => damage;
        public CombatTeam Team => team;
        override public ArmorStats Armor => armor;
    }
}