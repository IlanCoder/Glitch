using System;
using System.Collections.Generic;
using Items.Weapons;
using Items.Weapons.MeleeWeapons;
using Unity.Collections;
using UnityEngine;

namespace Characters.Player {
    public class PlayerInventoryManager : MonoBehaviour {
        PlayerManager _manager;

        [Header("Inventory Objs List")]
        [SerializeField] Transform weaponsParent;
        
        [Header("Inventory Scriptable List")]
        [SerializeField] List<BasicMeleeWeapon> _meleeWeapons = new List<BasicMeleeWeapon>();
        public Dictionary<int, GameObject> MainWeaponsInventory { get; protected set; } = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> OffhandWeaponsInventory { get; protected set; }= new Dictionary<int, GameObject>();

        void Awake() {
            _manager = GetComponent<PlayerManager>();
            InstantiateAllWeapons();
        }

        void Start() { }

        void InstantiateAllWeapons() {
            foreach (BasicMeleeWeapon weapon in _meleeWeapons) {
                GameObject tempWeapon = Instantiate(weapon.WeaponPrefab, weaponsParent);
                tempWeapon.SetActive(false);
                MainWeaponsInventory.TryAdd(weapon.ItemID,tempWeapon);
                if (!weapon.DualWield) continue;
                tempWeapon = Instantiate(weapon.OffhandPrefab, weaponsParent);
                tempWeapon.SetActive(false);
                OffhandWeaponsInventory.TryAdd(weapon.ItemID,tempWeapon);
            }
        }

        void EquipWeapon(BasicWeapon weapon) {
            if (!weapon.DualWield) {
                _manager.equipmentManager.EquipWeapon(weapon, MainWeaponsInventory[weapon.ItemID]);
                return;
            }
            _manager.equipmentManager.EquipWeapon(weapon, MainWeaponsInventory[weapon.ItemID],
                OffhandWeaponsInventory[weapon.ItemID]);
        }

        public void UnequipWeapon(BasicWeapon weapon) {
            MainWeaponsInventory[weapon.ItemID].SetActive(false);
            MainWeaponsInventory[weapon.ItemID].transform.SetParent(weaponsParent, false);
            if (!weapon.DualWield) return;
            OffhandWeaponsInventory[weapon.ItemID].SetActive(false);
            OffhandWeaponsInventory[weapon.ItemID].transform.SetParent(weaponsParent, false);
        }
    }
}
