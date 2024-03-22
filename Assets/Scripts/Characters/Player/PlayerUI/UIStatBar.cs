using System;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player.PlayerUI {
    public class UIStatBar : MonoBehaviour {
        Slider _slider;
        RectTransform _transform;
        
        [Header("Bar Options")]
        [SerializeField] bool scaleBarLengthWithStats = true;
        [SerializeField] float widthScaleMultiplier = 1;
        protected void Awake() {
            _slider = GetComponent<Slider>();
            _transform = GetComponent<RectTransform>();
        }

        public void SetStat(float val) {
            _slider.value = val;
        }

        public void SetMaxStat(float maxVal) {
            _slider.maxValue = maxVal;
            if (!scaleBarLengthWithStats) return;
            _transform.sizeDelta = new Vector2(maxVal * widthScaleMultiplier, _transform.sizeDelta.y);
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
