using System;
using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace WorldManager{
	[DisallowMultipleComponent]
	public class WorldVFxsManager : MonoBehaviour {
		public static WorldVFxsManager Instance;

		[SerializeField] Transform vFxsParent;
		const int DamageVFxsCopyCount = 5;
		
		[Header("Damage VFXs")]
		[SerializeField] GameObject bloodDamageVFx;

		List<GameObject> _bloodDamageVFxInstances = new List<GameObject>();

		void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
			LoadDamageVFxsLists();
		}

		void LoadDamageVFxsLists() {
			for (int i = 0; i < DamageVFxsCopyCount; i++) {
				_bloodDamageVFxInstances.Add(Instantiate(bloodDamageVFx, vFxsParent));
			}
		}

		public GameObject GetDamageVFxInstance(DamageVFxType vFxType) {
			GameObject vFx;
			switch (vFxType) {
				case DamageVFxType.Blood:
					if (TryGetVFxInstanceFromList(_bloodDamageVFxInstances, out vFx)) break;
					vFx = Instantiate(bloodDamageVFx, vFxsParent);
					_bloodDamageVFxInstances.Add(vFx);
					break;
				default: throw new ArgumentOutOfRangeException(nameof(vFxType), vFxType, null);
			}
			return vFx;
		}

		bool TryGetVFxInstanceFromList(List<GameObject> vFxList, out GameObject vFxInstance) {
			foreach (GameObject vFx in vFxList) {
				if(!vFx.TryGetComponent(out ParticleSystem particle)) continue;
				if(particle.isPlaying) continue;
				vFxInstance = vFx;
				return true;
			}
			vFxInstance = null;
			return false;
		}
	}
}
