﻿using System.Threading;
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
            CurrentHp = stats.MaxHp;
            CurrentPosture = stats.Posture;
            base.Awake();
        }

        protected override void Start() {
            Manager.CombatController.OverrideTeam(stats.Team);
            base.Start();
        }

        protected override void ReceiveDamage(int dmgReceived) {
            base.ReceiveDamage(dmgReceived);
            WorldCombatManager.Instance.onNpcHit?.Invoke(Manager, dmgReceived);
        }

        public void GainAgroEnergy(float deltaTime) {
            GainEnergy(stats.EnergyPerSecond * deltaTime);
        }
    }
}