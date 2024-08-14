namespace UI.CombatUI.EnemiesStatBars {
    public class BossStatBarsController : NpcStatBarsController {
        protected void Start() {
            gameObject.SetActive(false);
        }
    }
}