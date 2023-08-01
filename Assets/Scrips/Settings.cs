using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips
{
    internal static class Settings
    {
        public static int countPlayersInTeam = 3;
        public static int speed = 1;
        public static int homeSkin = 0, awaySkin = 1;

        public static Vector3[] startPositionHome = new Vector3[]
        {
            new Vector3(-3.2f, 0, -1),
            new Vector3(0, 2, -1),
            new Vector3(0f, -2, -1),
            new Vector3(-3.2f, 3, -1),
            new Vector3(-3.2f, -3, -1),
            new Vector3(5, 0, -1)
        };

        public static Vector3[] startPositionAway = new Vector3[]
        {
            new Vector3(-6.8f, 0, -1),
            new Vector3(-10f, 2, -1),
            new Vector3(-10f, -2, -1),
            new Vector3(-6.8f, 3, -1),
            new Vector3(-6.8f, -3, -1),
            new Vector3(-15f, 0, -1),
        };
    }
}
