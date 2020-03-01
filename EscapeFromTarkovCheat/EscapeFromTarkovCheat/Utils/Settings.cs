using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTarkovCheat.Utils
{
    class Settings
    {
        internal static bool DrawLootItems = true;
        internal static bool DrawLootableContainers = false;

        internal static bool DrawPlayers = true;
        internal static bool DrawExfiltrationPoints = true;

        internal static bool DrawPlayerName = false;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool DrawPlayerHealth = true;
        internal static bool DrawPlayerBox = true;

        internal static float DrawLootItemsDistance = 2000f;
        internal static float DrawLootableContainersDistance = 2000f;
        internal static float DrawPlayersDistance = 2000f;

        internal static bool NoRecoil = false;
        internal static bool NoSway = false;
        internal static bool DoorUnlocker = false;
    }
}