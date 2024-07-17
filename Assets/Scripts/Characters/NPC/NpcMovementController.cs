using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent))]
	public class NpcMovementController : CharacterMovementController {
		protected NavMeshAgent NavAgent;
		protected NpcManager Manager;

		[Header("Movement Speeds")]
		[SerializeField] protected float runningSpeed;
		
		[Header("Chase Settings")]
		[SerializeField] protected float stoppingDistance;
		[SerializeField] protected float straightAheadAngle;
		public float StoppingDistance => stoppingDistance;

		protected override void Awake() {
			base.Awake();
			Manager = GetComponent<NpcManager>();
			NavAgent = GetComponent<NavMeshAgent>();
			NavAgent.stoppingDistance = stoppingDistance;
			NavAgent.updatePosition = false;
		}
		
		public void GoIdle() {
			Manager.AnimController.UpdateMovementParameters(0, 0);
			EnableNavMeshAgent(false);
		}

		#region Chase
		public void StartChasing() {
			NavAgent.speed = runningSpeed;
			EnableNavMeshAgent();
		}

		public void ChaseTarget(CharacterManager target) {
			SetNavMeshDestination(target.transform.position);
			Manager.animController.UpdateMovementParameters(0, 1);
		}
		
		public bool IsDirectionStraightAhead(Vector3 direction) {
			Vector3 targetDirection = direction;
			targetDirection.y = 0;
			float angleToTarget = Vector3.Angle(transform.forward, targetDirection);
			return angleToTarget <= straightAheadAngle;
		}

		public void StopChasing() {
			Manager.animController.ResetMovementParameters();
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

		public Vector3 GetNextNavPosition() {
			return NavAgent.steeringTarget;
		}
		
		public bool HasArrivedToStoppingDistance() {
			return NavAgent.remainingDistance <= NavAgent.stoppingDistance;
		}
		#endregion
		
		public void RotateTowardsDirection(Vector3 direction) {
			Vector3 targetDir = direction;
			targetDir.y = 0;
			float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
			angle /= Mathf.Abs(angle);
			Manager.animController.UpdateRotation((int)angle);
		}
	}
}
