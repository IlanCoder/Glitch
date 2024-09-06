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
		[SerializeField] AudioSourceController sourceSFxController;

		public virtual void PlayDamageSFx() { 
			sourceSFxController.PlayPitchedSFx(WorldSFxsManager.Instance.GetRandomDamageAudioClip(damageSFxType));
		}

		public virtual void PlayAttackSwingSFx(AudioClip swingAudioClip) {
			if (!swingAudioClip) return;
			sourceSFxController.PlaySFx(swingAudioClip);
		}

		public virtual void PlayDeflectSFx(DeflectQuality deflectQuality) {
			if (deflectQuality == DeflectQuality.Miss) return;
			sourceSFxController.PlayPitchedSFx(WorldSFxsManager.Instance.GetDeflectAudioClip(deflectQuality));
		}
	}
}
