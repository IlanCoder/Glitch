using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent))]
	public class NpcMovementController : CharacterMovementController {
		protected NavMeshAgent NavAgent;
		protected NpcManager Manager;

		[Header("Chase Settings")]
		[SerializeField] protected float pursueStoppingDistance;
		[SerializeField] protected float straightAheadAngle;
		public float PursueStoppingDistance => pursueStoppingDistance;
		
		[Header("In Combat Settings")]
		[SerializeField] protected float walkStoppingDistance;
		public float WalkStoppingDistance => walkStoppingDistance;

		protected override void Awake() {
			base.Awake();
			Manager = GetComponent<NpcManager>();
			NavAgent = GetComponent<NavMeshAgent>();
			NavAgent.stoppingDistance = pursueStoppingDistance;
			NavAgent.updatePosition = false;
		}
		
		public void GoIdle() {
			Manager.AnimController.UpdateMovementParameters(0, 0);
			EnableNavMeshAgent(false);
		}

		#region Chase
		public void StartChasing(float stoppingDistance) {
			EnableNavMeshAgent();
			NavAgent.stoppingDistance = stoppingDistance;
		}

		public void ChaseTarget(CharacterManager target, float speed = 1) {
			SetNavMeshDestination(target.transform.position);
			Manager.animController.UpdateMovementParameters(0, speed);
		}
		
		public void StopChasing() {
			EnableNavMeshAgent(false);
		}
		#endregion

		public void EnableNavMeshAgent(bool enable = true) {
			if (NavAgent.isStopped != enable) return;
			NavAgent.isStopped = !enable;
		}

		#region NavMesh Path
		public void CalculatePath(CharacterManager target) {
			SetNavMeshDestination(target.transform.position);
		}

		void SetNavMeshDestination(Vector3 targetPos) {
			NavAgent.SetDestination(targetPos);
			NavAgent.nextPosition = transform.position;
		}
		
		public bool IsPathComplete() {
			return NavAgent.pathStatus == NavMeshPathStatus.PathComplete;
		}

		public Vector3 GetNavAgentRotation() {
			return NavAgent.transform.forward;
		}
		
		public bool HasArrivedToStoppingDistance() {
			return NavAgent.remainingDistance <= NavAgent.stoppingDistance;
		}
		#endregion
		
		public bool IsDirectionStraightAhead(Vector3 direction) {
			Vector3 targetDirection = direction;
			targetDirection.y = 0;
			float angleToTarget = Vector3.Angle(transform.forward, targetDirection);
			return angleToTarget <= straightAheadAngle;
		}
		
		public void RotateTowardsDirection(Vector3 direction) {
			Vector3 targetDir = direction;
			targetDir.y = 0;
			float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
			angle /= Mathf.Abs(angle);
			Manager.animController.UpdateRotation((int)angle);
		}
	}
}
