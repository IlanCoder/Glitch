using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent)),
	 RequireComponent(typeof(NpcMovementController)),
	 RequireComponent(typeof(NpcCombatController)),
	 RequireComponent(typeof(NpcAnimController)),
	 RequireComponent(typeof(NpcAnimOverrider)),
	 RequireComponent(typeof(NpcAgroController)),
	 RequireComponent(typeof(NpcEquipmentManager)),]
	public class NpcManager : CharacterManager {
		[HideInInspector] public NpcMovementController movementController;
		[HideInInspector] public NpcCombatController combatController;
		[HideInInspector] public NpcAnimController animController;
		[HideInInspector] public NpcAnimOverrider animOverrider;
		[HideInInspector] public NpcAgroController agroController;
		[HideInInspector] public NpcEquipmentManager equipmentManager;

		public override CharacterMovementController MovementController => movementController;
		public override CharacterCombatController CombatController => combatController;
		public override CharacterAnimController AnimController => animController;
		public override CharacterEquipmentManager EquipmentManager => equipmentManager;

		protected override void Awake() {
			base.Awake();
			movementController = GetComponent<NpcMovementController>();
			combatController = GetComponent<NpcCombatController>();
			animController = GetComponent<NpcAnimController>();
			animOverrider = GetComponent<NpcAnimOverrider>();
			agroController = GetComponent<NpcAgroController>();
			equipmentManager = GetComponent<NpcEquipmentManager>();
		}
	}
}
