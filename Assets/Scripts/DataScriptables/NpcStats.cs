using DataContainers;
using Enums;
using UnityEngine;

namespace DataScriptables {
    [CreateAssetMenu(fileName = "NpcStat",menuName = "Stats/Npc/Basic Npc")]
    public class NpcStats : ScriptableObject {
        [Header("Name")]
        [SerializeField] string characterName;
        public string CharacterName => characterName;

        [Header("Basic Stats")]
        [SerializeField, Min(1)] int maxHp;
        [SerializeField, Min(1)] int maxEnergy;
        public int MaxHp => maxHp;
        public int MaxEnergy => maxEnergy;

        [Header("Combat Stats")]
        [SerializeField, Min(0)] float energyPerSecond;
        [SerializeField] DamageStats damage;
        [SerializeField] CombatTeam team;

        public float EnergyPerSecond => energyPerSecond;
        public DamageStats Damage => damage;
        public CombatTeam Team => team;
    }
}