using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace UI.HUD.GamePopUps {
    [RequireComponent(typeof(CanvasGroup))]
    public class DeathPopUpAnimator : MonoBehaviour {
        [Header("Animation Params")]
        [SerializeField] float fadeInTime;
        [SerializeField] float textSpacing;
        
        [Header("Objects")]
        [SerializeField] TextMeshProUGUI text;
        CanvasGroup _canvasGroup;

        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        void OnEnable() {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, fadeInTime);
            text.characterSpacing = 0;
            DOTween.To(() => text.characterSpacing, x => text.characterSpacing = x, 
                textSpacing, fadeInTime);
        }
    }
}
