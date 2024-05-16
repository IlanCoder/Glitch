using System;
using System.Collections.Generic;
using Items.Weapons;
using Items.Weapons.MeleeWeapons;
using Unity.Collections;
using UnityEngine;
using WorldManager;

namespace Characters.Player {
    public class PlayerInventoryController : MonoBehaviour {
        PlayerManager _manager;

        [SerializeField] Transform weaponsParent;
        [Header("Inventory Objs List")]
        [SerializeField] List<BasicMeleeWeapon> _meleeWeapons = new List<BasicMeleeWeapon>();
        public Dictionary<int, GameObject> MainWeaponsInventory { get; protected set; } = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> OffhandWeaponsInventory { get; protected set; }= new Dictionary<int, GameObject>();

        void Awake() {
            _manager = GetComponent<PlayerManager>();
        }

        void InstantiateAllWeapons() {
            foreach (BasicMeleeWeapon weapon in _meleeWeapons) {
                GameObject tempWeapon = Instantiate(weapon.WeaponPrefab, weaponsParent);
                tempWeapon.SetActive(false);
                MainWeaponsInventory.TryAdd(weapon.ItemID, tempWeapon);
                if (!weapon.DualWield) continue;
                tempWeapon = Instantiate(weapon.OffhandPrefab, weaponsParent);
                tempWeapon.SetActive(false);
                OffhandWeaponsInventory.TryAdd(weapon.ItemID, tempWeapon);
            }
        }

        void EquipWeapon(int index, BasicWeapon weapon) {
            if (weapon == null) return;
            if (!weapon.DualWield) {
                _manager.equipmentManager.EquipWeapon(index, weapon, MainWeaponsInventory[weapon.ItemID]);
                return;
            }
            _manager.equipmentManager.EquipWeapon(index, weapon, MainWeaponsInventory[weapon.ItemID],
                OffhandWeaponsInventory[weapon.ItemID]);
        }

        public void UnequipWeapon(BasicWeapon weapon) {
            MainWeaponsInventory[weapon.ItemID].SetActive(false);
            MainWeaponsInventory[weapon.ItemID].transform.SetParent(weaponsParent, false);
            if (!weapon.DualWield) return;
            OffhandWeaponsInventory[weapon.ItemID].SetActive(false);
            OffhandWeaponsInventory[weapon.ItemID].transform.SetParent(weaponsParent, false);
        }

         #region Editor Funcs
#if UNITY_EDITOR
        [ContextMenu("Set Test Weapons")]
        void SetNewLevel() {
            LoadWeaponsIntoInventory();
            EquipWeapons();
        }
        
        void EquipWeapons() {
            EquipWeapon(0, _meleeWeapons[0]);
            EquipWeapon(1, _meleeWeapons[1]);
            EquipWeapon(2, _meleeWeapons[2]);
        }

        void LoadWeaponsIntoInventory() {
            _meleeWeapons.Add(WorldItemsManager.Instance.GetMeleeWeapon(0, transform));
            _meleeWeapons.Add(WorldItemsManager.Instance.GetMeleeWeapon(1, transform));
            _meleeWeapons.Add(WorldItemsManager.Instance.GetMeleeWeapon(2, transform));
            InstantiateAllWeapons();
        }
#endif
  #endregion
    }
}
