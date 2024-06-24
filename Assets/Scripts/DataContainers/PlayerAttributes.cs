using System;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public struct PlayerAttributes {
        [SerializeField, Range(0, 99)] public int vitality;
        [SerializeField, Range(0, 99)] public int endurance;
        [SerializeField, Range(0, 99)] public int dexterity;
        [SerializeField, Range(0, 99)] public int strength;
        [SerializeField, Range(0, 99)] public int cyber;
        [SerializeField, Range(0, 99)] public int control;
    }
}