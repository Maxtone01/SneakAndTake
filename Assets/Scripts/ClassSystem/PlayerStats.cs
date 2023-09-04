using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClassSystem
{
    public class PlayerStats
    {
        public PlayerStats(float stamina, float strength, float inteligence)
        {
            Stamina = stamina;
            Strength = strength;
            Inteligence = inteligence;
        }

        public float Stamina { get; private set; }
        public float Strength { get; private set; }
        public float Inteligence { get; private set; }

    }
}