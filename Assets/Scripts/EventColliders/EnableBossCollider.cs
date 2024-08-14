using System;
using Characters;
using UnityEngine;
using WorldManager;

namespace EventColliders {
    [RequireComponent(typeof(Collider))]
    public class EnableBossCollider : MonoBehaviour {
        [SerializeField] CharacterManager boss;

        void Awake() {
            if (!GetComponent<Collider>().isTrigger) GetComponent<Collider>().isTrigger = true;
        }

        void OnTriggerEnter(Collider other) {
            if (boss.isDead) return;
            WorldCombatManager.Instance.onBossStarted?.Invoke(boss);
        }
    }
}