using System;
using Enums;
using UnityEngine;

namespace WorldManager {
    public class WorldCombatManager : MonoBehaviour {
        public static WorldCombatManager Instance;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        public bool IsTargetEnemy(CombatTeam characterTeam, CombatTeam enemyTeam) {
            if (characterTeam == enemyTeam) return false;
            if (characterTeam == CombatTeam.GlobalAlly || enemyTeam == CombatTeam.GlobalAlly) return false;
            if (characterTeam == CombatTeam.GlobalEnemy || enemyTeam == CombatTeam.GlobalEnemy) return true;
            return characterTeam switch {
                CombatTeam.PlayerFriendly => IsPlayerEnemy(enemyTeam),
                CombatTeam.Family1 => IsFamily1Enemy(enemyTeam),
                CombatTeam.Family2 => IsFamily2Enemy(enemyTeam),
                CombatTeam.Family3 => IsFamily3Enemy(enemyTeam),
                CombatTeam.Family4 => IsFamily4Enemy(enemyTeam),
                CombatTeam.Family5 => IsFamily5Enemy(enemyTeam),
                _ => false
            };
        }

        bool IsPlayerEnemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.Family1 => true,
                CombatTeam.Family2 => true,
                CombatTeam.Family3 => true,
                CombatTeam.Family4 => true,
                CombatTeam.Family5 => true,
                _ => false
            };
        }
        
        bool IsFamily1Enemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.PlayerFriendly => true,
                CombatTeam.Family2 => true,
                CombatTeam.Family3 => true,
                CombatTeam.Family4 => true,
                CombatTeam.Family5 => true,
                _ => false
            };
        }
        
        bool IsFamily2Enemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.PlayerFriendly => true,
                CombatTeam.Family1 => true,
                CombatTeam.Family3 => true,
                CombatTeam.Family4 => true,
                CombatTeam.Family5 => true,
                _ => false
            };
        }
        
        bool IsFamily3Enemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.PlayerFriendly => true,
                CombatTeam.Family1 => true,
                CombatTeam.Family2 => true,
                CombatTeam.Family4 => true,
                CombatTeam.Family5 => true,
                _ => false
            };
        }
        
        bool IsFamily4Enemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.PlayerFriendly => true,
                CombatTeam.Family1 => true,
                CombatTeam.Family2 => true,
                CombatTeam.Family3 => true,
                CombatTeam.Family5 => true,
                _ => false
            };
        }
        
        bool IsFamily5Enemy(CombatTeam enemyTeam) {
            return enemyTeam switch {
                CombatTeam.PlayerFriendly => true,
                CombatTeam.Family1 => true,
                CombatTeam.Family2 => true,
                CombatTeam.Family3 => true,
                CombatTeam.Family4 => true,
                _ => false
            };
        }
    }
}