using System.Collections.Generic;
using Characters;
using Characters.NPC;
using Enums;
using UnityEngine;

namespace Attacks.NPC {
    [CreateAssetMenu(fileName = "SingleAttack",menuName = "AI/Attacks/Single Attack")]
    public class NpcAttack : ScriptableObject {
        [SerializeField] protected AnimationClip attackAnimation;
        public AnimationClip AttackAnimation => attackAnimation;
        
        [Header("Attack Parameters")]
        [SerializeField] protected AttackType attackType;
        [SerializeField] protected float downTime;
        public float DownTime => downTime;
        [SerializeField, Min(1)] protected int attackWeight = 1;
        public int AttackWeight => attackWeight;
        [HideInInspector] public int listedRollWeight;

        [Header("Attack Requirements")]
        [SerializeField] protected float minAngle = -35f;
        [SerializeField] protected float maxAngle = 35f;
        [SerializeField] protected float minDistance = 0;
        [SerializeField] protected float maxDistance = 2;

        [Header("Attack Chain")]
        [SerializeField] protected List<NpcAttack> possibleChains;
        [SerializeField] protected float chainChance;
        int _totalChainsWeight;

        public void InstantiateAttack() {
            List<NpcAttack> tempChains = new List<NpcAttack>();
            foreach (NpcAttack chain in possibleChains) {
                NpcAttack temp = Instantiate(chain);
                temp.InstantiateAttack();
                tempChains.Add(temp);
            }
            WeightPossibleChains();
            possibleChains = tempChains;
        }

        void WeightPossibleChains() {
            _totalChainsWeight = 0;
            foreach (NpcAttack chain in possibleChains) {
                _totalChainsWeight += chain.AttackWeight;
                chain.listedRollWeight = _totalChainsWeight;
            }
        }
        
        public virtual bool RequirementsMet(NpcManager attacker, CharacterManager target) {
            float distance = Vector3.Distance(attacker.transform.position, target.transform.position);
            if (distance < minDistance) return false;
            if (distance > maxDistance) return false;
            float angle = Vector3.SignedAngle(attacker.transform.position, target.transform.position, Vector3.up);
            if (angle < minAngle) return false;
            if (angle > maxAngle) return false;
            return true;
        }

        public bool TryRollForChain(out NpcAttack chainedAttack) {
            chainedAttack = null;
            if (chainChance >= Random.Range(0, 100)) return false;
            chainedAttack = RollChainWeights();
            return true;
        }

        NpcAttack RollChainWeights() {
            int randomWeight = Random.Range(0, _totalChainsWeight);
            foreach (NpcAttack chain in possibleChains) {
                if (randomWeight >= chain.listedRollWeight) continue;
                return chain;
            }
            return null;
        }
    }
}