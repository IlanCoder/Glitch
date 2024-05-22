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

		public void StartChasing() {
			NavAgent.speed = runningSpeed;
			EnableNavMeshAgent();
		}

		public void EnableNavMeshAgent(bool enable = true) {
			if (NavAgent.enabled == enable) return;
			NavAgent.enabled = enable;
		}

		public void SetNavMeshDestination(Vector3 targetPos) {
			NavAgent.SetDestination(targetPos);
			NavAgent.nextPosition = transform.position;
		}

		public bool HasArrivedToLockOnRange() {
			return NavAgent.remainingDistance <= NavAgent.stoppingDistance;
		}
	}
}
