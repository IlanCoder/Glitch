using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace UI.HUD.GamePopUps {
    [RequireComponent(typeof(CanvasGroup))]
    public class DeathPopUpAnimator : MonoBehaviour {
        [Header("Animation Params")]
        [SerializeField] float fadeInTime;
        [SerializeField] float fadeOutDelay;
        [SerializeField] float targetAlpha;
        [SerializeField] float textSpacing;
        
        [Header("Objects")]
        [SerializeField] TextMeshProUGUI text;
        CanvasGroup _canvasGroup;

        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        void OnEnable() {
            _canvasGroup.alpha = 0;
            text.characterSpacing = 0;
            FadeIn();
        }

        void FadeIn() {
            _canvasGroup.DOFade(targetAlpha, fadeInTime);
            DOTween.To(() => text.characterSpacing, x => text.characterSpacing = x, 
            textSpacing, fadeInTime).OnComplete(FadeOut);
        }
        
        void FadeOut() {
            Sequence _sequence = DOTween.Sequence();
            _sequence.AppendInterval(fadeOutDelay);
            _sequence.Append(_canvasGroup.DOFade(0, fadeInTime));
        }
    }
}
