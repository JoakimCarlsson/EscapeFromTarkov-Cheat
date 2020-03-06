using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Visual;
using UnityEngine;

namespace EscapeFromTarkovCheat.Utils
{
    class Settings
    {
        internal static bool DrawLootItems = false;
        internal static bool DrawLootableContainers = false;

        internal static bool DrawExfiltrationPoints = true;

        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = false;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool DrawPlayerHealth = true;
        internal static bool DrawPlayerBox = true;

        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 2000f;
        internal static float DrawPlayersDistance = 2000f;
    }
}