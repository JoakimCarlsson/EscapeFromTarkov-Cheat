using System;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
{
    public class PlayerESP : MonoBehaviour
    {
        private IEnumerable<Player> _players;
        private Camera _camera;

        private void Start()
        {
            InvokeRepeating(nameof(GetPlayers), 10f, 10f);
        }

        private void OnGUI()
        {
            if (Settings.DrawPlayers)
            {
                DrawPlayerESP();
            }
        }

        public void DrawPlayerESP()
        {
            if (_players == null)
                return;

            foreach (var player in _players)
            {
                if (!player.isActiveAndEnabled)
                    continue;

                if (!player.IsVisible)
                    continue;

                if (player.PointOfView == EPointOfView.FirstPerson)
                    continue;

                Vector3 playerPosition = player.Transform.position;
                float distanceToObject = Vector3.Distance(_camera.transform.position, player.Transform.position);
                Vector3 boundingVector = _camera.WorldToScreenPoint(playerPosition);

                if (distanceToObject <= Settings.DrawPlayersDistance && boundingVector.z > 0.01)
                {
                    Color playerColor;

                    if (player.HealthController.IsAlive)
                        playerColor = Color.yellow;
                    else
                        playerColor = Color.white;

                    if (Settings.DrawPlayerSkeleton && player.ActiveHealthController.IsAlive)
                    {
                        var playerRightPalmVector = _camera.WorldToScreenPoint(player.PlayerBones.RightPalm.position);
                        var playerLeftPalmVector = _camera.WorldToScreenPoint(player.PlayerBones.LeftPalm.position);
                        var playerLeftShoulderVector = _camera.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position);
                        var playerRightShoulderVector = _camera.WorldToScreenPoint(player.PlayerBones.RightShoulder.position);
                        var playerNeckVector = _camera.WorldToScreenPoint(player.PlayerBones.Neck.position);
                        var playerCenterVector = _camera.WorldToScreenPoint(player.PlayerBones.Pelvis.position);
                        var playerRightFootVector =  _camera.WorldToScreenPoint(player.PlayerBones.KickingFoot.position);
                        var playerLeftFootVector= _camera.WorldToScreenPoint(GameUtils.GetBonePosByID(player, 18));
                        var playerLeftElbow  = _camera.WorldToScreenPoint(GameUtils.GetBonePosByID(player, 91));
                        var playerRightElbow = _camera.WorldToScreenPoint(GameUtils.GetBonePosByID(player, 112));
                        var playerLeftKnee = _camera.WorldToScreenPoint(GameUtils.GetBonePosByID(player, 17));
                        var playerRightKnee = _camera.WorldToScreenPoint(GameUtils.GetBonePosByID(player, 22));

                        Render.DrawLine(new Vector2(playerNeckVector.x, Screen.height - playerNeckVector.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerLeftShoulderVector.x, Screen.height - playerLeftShoulderVector.y), new Vector2(playerLeftElbow.x, Screen.height - playerLeftElbow.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerRightShoulderVector.x, Screen.height - playerRightShoulderVector.y), new Vector2(playerRightElbow.x, Screen.height - playerRightElbow.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerLeftElbow.x, Screen.height - playerLeftElbow.y), new Vector2(playerLeftPalmVector.x, Screen.height - playerLeftPalmVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerRightElbow.x, Screen.height - playerRightElbow.y), new Vector2(playerRightPalmVector.x, Screen.height - playerRightPalmVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerRightShoulderVector.x, Screen.height - playerRightShoulderVector.y), new Vector2(playerLeftShoulderVector.x, Screen.height - playerLeftShoulderVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerLeftKnee.x, Screen.height - playerLeftKnee.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerRightKnee.x, Screen.height - playerRightKnee.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerLeftKnee.x, Screen.height - playerLeftKnee.y), new Vector2(playerLeftFootVector.x, Screen.height - playerLeftFootVector.y), 1f, playerColor);
                        Render.DrawLine(new Vector2(playerRightKnee.x, Screen.height - playerRightKnee.y), new Vector2(playerRightFootVector.x, Screen.height - playerRightFootVector.y), 1f, playerColor);
                    }

                    var playerHeadVector = _camera.WorldToScreenPoint(player.PlayerBones.Head.position);

                    float boxVectorX = boundingVector.x;
                    float boxVectorY = playerHeadVector.y + 10f;
                    float boxHeight = Math.Abs(playerHeadVector.y - boundingVector.y) + 10f;
                    float boxWidth = boxHeight * 0.65f;

                    var playerName = player.AIData.IsAI ? "AI" : player.name;

                    string playerText;

                    if (player.HealthController.IsAlive && Settings.DrawPlayerName)
                        playerText = playerName;
                    else if (player.HealthController.IsAlive && !Settings.DrawPlayerName)
                        playerText = string.Empty;
                    else
                        playerText = "Dead";

                    string playerTextDraw = $"{playerText} [ {(int)distanceToObject} m]";

                    var playerTextVector = GUI.skin.GetStyle(playerText).CalcSize(new GUIContent(playerText));
                    Render.DrawString(new Vector2(boundingVector.x - playerTextVector.x / 2f, Screen.height - boxVectorY - 20f), playerTextDraw, playerColor);

                    if (Settings.DrawPlayerBox && player.ActiveHealthController.IsAlive)
                        Render.DrawBox(boxVectorX - boxWidth / 2f, Screen.height - boxVectorY, boxWidth, boxHeight, playerColor);
                }
            }
        }

        private void GetPlayers()
        {
            GameWorld world = Singleton<GameWorld>.Instance;

            if (world != null)
            {
                if (Settings.DrawPlayers)
                    _players = FindObjectsOfType<Player>();

                if (_camera == null)
                    _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            }
        }
    }
}