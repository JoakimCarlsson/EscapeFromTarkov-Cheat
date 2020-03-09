using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Interactive;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Data
{
    class GameExfiltrationPoint
    {
        public ExfiltrationPoint ExfiltrationPoint { get; }

        public Vector3 ScreenPosition => screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 screenPosition;

        public GameExfiltrationPoint(ExfiltrationPoint exfiltrationPoint)
        {
            if (exfiltrationPoint == null)
                throw new ArgumentNullException(nameof(exfiltrationPoint));

            ExfiltrationPoint = exfiltrationPoint;
            screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsExfiltrationPointValid(ExfiltrationPoint))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(ExfiltrationPoint.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Camera.main.transform.position, ExfiltrationPoint.transform.position);
        }
    }
}
