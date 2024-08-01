using System;
using Enums;
using UnityEngine;

namespace Attacks.Player{
	[CreateAssetMenu(fileName = "SingleAttack",menuName = "Attacks/Player/Single Attack")]
	public class PlayerAttack : CharacterAttack {
		[Header("Player Specific Parameters")]
		
		[SerializeField, Min(0)] protected float staminaCostMultiplier;
		
		public float StaminaCostMultiplier => staminaCostMultiplier;

	}
}
