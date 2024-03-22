using System.Reflection;
using UnityEngine;

namespace Structs {
    public struct DamageTypes{
        public float SlashDmg;
        public float StrikeDmg;
        public float ThrustDmg;
        public float PhotonDmg;
        public float ShockDmg;
        public float PlasmaDmg;

        public void SetDamage(float slash, float strike, float thrust, float photon, float shock, float plasma) {
            SlashDmg = slash;
            StrikeDmg = strike;
            ThrustDmg = thrust;
            PhotonDmg = photon;
            ShockDmg = shock;
            PlasmaDmg = plasma;
        }
    }
}
