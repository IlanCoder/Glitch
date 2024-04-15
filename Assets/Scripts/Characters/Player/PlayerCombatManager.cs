using System;

namespace Characters.Player{
	public class PlayerCombatManager : CharacterCombatManager {
		PlayerManager _manager;

		protected override void Awake() {
			_manager = GetComponent<PlayerManager>();
		}
	}
}
