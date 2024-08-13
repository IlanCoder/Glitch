using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.UIObjects {
    public class UIStatBar : MonoBehaviour {
        Slider _slider;
        RectTransform _transform;
        
        [Header("Bar Options")]
        [SerializeField] bool scaleBarLengthWithStats = true;
        [SerializeField] float widthScaleMultiplier = 1;

        [Header("Decrease Effect Bar")]
        [SerializeField] UIDecreaseEffectBar decreaseEffect;

        protected void Awake() {
            _slider = GetComponent<Slider>();
            _transform = GetComponent<RectTransform>();
        }

        public void SetStat(float val) {
            float diff = _slider.value - val;
            float oldVal = _slider.value;
            _slider.value = val;
            if (diff <= _slider.maxValue / 100) return;
            if (decreaseEffect.gameObject.activeSelf) return;
            decreaseEffect.EnableDecreaseSlider(oldVal);
        }

        public void SetMaxStat(float maxVal) {
            if (maxVal < _slider.value) _slider.value = maxVal;
            _slider.maxValue = maxVal;
            if (decreaseEffect) decreaseEffect.SetMaxVal(maxVal, widthScaleMultiplier, scaleBarLengthWithStats);
            if (!scaleBarLengthWithStats) return;
            _transform.sizeDelta = new Vector2(maxVal * widthScaleMultiplier, _transform.sizeDelta.y);
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
