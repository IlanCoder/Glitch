using System;
using System.Collections.Generic;
using Attacks.NPC;
using Characters;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    [Serializable]
    public class BehaviorTreeBlackboard {
        [Header("Targets")]
        public CharacterManager targetCharacter;
        
        [Header("Attacks")]
        public NpcAttack previousAttack;
        public NpcAttack currentAttack;
        public Queue<NpcAttack> AttackChain = new Queue<NpcAttack>();

        [Header("Cancel Events")]
        public Dictionary<string, bool> CancelEvents = new Dictionary<string, bool>();
    }
}