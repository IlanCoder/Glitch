using UnityEngine;

namespace WorldManager {
    [DisallowMultipleComponent]
    public class WorldCamerasManager : MonoBehaviour {
        public static WorldCamerasManager Instance;

        [SerializeField] Camera npcUICamera;
        public Camera NpcUICamera => npcUICamera;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }
    }
}