using System;

namespace Enums {
    [Flags, Serializable]
    public enum DamageTypes {
        Slash = 1 << 1,
        Strike = 1 << 2,
        Thrust = 1 << 3,
        Physical = Slash | Strike | Thrust,
        Photon = 1 << 4,
        Shock = 1 << 5,
        Plasma = 1 << 6,
    }
}