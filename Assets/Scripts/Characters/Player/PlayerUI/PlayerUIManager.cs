using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player.PlayerUI {
    public class PlayerUIManager : MonoBehaviour {
        [HideInInspector]public HUDManager hudManager;

        void Awake() {
            hudManager = GetComponentInChildren<HUDManager>();
        }
    }
}
