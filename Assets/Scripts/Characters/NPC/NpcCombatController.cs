using System.Collections.Generic;
using Attacks.NPC;
using UnityEngine;

namespace Characters.NPC {
    public class NpcCombatController : CharacterCombatController {
        protected NpcManager Npc;

        [SerializeField] protected List<NpcAttack> attacks;

        protected override void Awake() {
            Npc = GetComponent<NpcManager>();
            InstantiateAttacks();
        }
        
        void InstantiateAttacks() {
            List<NpcAttack> tempAttacks = new List<NpcAttack>();
            foreach (NpcAttack attack in attacks) {
                NpcAttack temp = Instantiate(attack);
                temp.InstantiateAttack();
                tempAttacks.Add(temp);
            }
            attacks = tempAttacks;
        }

        public bool TryGetAvailableAttacks(CharacterManager target, out List<NpcAttack> tempList) {
            tempList = new List<NpcAttack>();
            int listWeight = 0;
            foreach (NpcAttack attack in attacks) {
                if(!attack.RequirementsMet(Npc, target)) continue;
                listWeight += attack.AttackWeight;
                attack.listedRollWeight = listWeight;
                tempList.Add(attack);
            }
            return tempList.Count > 0;
        }

        public NpcAttack RollForAttack(List<NpcAttack> attacksList) {
            int totalWeight = attacksList[^1].listedRollWeight;
            int randomWeight = Random.Range(0, totalWeight);
            foreach (NpcAttack attack in attacksList) {
                if (randomWeight >= attack.listedRollWeight) continue;
                return attack;
            }
            return null;
        }
    }
}