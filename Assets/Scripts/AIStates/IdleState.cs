﻿using System;
using Characters;
using Characters.NPC;
using UnityEngine;

namespace AIStates {
    public class IdleState : AIState {
        override public void EnterState(NpcManager manager) {
            base.EnterState(manager);
            Manager.movementController.GoIdle();
        }

        override public AIState Tick() {
            if (CheckForEnemy(out CharacterManager target)) return new PursueState(target);
            return this;
        }
        
        override public void ExitState(){}

        protected virtual bool CheckForEnemy(out CharacterManager target) {
            return Manager.combatController.CheckLineSightRadius(out target);
        }
    }
}