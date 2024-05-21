using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent))]
	public class NpcMovementController : CharacterMovementController {
		protected NavMeshAgent NavAgent;
		protected NpcManager Manager;

		[Header("Nav Mesh")]
		[SerializeField] protected float lockOnDistance;
		[SerializeField] protected float walkingSpeed;

		protected override void Awake() {
			base.Awake();
			Manager = GetComponent<NpcManager>();
			NavAgent = GetComponent<NavMeshAgent>();
			NavAgent.stoppingDistance = lockOnDistance;
			NavAgent.speed = walkingSpeed;
		}

		override public void HandleGravity() {
			if (Manager.isGrounded != NavAgent.enabled) {
				Manager.characterController.enabled = !Manager.isGrounded;
				NavAgent.enabled = Manager.isGrounded;
			}
			if (!Manager.characterController.enabled) return;
			base.HandleGravity();
		}

		public void GoIdle() {
			Manager.AnimController.UpdateMovementParameters(0, 0);
		}

		public void EnableNavMeshAgent(bool enable = true) {
			if (NavAgent.enabled == enable) return;
			NavAgent.enabled = enable;
		}

		public void SetNavMeshDestination(Vector3 targetPos) {
			NavAgent.SetDestination(targetPos);
		}

		public bool HasArrivedToLockOnRange() {
			return NavAgent.remainingDistance <= NavAgent.stoppingDistance;
		}
	}
}
