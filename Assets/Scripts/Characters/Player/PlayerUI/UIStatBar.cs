using System;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player.PlayerUI {
    public class UIStatBar : MonoBehaviour {
        Slider _slider;

        protected void Awake() {
            _slider = GetComponent<Slider>();
        }

        public void SetStat(float val) {
            _slider.value = val;
        }

        public void SetMaxStat(float maxVal) {
            _slider.maxValue = maxVal;
        }
    }
}
