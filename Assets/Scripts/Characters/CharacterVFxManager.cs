using UnityEngine;
using Enums;
using WorldManager;

namespace Characters{
	public class CharacterVFxManager : MonoBehaviour {
		[SerializeField] DamageVFxType damageVFxType;
		
		public virtual void PlayDamageVFx(Vector3 contactPoint) {
			GameObject vFx = WorldVFxsManager.Instance.GetDamageVFxInstance(damageVFxType);
			vFx.transform.position = contactPoint;
			if(!vFx.TryGetComponent(out ParticleSystem particle)) return;
			particle.Play();
		}
	}
}
