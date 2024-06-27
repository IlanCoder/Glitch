using System;
using Enums;
using UnityEngine;

namespace Attacks.Player{
	[CreateAssetMenu(fileName = "SingleAttack",menuName = "Attacks/Single Attack")]
	public class PlayerAttack : ScriptableObject {
		[SerializeField] protected AnimationClip attackAnimation;
		[SerializeField, Min(0)] protected float motionMultiplier;
		[SerializeField, Min(0)] protected float staminaCostMultiplier;
		[SerializeField, Min(0)] protected float energyGainMultiplier;
		[SerializeField] protected AttackType input;
		
		public AnimationClip AttackAnimation => attackAnimation;
		public float MotionCostMultiplier => motionMultiplier;
		public float StaminaCostMultiplier => staminaCostMultiplier;
		public float EnergyGainMultiplier => energyGainMultiplier;
		public AttackType Input => input;
	}
}
