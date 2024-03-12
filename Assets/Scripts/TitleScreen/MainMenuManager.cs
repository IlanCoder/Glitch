using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WorldManager;

namespace TitleScreen {
    public class MainMenuManager : MonoBehaviour {
        [SerializeField] WorldSaveManager saveManager;
        
        [Header("Buttons")]
        [SerializeField] Button lastMainMenuSelectedButton;
        
        [Header("Pop Ups")]
        [SerializeField] GameObject errorPopUp;
        [SerializeField] Button errorButton;

        void OnEnable() {
            lastMainMenuSelectedButton.Select();
        }

        void OnDisable() {
            lastMainMenuSelectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        }
        
        public void StartNewGame() {
            if (saveManager.AttemptToCreateNewGame()) return;
            OpenErrorWindow();
        }
        
        void OpenErrorWindow() {
            lastMainMenuSelectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            errorPopUp.SetActive(true);
            errorButton.Select();
        }

        public void CloseErrorWindow() {
            errorPopUp.SetActive(false);
            lastMainMenuSelectedButton.Select();
        }
    }
}