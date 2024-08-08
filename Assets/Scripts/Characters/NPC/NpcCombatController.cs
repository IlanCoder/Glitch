using System.Collections.Generic;
using Attacks.NPC;
using UnityEngine;

namespace Characters.NPC {
    public class NpcCombatController : CharacterCombatController {
        protected NpcManager Npc;

        [Header("Combat Speeds")]
        [SerializeField] protected float attackRotationTrackingSpeed = 10;

        [SerializeField] protected List<NpcAttack> attacks;

        protected override void Awake() {
            base.Awake();
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
        
        public bool TryGetAvailableAttacks(CharacterManager target, NpcAttack attackToIgnore, out List<NpcAttack> tempList) {
            tempList = new List<NpcAttack>();
            int listWeight = 0;
            foreach (NpcAttack attack in attacks) {
                if (attack == attackToIgnore) continue;
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

        public Queue<NpcAttack> RollForAttackChain(NpcAttack firstAttack) {
            Queue<NpcAttack> tempQueue = new Queue<NpcAttack>();
            NpcAttack selectedAttack = firstAttack;
            tempQueue.Enqueue(selectedAttack);
            while (selectedAttack.TryRollForChain(out selectedAttack)) {
                tempQueue.Enqueue(selectedAttack);
            }
            return tempQueue;
        }

        public void HandleAttackAnimation(NpcAttack attack, bool firstInChain = true) {
            Npc.animOverrider.OverrideNextAttack(attack, firstInChain);
            Npc.animController.PlayAttackAnimation(firstInChain);
            CurrentAttack = attack;
        }

        public void HandleAttackRotationTracking(CharacterManager target) {
            if (Npc.rotationLocked) return;
            Vector3 targetDir = target.transform.position - transform.position;
            targetDir.y = 0;
            targetDir.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                attackRotationTrackingSpeed * Time.fixedDeltaTime);
        }
        
        #region Animation Events
        public void EnableRotationTracking() {
            Npc.rotationLocked = false;
        }
        
        public void DisableRotationTracking() {
            Npc.rotationLocked = true;
        }
        #endregion
    }
}