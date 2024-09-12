using DataContainers;
using Enums;
using UnityEngine;

namespace DataScriptables {
    [CreateAssetMenu(fileName = "NpcStat",menuName = "Stats/Npc/Basic Npc")]
    public class NpcStats : CharacterStats {
        [Header("Combat Stats")]
        [SerializeField, Min(0)] float energyPerSecond;
        [SerializeField] DamageStats damage;
        [SerializeField] CombatTeam team;

        public float EnergyPerSecond => energyPerSecond;
        public DamageStats Damage => damage;
        public CombatTeam Team => team;
    }
}