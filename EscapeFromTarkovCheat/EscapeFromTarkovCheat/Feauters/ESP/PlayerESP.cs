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
                if (player == null)
                    continue;

                if (!player.IsVisible)
                    continue;

                if (player.PointOfView == EPointOfView.FirstPerson)
                    continue;

                Vector3 playerPosition = player.Transform.position;
                float distanceToObject = Vector3.Distance(Camera.main.transform.position, player.Transform.position);
                Vector3 boundingVector = Camera.main.WorldToScreenPoint(playerPosition);

                if (distanceToObject <= Settings.DrawPlayersDistance && boundingVector.z > 0.01)
                {
                    Color playerColor;

                    if (player.HealthController.IsAlive)
                        playerColor = Color.yellow;
                    else
                        playerColor = Color.white;

                    if (Settings.DrawPlayerSkeleton && player.ActiveHealthController.IsAlive)
                    {
                        #region Skeleton ESP

                        var playerRightPalmVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).z);
                        var playerLeftPalmVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).z);
                        var playerLeftShoulderVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).z);
                        var playerRightShoulderVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).z);
                        var playerNeckVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).z);
                        var playerCenterVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).z);
                        var playerRightFootVector = new Vector3(
                            Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).x,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).y,
                            Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).z);

                        var playerLeftFootVector = new Vector3(
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 18)).x,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 18)).y,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 18)).z
                        );
                        var playerLeftElbow = new Vector3(
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 91)).x,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 91)).y,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 91)).z
                        );
                        var playerRightElbow = new Vector3(
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 112)).x,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 112)).y,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 112)).z
                        );
                        var playerLeftKnee = new Vector3(
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 17)).x,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 17)).y,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 17)).z
                        );
                        var playerRightKnee = new Vector3(
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 22)).x,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 22)).y,
                            Camera.main.WorldToScreenPoint(Helpers.GetBonePosByID(player, 22)).z
                        );

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

                        #endregion
                    }

                    var playerHeadVector = new Vector3(
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).x,
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y,
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).z);

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
                    GUI.Label(new Rect(boundingVector.x - playerTextVector.x / 2f, Screen.height - boxVectorY - 20f, 300f, 50f), playerTextDraw);

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
                if (Settings.DrawLootableContainers)
                    _players = FindObjectsOfType<Player>();
            }
        }
    }
}