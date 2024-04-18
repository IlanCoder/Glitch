using System.Reflection;
using UnityEngine;

namespace Structs {
    public struct DamageValues{
        public float SlashDmg { get; private set; }
        public float StrikeDmg { get; private set; }
        public float ThrustDmg { get; private set; }
        public float PhotonDmg { get; private set; }
        public float ShockDmg { get; private set; }
        public float PlasmaDmg { get; private set; }
        public float TotalBaseDamage { get; private set; }
        public float TotalMultipliedDamage { get; private set; }

        public void SetDamage(float slash, float strike, float thrust, float photon, float shock, float plasma) {
            SlashDmg = slash;
            StrikeDmg = strike;
            ThrustDmg = thrust;
            PhotonDmg = photon;
            ShockDmg = shock;
            PlasmaDmg = plasma;
            TotalBaseDamage = SlashDmg + StrikeDmg + ThrustDmg + PhotonDmg + ShockDmg + PlasmaDmg;
        }

        public void SetMultipliedDamage(float motionMultiplier, float attackMultiplier) {
            TotalMultipliedDamage = TotalBaseDamage * motionMultiplier * attackMultiplier;
        }
    }
}
