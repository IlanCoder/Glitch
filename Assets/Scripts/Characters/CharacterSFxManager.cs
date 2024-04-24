using Audio;
using Enums;
using UnityEngine;
using WorldManager;

namespace Characters{
	public class CharacterSFxManager : MonoBehaviour {
		[SerializeField] protected DamageSFxType damageSFxType;

		[Header("Audio Source Controllers")]
		[SerializeField] AudioSourceController hitSFxController;

		public virtual void PlayDamageSFx() { 
			hitSFxController.PlayPitchedSFx(WorldSFxsManager.Instance.GetRandomDamageAudioClip(damageSFxType));
		}
	}
}
