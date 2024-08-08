using Attacks;
using Audio;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using WorldManager;

namespace Characters{
	public class CharacterSFxController : MonoBehaviour {
		[SerializeField] protected DamageSFxType damageSFxType;

		[Header("Audio Source Controllers")]
		[SerializeField] AudioSourceController characterSFxController;

		public virtual void PlayDamageSFx() { 
			characterSFxController.PlayPitchedSFx(WorldSFxsManager.Instance.GetRandomDamageAudioClip(damageSFxType));
		}

		public virtual void PlayAttackSwingSFx(AudioClip swingAudioClip) {
			if (!swingAudioClip) return;
			characterSFxController.PlaySFx(swingAudioClip);
		}
	}
}
