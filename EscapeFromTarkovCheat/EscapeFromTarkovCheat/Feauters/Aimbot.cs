using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters
{
    class Aimbot : MonoBehaviour
    {
        public void Update()
        {

            if ((Main.GameWorld != null))
            {
                if (Settings.NoRecoil)
                    NoRecoil();

                if (Settings.Aimbot)
                    Aimot();
            }
        }

        private void Aimot()
        {
            
        }
        private void NoRecoil()
        {
            if (Main.LocalPlayer == null)
                return;
            
            Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
        }
    }
}
