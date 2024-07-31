using UnityEngine;

namespace Characters.NPC {
    public class NpcStatsController : CharacterStatsController {
        [Header("Main Stats")]
        [SerializeField] int maxHp;
        [SerializeField] int maxEnergy;
		
        protected override void Awake() {
            base.Awake();
            SetMaxHp(maxHp);
            CurrentHp = maxHp;
            SetMaxEnergy(maxEnergy);
        }
    }
}