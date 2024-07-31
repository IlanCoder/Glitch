using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD {
    public class UIDecreaseEffectBar : MonoBehaviour {

        RectTransform _transform;
        Slider _slider;
        [SerializeField] Slider parentSlider;

        float decreasePerSec;
        float enabledTime;

        [Header("Bar Options")]
        [SerializeField, Tooltip("% per second")] float decreaseSpeed;
        [SerializeField] float decreaseDelay;

        void Awake() {
            _transform = GetComponent<RectTransform>();
            _slider = GetComponent<Slider>();
            gameObject.SetActive(false);
        }

        void LateUpdate() {
            if (Time.time - enabledTime < decreaseDelay) return;
            if (_slider.value <= parentSlider.value) {
                gameObject.SetActive(false);
                return;
            }
            float val = _slider.value - decreasePerSec * Time.deltaTime;
            _slider.value = val;
        }

        public void EnableDecreaseSlider(float initVal) {
            _slider.value = initVal;
            enabledTime = Time.time;
            gameObject.SetActive(true);
        }

        public void SetMaxVal(float maxVal, float widthScaleMultiplier, bool scale = true) {
            _slider.maxValue = maxVal;
            decreasePerSec = maxVal / 100 * decreaseSpeed;
            if (!scale) return;
            _transform.sizeDelta = new Vector2(maxVal * widthScaleMultiplier, _transform.sizeDelta.y);
        }
    }
}