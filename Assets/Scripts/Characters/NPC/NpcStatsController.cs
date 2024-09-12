using System.Threading;
using DataScriptables;
using UnityEngine;
using WorldManager;

namespace Characters.NPC {
    public class NpcStatsController : CharacterStatsController {
        [Header("Main Stats")]
        [SerializeField] protected NpcStats stats;
        public override CharacterStats CharacterStats => stats;
        public NpcStats Stats => stats;

        protected override void Awake() {
            stats = Instantiate(stats, transform);
            base.Awake();

            SetMaxHp(stats.MaxHp);
            CurrentHp = stats.MaxHp;

            SetMaxEnergy(stats.MaxEnergy);
        }

        protected override void Start() {
            Manager.CombatController.OverrideTeam(stats.Team);
            base.Start();
        }

        override public void ReceiveDamage(int dmgReceived) {
            base.ReceiveDamage(dmgReceived);
            WorldCombatManager.Instance.onNpcHit?.Invoke(Manager, dmgReceived);
        }

        public void GainAgroEnergy(float deltaTime) {
            GainEnergy(stats.EnergyPerSecond * deltaTime);
        }
    }
}