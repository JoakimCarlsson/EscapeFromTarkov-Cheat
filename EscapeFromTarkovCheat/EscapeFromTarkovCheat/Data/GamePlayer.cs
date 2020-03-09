using System;
using EFT;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Data
{

    public class GamePlayer
    {

        public Player Player => player;

        public Vector3 ScreenPosition => screenPosition;

        public Vector3 HeadScreenPosition => headScreenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public bool IsAI { get; private set; }

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";

        private readonly Player player;

        private Vector3 screenPosition;
        private Vector3 headScreenPosition;

        public GamePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            this.player = player;
            screenPosition = default;
            headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            IsAI = true;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(player))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(player.Transform.position);

            if (player.PlayerBones != null)
                headScreenPosition = GameUtils.WorldPointToScreenPoint(player.PlayerBones.Head.position);

            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Camera.main.transform.position, player.Transform.position);

            if ((player.Profile != null) && (player.Profile.Info != null))
                IsAI = (player.Profile.Info.RegistrationDate <= 0);
        }

    }

}