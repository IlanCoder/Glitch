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
        public List<NpcAttack> availableAttacks;
        public NpcAttack previousAttack;
        public NpcAttack currentAttack;
    }
}