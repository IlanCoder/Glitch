using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TitleScreen {
    public class UISaveSlotsView : MonoBehaviour {

        GameObject _currentSelected;
        GameObject _prevSelected;

        [SerializeField] RectTransform contentPanel;
        [SerializeField] ScrollRect scrollRect;

        void Update() {
            _currentSelected = EventSystem.current.currentSelectedGameObject;
            if (_currentSelected == null) return;
            if (_currentSelected == _prevSelected) return;
            _prevSelected = _currentSelected;
            SnapTo(_currentSelected.GetComponent<RectTransform>());
        }

        void SnapTo(RectTransform target) {
            Canvas.ForceUpdateCanvases();
            Vector2 newPos = scrollRect.transform.InverseTransformPoint(contentPanel.position) -
                             scrollRect.transform.InverseTransformPoint(target.position);
            newPos.x = 0;
            contentPanel.anchoredPosition = newPos;
        }
    }
}
