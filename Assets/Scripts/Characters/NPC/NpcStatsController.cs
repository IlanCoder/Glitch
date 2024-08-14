using DataScriptables;
using UnityEngine;
using WorldManager;

namespace Characters.NPC {
    public class NpcStatsController : CharacterStatsController {
        [Header("Main Stats")]
        [SerializeField] protected NpcStats stats;
        public NpcStats Stats => stats;

        protected override void Awake() {
            base.Awake();
            characterName = stats.CharacterName;
            
            SetMaxHp(stats.MaxHp);
            CurrentHp = stats.MaxHp;

            SetMaxEnergy(stats.MaxEnergy);
            manager.CombatController.OverrideTeam(stats.Team);
        }

        override public void ReceiveDamage(int dmgReceived) {
            base.ReceiveDamage(dmgReceived);
            WorldCombatManager.Instance.onNpcHit?.Invoke(manager, dmgReceived);
        }
    }
}