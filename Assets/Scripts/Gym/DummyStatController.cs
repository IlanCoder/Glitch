using System;
using Characters;
using UnityEngine;

namespace Gym{
	public class DummyStatController : CharacterStatsController {
		[Header("Main Stats")]
		[SerializeField] int maxHp;
		
		protected override void Awake() {
			base.Awake();
			SetMaxHp(maxHp);
			CurrentHp = maxHp;
		}
	}
}
