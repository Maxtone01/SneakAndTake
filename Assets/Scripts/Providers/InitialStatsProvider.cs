using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClassSystem
{
    public class InitialStatsProvider
    {
        public PlayerStats GetPlayerStats(PlayerClass payerClass)
        {
            switch (payerClass)
            {
                case PlayerClass.Strongman:
                    return new PlayerStats(30, 30, 10);
                case PlayerClass.Sneaker:
                    return new PlayerStats(30, 15, 15);
                case PlayerClass.Scout:
                    return new PlayerStats(10, 15, 20);
                case PlayerClass.Trickster:
                    return new PlayerStats(25, 10, 30);
                default:
                    break;
            }
            return null;
        }
    }
}