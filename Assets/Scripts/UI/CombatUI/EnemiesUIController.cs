using System.Collections.Generic;
using Characters;
using Characters.Player;
using UI.CombatUI.EnemiesStatBars;
using UnityEngine;
using WorldManager;

namespace UI.CombatUI {
    public class EnemiesUIController : MonoBehaviour {
        [SerializeField] PlayerManager player;
        [SerializeField] Camera mainCamera;

        [Header("Boss Settings")]
        [SerializeField] BossStatBarsController bossStatBars;
        
        [Header("Pool Settings")]
        [SerializeField] GameObject enemyStatBarsPrefab;
        [SerializeField] int initCopyCounts;
        List<GameObject> _statBarsPool = new List<GameObject>();

        Dictionary<CharacterManager, EnemyStatBarsController> _activeStatBars =
            new Dictionary<CharacterManager, EnemyStatBarsController>();

        EnemyStatBarsController _lockedOnController;

        void Awake() {
            for (int i = 0; i < initCopyCounts; i++) {
                InstantiateStatBarsCopy();
            }
        }

        void Start() {
            player.combatController.onLockOnTargetChange.AddListener(ChangeLockOnTarget);
            WorldCombatManager.Instance.onNpcHit.AddListener(TieHitCharacter);
            WorldCombatManager.Instance.onBossStarted.AddListener(TieBoss);
        }

        void LateUpdate() {
            CheckEnemyBarsToRemove();
            if (!bossStatBars.gameObject.activeSelf) return;
            if (!bossStatBars.IsStillVisible()) bossStatBars.FadeOut();
        }

        void CheckEnemyBarsToRemove() {
            Dictionary<CharacterManager, EnemyStatBarsController> barsToRemove =
            new Dictionary<CharacterManager, EnemyStatBarsController>();
            foreach (KeyValuePair<CharacterManager, EnemyStatBarsController> activeStatBar in _activeStatBars) {
                if (!activeStatBar.Value.IsStillVisible())
                    barsToRemove.Add(activeStatBar.Key, activeStatBar.Value);
            }
            foreach (KeyValuePair<CharacterManager, EnemyStatBarsController> removeTarget in barsToRemove) {
                if (!removeTarget.Value.IsStillVisible())
                    UntieStatBarsFromCharacter(removeTarget.Key);
            }
        }

        EnemyStatBarsController TieStatBarsToCharacter(CharacterManager target, bool inCombat = false,
            int initDamage = 0) {
            GameObject statBars = GetEnemyStatBarsObject();
            EnemyStatBarsController controller = statBars.GetComponent<EnemyStatBarsController>();
            controller.TieNewCharacter(target, inCombat, initDamage);
            statBars.SetActive(true);
            _activeStatBars.Add(target, controller);
            return controller;
        }

        void UntieStatBarsFromCharacter(CharacterManager target) {
            if (!_activeStatBars.TryGetValue(target, out EnemyStatBarsController barsController)) return;
            barsController.FadeOut();
            _activeStatBars.Remove(target);
        }

        GameObject GetEnemyStatBarsObject() {
            foreach (GameObject obj in _statBarsPool) {
                if (!obj.activeSelf) return obj;
            }
            return InstantiateStatBarsCopy();
        }

        GameObject InstantiateStatBarsCopy() {
            GameObject instance = Instantiate(enemyStatBarsPrefab, transform, false);
            instance.SetActive(false);
            instance.GetComponent<EnemyStatBarsController>().mainCamera = mainCamera;
            _statBarsPool.Add(instance);
            return instance;
        }

        void ChangeLockOnTarget(CharacterManager newTarget) {
            if (_lockedOnController) _lockedOnController.RemoveLockOn();
            _lockedOnController = null;
            if (!newTarget) return;
            if (bossStatBars.TiedCharacter == newTarget) return;
            if (!_activeStatBars.TryGetValue(newTarget, out EnemyStatBarsController barsController))
                barsController = TieStatBarsToCharacter(newTarget);
            barsController.LockOn();
            _lockedOnController = barsController;
        }

        void TieHitCharacter(CharacterManager character, int damageReceived) {
            if (bossStatBars.TiedCharacter == character) return;
            if (_activeStatBars.TryGetValue(character, out EnemyStatBarsController barsController))
                barsController.EnableCombat();
            else TieStatBarsToCharacter(character, true, damageReceived);
        }

        void TieBoss(CharacterManager boss) {
            if (_activeStatBars.ContainsKey(boss)) UntieStatBarsFromCharacter(boss);
            bossStatBars.TieNewCharacter(boss);
            bossStatBars.gameObject.SetActive(true);
        }
    }
}