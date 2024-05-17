using UnityEngine;

namespace Characters.NPC{
	public class NpcMovementController : CharacterMovementController<NpcManager> {

		public void GoIdle() {
			manager.AnimController.UpdateMovementParameters(0, 0);
		}
	}
}
