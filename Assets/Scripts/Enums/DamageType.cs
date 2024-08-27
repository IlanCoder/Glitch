using System;

namespace Enums {
    [Flags, Serializable]
    public enum DamageTypes {
        Physical = 1 << 1,
        Photon = 1 << 2,
        Shock = 1 << 3,
        Plasma = 1 << 4,
    }
}