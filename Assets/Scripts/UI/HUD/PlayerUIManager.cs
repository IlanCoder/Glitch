using UI.HUD.GamePopUps;
using UnityEngine;

namespace UI.HUD {
    public class PlayerUIManager : MonoBehaviour {
        [HideInInspector] public HUDManager hudManager;
        [HideInInspector] public GamePopUpsManager gamePopUpsManager;
        void Awake() {
            hudManager = GetComponentInChildren<HUDManager>();
            gamePopUpsManager = GetComponentInChildren<GamePopUpsManager>();
        }
    }
}
