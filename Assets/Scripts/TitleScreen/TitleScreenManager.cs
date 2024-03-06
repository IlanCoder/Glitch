using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WorldManager;

namespace TitleScreen {
    public class TitleScreenManager : MonoBehaviour {
        [SerializeField] EventSystem eventSystem;
        [SerializeField] WorldSaveManager saveManager;

        [Header("Screens")]
        [SerializeField] GameObject titleScreen;
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject loadMenu;

        [Header("Buttons")]
        [SerializeField] Button loadMenuDefaultSelectedButton;
        [SerializeField] Button lastMainMenuSelectedButton;
        
        [Header("Pop Ups")]
        [SerializeField] GameObject errorScreen;
        [SerializeField] Button errorButton;
        
        public void StartNewGame() {
            if (saveManager.AttemptToCreateNewGame()) return;
            OpenErrorWindow("No Character Slots Available");
        }

        public void CloseTitleScreen() {
            titleScreen.SetActive(false);
            mainMenu.SetActive(true);
            lastMainMenuSelectedButton.Select();
        }
        
        public void OpenLoadGameMenu() {
            lastMainMenuSelectedButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            mainMenu.SetActive(false);
            loadMenu.SetActive(true);
            loadMenuDefaultSelectedButton.Select();
        }
        
        public void CloseLoadGameMenu() {
            loadMenu.SetActive(false);
            mainMenu.SetActive(true);
            lastMainMenuSelectedButton.Select();
        }

        void OpenErrorWindow(string errorMessage) {
            lastMainMenuSelectedButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            errorScreen.SetActive(true);
            errorScreen.GetComponentInChildren<TextMeshProUGUI>().text = errorMessage;
            errorButton.Select();
        }

        public void CloseErrorWindow() {
            lastMainMenuSelectedButton.Select();
            errorScreen.SetActive(false);
        }
    }
}
