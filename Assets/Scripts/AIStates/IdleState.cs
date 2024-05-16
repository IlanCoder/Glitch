using System;
using Characters;
using Characters.NPC;
using UnityEngine;

namespace AIStates {
    public class IdleState : AIState {
        override public void EnterState(NpcManager manager) {
            base.EnterState(manager);
            Manager.AnimManager.UpdateMovementParameters(0, 0);
        }

        override public AIState Tick() {
            if (CheckForEnemy()) return null;
            return this;
        }

        protected virtual bool CheckForEnemy() {
            if (!Manager.combatManager.CheckLineSightRadius(out CharacterManager target)) return false;
            Manager.LockOn(target);
            return true;
        }
    }
}