using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent))]
	public class NpcMovementController : CharacterMovementController {
		protected NavMeshAgent NavAgent;
		protected NpcManager Manager;

		[Header("Movement Speeds")]
		[SerializeField] protected float runningSpeed;
		
		[Header("Nav Mesh")]
		[SerializeField] protected float lockOnDistance;

		protected override void Awake() {
			base.Awake();
			Manager = GetComponent<NpcManager>();
			NavAgent = GetComponent<NavMeshAgent>();
			NavAgent.stoppingDistance = lockOnDistance;
			NavAgent.updatePosition = false;
		}
		
		public void GoIdle() {
			Manager.AnimController.UpdateMovementParameters(0, 0);
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
		#endregion

		public void EnableNavMeshAgent(bool enable = true) {
			if (NavAgent.enabled == enable) return;
			NavAgent.enabled = enable;
		}
		
		void SetNavMeshDestination(Vector3 targetPos) {
			NavAgent.SetDestination(targetPos);
			NavAgent.nextPosition = transform.position;
		}

		public bool HasArrivedToLockOnRange() {
			return NavAgent.remainingDistance <= NavAgent.stoppingDistance;
		}

		public bool IsPathComplete() {
			return NavAgent.pathStatus == NavMeshPathStatus.PathComplete;
		}

		public void RotateTowardsTarget(CharacterManager target) {
			Vector3 targetPos = target.transform.position - transform.position;
			targetPos.y = 0;
			float angle = Vector3.SignedAngle(transform.forward, targetPos, Vector3.up);
			angle /= Mathf.Abs(angle);
			Manager.animController.UpdateRotation((int)angle);
		}
	}
}
