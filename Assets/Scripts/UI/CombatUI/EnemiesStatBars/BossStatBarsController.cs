using System;
using Characters;
using TMPro;
using UnityEngine;

namespace UI.CombatUI.EnemiesStatBars {
    public class BossStatBarsController : NpcStatBarsController {

        [Header("Texts")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI damageText;

        [Header("Damage Text Settings")]
        [SerializeField] float showDamageLength;

        int _damage;
        float _textAppearTime;

        protected void Start() {
            DisableDamageText();
            gameObject.SetActive(false);
        }

        public void TickDamageTextTimer() {
            if (!damageText.gameObject.activeSelf) return;
            if (Time.time - _textAppearTime < showDamageLength) return;
            DisableDamageText();
        }

        void DisableDamageText() {
            _damage = 0;
            damageText.gameObject.SetActive(false);
        }

        override public void TieNewCharacter(CharacterManager newTarget, bool inCombat = false, int damageReceived = 0) {
            base.TieNewCharacter(newTarget, inCombat, damageReceived);
            nameText.text = newTarget.name;
        }

        public void IncreaseDamageText(int damage) {
            _damage += damage;
            _textAppearTime = Time.time;
            damageText.text = _damage.ToString();
            if(!damageText.gameObject.activeSelf) damageText.gameObject.SetActive(true);
        }

        protected void OnDisable() {
            DisableDamageText();
        }
    }
}