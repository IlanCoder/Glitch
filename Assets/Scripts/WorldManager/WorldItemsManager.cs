using System.Collections.Generic;
using Items.Weapons.MeleeWeapons;
using UnityEngine;

namespace WorldManager{
	[DisallowMultipleComponent]
	public class WorldItemsManager : MonoBehaviour {
		public static WorldItemsManager Instance;

		[Header("MeleeWeapons")]
		[SerializeField] List<BasicMeleeWeapon> meleeWeapons;
		
		void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
			GenerateItemsIDs();
		}

		void GenerateItemsIDs() {
			GenerateMeleeItemsIDs();
		}

		void GenerateMeleeItemsIDs() {
			for (int i = 0; i < meleeWeapons.Count; i++) {
				meleeWeapons[i].SetItemID(i);
			}
		}
		
		public BasicMeleeWeapon GetMeleeWeapon(int index, Transform parent) {
			BasicMeleeWeapon weapon = Instantiate(meleeWeapons[index], parent);
			weapon.InitializeWeapon();
			return weapon;
		}
	}
}
