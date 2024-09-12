using UnityEngine;

namespace SaveSystem {
    public class PlayerSaveData {
        public string PlayerName = "Rando";
        public float TimePlayed;
        
        #region Player Position
        public float PlayerXPos;
        public float PlayerYPos;
        public float PlayerZPos;
  #endregion

        #region Player Max Stats
        public int MaxHp;
        public int MaxStamina;
        public int MaxEnergy;
#endregion
        
        #region Player Stats
        public int CurrentHp;
        public float CurrentStamina;
        public float CurrentEnergy;
  #endregion
    }
}
