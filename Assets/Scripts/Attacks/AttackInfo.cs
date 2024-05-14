using Enums;
using UnityEngine;

namespace Attacks{
	[CreateAssetMenu(fileName = "SingleAttack",menuName = "Attacks/Single Attack")]
	public class AttackInfo : ScriptableObject {
		[SerializeField] protected AnimationClip attackAnimation;
		[SerializeField, Min(0)] protected float motionMultiplier;
		[SerializeField, Min(0)] protected float staminaCostMultiplier;
		[SerializeField, Min(0)] protected float energyGainMultiplier;
		[SerializeField] protected AttackType input;
		
		public AnimationClip AttackAnimation { get { return attackAnimation; } }
		public float MotionCostMultiplier { get { return motionMultiplier; } }
		public float StaminaCostMultiplier { get { return staminaCostMultiplier; } }
		public float EnergyGainMultiplier { get { return energyGainMultiplier; } }
		public AttackType Input { get { return input; } }
	}
}
