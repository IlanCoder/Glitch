﻿using Characters;
using Characters.NPC;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes.Leaves.Movement {
    public class FindChaseTargetNode : LeafNode {
        CharacterManager _target;
        
        protected override void InitializeNode() {
            _target = null;
        }

        protected override NodeStatus Tick() {
            Character.combatController.CheckLineSightRadius(out _target);
            return _target is null ? NodeStatus.Failed : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            Character.movementController.chaseTarget = _target;
        }
    }
}