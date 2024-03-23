using UnityEngine;

namespace UI.TitleScreen {
    public class TitleScreenManager : MonoBehaviour {
        [Header("Screens")]
        [SerializeField] GameObject titleScreen;
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject loadMenu;
        
        public void CloseTitleScreen() {
            titleScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        public void OpenLoadGameMenu() {
            
            mainMenu.SetActive(false);
            loadMenu.SetActive(true);
        }
        
        public void CloseLoadGameMenu() {
            loadMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
