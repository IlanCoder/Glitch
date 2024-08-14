using System;
using Characters;
using UnityEngine;
using WorldManager;

namespace Gym{
	public class DummyStatController : CharacterStatsController {
		[Header("Main Stats")]
		[SerializeField] int maxHp;
		
		protected override void Awake() {
			base.Awake();
			
			SetMaxHp(maxHp);
			CurrentHp = maxHp;
		}
		
		override public void ReceiveDamage(int dmgReceived) {
			base.ReceiveDamage(dmgReceived);
			WorldCombatManager.Instance.onNpcHit?.Invoke(manager, dmgReceived);
		}
	}
}
