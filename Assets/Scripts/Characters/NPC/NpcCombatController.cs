using UnityEngine;
using WorldManager;

namespace Characters.NPC {
    public class NpcCombatController : CharacterCombatController {
        protected NpcManager Npc;
        
        [Header("Lock On")]
        [SerializeField] LayerMask lockOnLayer;

        [Header("Detection")]
        [SerializeField] protected Transform eyes;
        [SerializeField] protected float lineSightRadius;
        [SerializeField] protected float lineSightAngle;
        [SerializeField] LayerMask visionObstructLayer;
        [SerializeField] protected float straightAheadAngle;

        protected override void Awake() {
            Npc = GetComponent<NpcManager>();
        }
        
        public bool CheckLineSightRadius(out CharacterManager target) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, lineSightRadius, lockOnLayer);
            foreach (Collider col in colliders) {
                if (!CanBeTargeted(col, out target)) continue;
                return true;
            }
            target = null;
            return false;
        }
        
        bool CanBeTargeted(Collider col, out CharacterManager target) {
            if (!col.TryGetComponent(out target)) return false;
            if (target.isDead) return false;
            if (target == Npc.combatController.LockOnTarget) return false;
            if (!WorldCombatManager.Instance.IsTargetEnemy(team, target.CombatController.Team)) return false;

            Vector3 targetDirection = target.transform.position - transform.position;
            float angleToTarget = Vector3.Angle(eyes.transform.forward, targetDirection);

            if (angleToTarget > lineSightAngle) return false;
            return !Physics.Linecast(eyes.transform.position, target.CombatController.LockOnPivot.position,
            visionObstructLayer);
        }

        public bool IsDirectionStraightAhead(Vector3 direction) {
            Vector3 targetDirection = direction;
            targetDirection.y = 0;
            float angleToTarget = Vector3.Angle(eyes.transform.forward, targetDirection);
            return angleToTarget <= straightAheadAngle;
        }

        #region Editor
        #if UNITY_EDITOR
        public float EditorLineSightRadius { get { return lineSightRadius;} set {lineSightRadius = value; } }
        public float EditorLineSightAngle { get { return lineSightAngle;} set {lineSightAngle = value; } }
        #endif
  #endregion
    }
}