using Characters;
using TMPro;
using UnityEngine;

namespace UI.CombatUI.EnemiesStatBars {
    public class BossStatBarsController : NpcStatBarsController {

        [Header("Texts")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI damageText;
        
        protected void Start() {
            gameObject.SetActive(false);
        }

        override public void TieNewCharacter(CharacterManager newTarget, bool inCombat = false, int damageReceived = 0) {
            base.TieNewCharacter(newTarget, inCombat, damageReceived);
            nameText.text = newTarget.name;
        }
    }
}