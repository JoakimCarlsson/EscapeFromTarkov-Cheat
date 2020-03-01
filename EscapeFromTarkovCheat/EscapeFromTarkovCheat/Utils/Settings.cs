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
        internal static bool DrawLootItems = true;
        internal static bool DrawLootableContainers = false;

        internal static bool DrawExfiltrationPoints = true;

        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = false;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool DrawPlayerHealth = true;
        internal static bool DrawPlayerBox = true;

        internal static float DrawLootItemsDistance = 2000f;
        internal static float DrawLootableContainersDistance = 2000f;
        internal static float DrawPlayersDistance = 2000f;

        internal static bool Aimbot = false;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;
        internal static bool AimbotDrawFov = false;
        internal static bool AimbotSmooth = true;
        internal static float AimbotSmoothValue = 1f;
        internal static float AimbotFOV = 20f;
        
        internal static bool NoRecoil = false;
        internal static bool NoSway = false;
        internal static bool DoorUnlocker = false;
    }
}