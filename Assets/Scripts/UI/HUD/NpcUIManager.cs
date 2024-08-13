using Characters.NPC;
using UnityEngine;
using WorldManager;

namespace UI.HUD {
    public class NpcUIManager : UISliderManager{
        NpcManager _npc;
        Camera _cam;

        protected override void Awake() {
            _npc = manager.GetComponent<NpcManager>();
            base.Awake();
        }

        void Start() {
            _cam = WorldCamerasManager.Instance.NpcUICamera;
        }

        void LateUpdate() {
            transform.forward = _cam.transform.forward;
        }
    }
}