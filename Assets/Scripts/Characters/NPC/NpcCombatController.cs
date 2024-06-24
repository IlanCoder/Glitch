using UnityEngine;
using WorldManager;

namespace Characters.NPC {
    public class NpcCombatController : CharacterCombatController {
        protected NpcManager Npc;

        protected override void Awake() {
            Npc = GetComponent<NpcManager>();
        }
    }
}