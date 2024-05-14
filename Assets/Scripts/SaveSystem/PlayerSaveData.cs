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

        #region Player Attributes
        public int Vitality;
        public int Endurance;
        public int Dexterity;
        public int Strength;
        public int Cyber;
        public int Control;
#endregion
        
        #region Player Stats
        public int CurrentHp;
        public float CurrentStamina;
        public float CurrentEnergy;
  #endregion
    }
}
