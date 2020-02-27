using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using UnityEngine;
using EFT;
using EFT.Interactive;
using Object = UnityEngine.Object;
using Diz.Skinning;

namespace EscapeFromTarkovCheat
{
    public class CheatBehaviour : MonoBehaviour
    {
        private IEnumerable<ExfiltrationPoint> _exfiltrationPoints;
        private IEnumerable<LootableContainer> _lootableContainers;
        private IEnumerable<LootItem> _lootItems;
        private IEnumerable<Player> _players;

        private bool _showLootItems = true;
        private bool _showExfiltrationPoints = true;
        private bool _showLootableContainer = true;
        private bool _showPlayers = true;
        private Player _localPlayer;

        public void Awake()
        {
            //Debug.unityLogger.logEnabled = false;
        }

        public void Start()
        {
            InvokeRepeating(nameof(Trainer), 10f, 10f);
        }

        public void Update()
        {

        }

        public void OnGUI()
        {
            if (_showLootItems)
            {
                DrawItemESP();
            }

            if (_showExfiltrationPoints)
            {
                DrawExfiltrationPoints();
            }

            if (_showLootableContainer)
            {
                DrawLootableContainers();
            }

            if (_showPlayers)
            {
                DrawPlayerESP();
            }
        }

        private void DrawPlayerESP()
        {
            //try
            //{
            if (_players == null)
                return;

            foreach (var player in _players)
            {
                if (player == null)
                    continue;

                if (!player.IsVisible)
                    continue;

                if (EPointOfView.FirstPerson == player.PointOfView)
                    continue;

                Vector3 playerPos = player.Transform.position;
                float distanceToObject = Vector3.Distance(Camera.main.transform.position, player.Transform.position);
                Vector3 boundingVector = Camera.main.WorldToScreenPoint(playerPos);

                if (distanceToObject <= 300f && boundingVector.z > 0.01)
                {
                    #region Bone Vectors

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
                    var playerHeadVector = new Vector3(
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).x,
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y,
                        Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).z);
                    var playerLeftFootVector = new Vector3(
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 18)).x,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 18)).y,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 18)).z
                        );
                    var playerLeftElbow = new Vector3(
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 91)).x,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 91)).y,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 91)).z
                        );
                    var playerRightElbow = new Vector3(
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 112)).x,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 112)).y,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 112)).z
                        );
                    var playerLeftKnee = new Vector3(
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 17)).x,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 17)).y,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 17)).z
                        );
                    var playerRightKnee = new Vector3(
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 22)).x,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 22)).y,
                        Camera.main.WorldToScreenPoint(GetBonePosByID(player, 22)).z
                        );

                    #endregion Bone Vectors

                    float boxVectorX = boundingVector.x;
                    float boxVectorY = playerHeadVector.y + 10f;
                    float boxHeight = Math.Abs(playerHeadVector.y - boundingVector.y) + 10f;
                    float boxWidth = boxHeight * 0.65f;
                    Color playerColor;

                    if (player.HealthController.IsAlive)
                        playerColor = Color.yellow;
                    else
                        playerColor = Color.white;

                    Utils.DrawBox(boxVectorX - boxWidth / 2f, Screen.height - boxVectorY, boxWidth, boxHeight, playerColor);
                    var playerName = player.AIData.IsAI ? "AI" : player.name;

                    string playerText;

                    if (player.HealthController.IsAlive)
                        playerText = playerName;
                    else
                        playerText = playerName + " (Dead)";

                    string playerTextDraw = $"{playerText} [{(int)distanceToObject}m]";
                    var playerTextVector = GUI.skin.GetStyle(playerText).CalcSize(new GUIContent(playerText));
                    GUI.Label(new Rect(boundingVector.x - playerTextVector.x / 2f, Screen.height - boxVectorY - 20f, 300f, 50f), playerTextDraw);



                    #region Draw Bones

                    Utils.DrawLine(new Vector2(playerNeckVector.x, Screen.height - playerNeckVector.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerLeftShoulderVector.x, Screen.height - playerLeftShoulderVector.y), new Vector2(playerLeftElbow.x, Screen.height - playerLeftElbow.y), playerColor);
                    Utils.DrawLine(new Vector2(playerRightShoulderVector.x, Screen.height - playerRightShoulderVector.y), new Vector2(playerRightElbow.x, Screen.height - playerRightElbow.y), playerColor);
                    Utils.DrawLine(new Vector2(playerLeftElbow.x, Screen.height - playerLeftElbow.y), new Vector2(playerLeftPalmVector.x, Screen.height - playerLeftPalmVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerRightElbow.x, Screen.height - playerRightElbow.y), new Vector2(playerRightPalmVector.x, Screen.height - playerRightPalmVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerRightShoulderVector.x, Screen.height - playerRightShoulderVector.y), new Vector2(playerLeftShoulderVector.x, Screen.height - playerLeftShoulderVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerLeftKnee.x, Screen.height - playerLeftKnee.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerRightKnee.x, Screen.height - playerRightKnee.y), new Vector2(playerCenterVector.x, Screen.height - playerCenterVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerLeftKnee.x, Screen.height - playerLeftKnee.y), new Vector2(playerLeftFootVector.x, Screen.height - playerLeftFootVector.y), playerColor);
                    Utils.DrawLine(new Vector2(playerRightKnee.x, Screen.height - playerRightKnee.y), new Vector2(playerRightFootVector.x, Screen.height - playerRightFootVector.y), playerColor);

                    #endregion Draw Bones
                }
            }
            //}
            //catch (Exception e)
            //{

            //}
        }

        public Vector3 GetBonePosByID(Player player, int id)
        {
            Vector3 result;
            try
            {
                result = SkeletonBonePos(player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint, id);
            }
            catch (Exception)
            {
                result = Vector3.zero;
            }
            return result;
        }

        public static Vector3 SkeletonBonePos(Skeleton skeleton, int id)
        {
            return skeleton.Bones.ElementAt(id).Value.position;
        }

        private Color GetPlayerColor(EPlayerSide side)
        {
            switch (side)
            {
                case EPlayerSide.Bear:
                    return Color.red;
                case EPlayerSide.Usec:
                    return Color.blue;
                case EPlayerSide.Savage:
                    return Color.white;
                default:
                    return Color.white;
            }
        }

        private void DrawExfiltrationPoints()
        {
            if (_exfiltrationPoints == null)
                return;

            foreach (var exfiltrationPoint in _exfiltrationPoints)
            {
                if (exfiltrationPoint == null)
                    continue;
                var boundingVector = Camera.main.WorldToScreenPoint(exfiltrationPoint.transform.position);
                if (boundingVector.z > 0.01)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, exfiltrationPoint.transform.position);
                    GUI.color = Color.green;
                    string boxText = $"{ExtractionNameToSimpleName(exfiltrationPoint.name)} - {(int)distanceToObject}m";
                    GUI.Label(new Rect(boundingVector.x - 50f, Screen.height - boundingVector.y, 100f, 50f), boxText);
                }
            }
        }

        private void DrawLootableContainers()
        {
            if (_lootableContainers == null)
                return;

            foreach (var lootableContainer in _lootableContainers)
            {
                if (lootableContainer == null)
                    continue;

                float distance = Vector3.Distance(Camera.main.transform.position, lootableContainer.transform.position);
                var boundingVector = Camera.main.WorldToScreenPoint(lootableContainer.transform.position);
                if (boundingVector.z > 0.01 && distance <= 50)
                {
                    GUI.color = Color.magenta;
                    string boxText = $"{lootableContainer.name} - [{(int)distance}]m";
                    GUI.Label(new Rect(boundingVector.x - 50f, Screen.height - boundingVector.y, 100f, 50f), boxText);
                }
            }
        }

        private void DrawItemESP()
        {
            if (_lootItems == null)
                return;

            foreach (LootItem lootItem in _lootItems)
            {
                if (lootItem == null)
                    continue;

                float distance = Vector3.Distance(Camera.main.transform.position, lootItem.transform.position);
                Vector3 boundingVector = Camera.main.WorldToScreenPoint(lootItem.transform.position);

                if (boundingVector.z > 0.01 && distance <= 50)
                {
                    if (lootItem.name.Contains("key") ||
                        lootItem.name.Contains("usb") ||
                        lootItem.name.Contains("alkali") ||
                        lootItem.name.Contains("ophalmo") ||
                        lootItem.name.Contains("gunpowder") ||
                        lootItem.name.Contains("phone") ||
                        lootItem.name.Contains("money") ||
                        lootItem.name.Contains("document") ||
                        lootItem.name.Contains("quest") ||
                        lootItem.name.Contains("spark") ||
                        lootItem.name.Contains("grizzly") ||
                        lootItem.name.Contains("sv-98") ||
                        lootItem.name.Contains("sv98") ||
                        lootItem.name.Contains("rsas") ||
                        lootItem.name.Contains("salewa") ||
                        lootItem.name.Equals("bitcoin") ||
                        lootItem.name.Contains("dvl") ||
                        lootItem.name.Contains("m4a1") ||
                        lootItem.name.Contains("roler") ||
                        lootItem.name.Contains("chain") ||
                        lootItem.name.Contains("wallet") ||
                        lootItem.name.Contains("RSASS") ||
                        lootItem.name.Contains("glock") ||
                        lootItem.name.Contains("SA-58"))
                    {
                        string text = $"{lootItem.Name} - [{(int)distance}]m";

                        GUI.color = Color.yellow;
                        GUI.Label(new Rect(boundingVector.x - 50f, Screen.height - boundingVector.y, 100f, 50f), text);
                    }
                }
            }
        }

        public void Trainer()
        {
            //try
            //{
            GameWorld world = Singleton<GameWorld>.Instance;

            if (world != null)
            {
                _exfiltrationPoints = FindObjectsOfType<ExfiltrationPoint>();
                _lootableContainers = FindObjectsOfType<LootableContainer>();
                _lootItems = FindObjectsOfType<LootItem>();
                _players = FindObjectsOfType<Player>();
            }
            //}
            //catch (Exception e)
            //{
            //    //Save Errors to a logfile.
            //}
        }

        private string ExtractionNameToSimpleName(string extractionName)
        {
            // Factory
            if (extractionName.Contains("exit (3)"))
                return "Cellars";
            if (extractionName.Contains("exit (1)"))
                return "Gate 3";
            if (extractionName.Contains("exit (2)"))
                return "Gate 0";
            if (extractionName.Contains("exit_scav_gate3"))
                return "Gate 3";
            if (extractionName.Contains("exit_scav_camer"))
                return "Blinking Light";
            if (extractionName.Contains("exit_scav_office"))
                return "Office";

            // Woods
            if (extractionName.Contains("eastg"))
                return "East Gate";
            if (extractionName.Contains("scavh"))
                return "House";
            if (extractionName.Contains("deads"))
                return "Dead Mans Place";
            if (extractionName.Contains("var1_1_constant"))
                return "Outskirts";
            if (extractionName.Contains("scav_outskirts"))
                return "Outskirts";
            if (extractionName.Contains("water"))
                return "Outskirts Water";
            if (extractionName.Contains("boat"))
                return "The Boat";
            if (extractionName.Contains("mountain"))
                return "Mountain Stash";
            if (extractionName.Contains("oldstation"))
                return "Old Station";
            if (extractionName.Contains("UNroad"))
                return "UN Road Block";
            if (extractionName.Contains("var2_1_const"))
                return "UN Road Block";
            if (extractionName.Contains("gatetofactory"))
                return "Gate to Factory";
            if (extractionName.Contains("RUAF"))
                return "RUAF Gate";

            // Shoreline
            if (extractionName.Contains("roadtoc"))
                return "Road to Customs";
            if (extractionName.Contains("lighthouse"))
                return "Lighthouse";
            if (extractionName.Contains("tunnel"))
                return "Tunnel";
            if (extractionName.Contains("wreckedr"))
                return "Wrecked Road";
            if (extractionName.Contains("deadend"))
                return "Dead End";
            if (extractionName.Contains("housefence"))
                return "Ruined House Fence";
            if (extractionName.Contains("gyment"))
                return "Gym Entrance";
            if (extractionName.Contains("southfence"))
                return "South Fence Passage";
            if (extractionName.Contains("adm_base"))
                return "Admin Basement";

            // Customs
            if (extractionName.Contains("administrationg"))
                return "Administration Gate";
            if (extractionName.Contains("factoryfar"))
                return "Factory Far Corner";
            if (extractionName.Contains("oldazs"))
                return "Old Gate";
            if (extractionName.Contains("milkp_sh"))
                return "Shack";
            if (extractionName.Contains("beyondfuel"))
                return "Beyond Fuel Tank";
            if (extractionName.Contains("railroadtom"))
                return "Railroad to Mil Base";
            if (extractionName.Contains("_pay_car"))
                return "V-Exit";
            if (extractionName.Contains("oldroadgate"))
                return "Old Road Gate";
            if (extractionName.Contains("sniperroad"))
                return "Sniper Road Block";
            if (extractionName.Contains("warehouse17"))
                return "Warehouse 17";
            if (extractionName.Contains("factoryshacks"))
                return "Factory Shacks";
            if (extractionName.Contains("railroadtotarkov"))
                return "Railroad to Tarkov";
            if (extractionName.Contains("trailerpark"))
                return "Trailer Park";
            if (extractionName.Contains("crossroads"))
                return "Crossroads";
            if (extractionName.Contains("railroadtoport"))
                return "Railroad to Port";

            // Interchange
            if (extractionName.Contains("NW_Exfil"))
                return "North West Extract";
            if (extractionName.Contains("SE_Exfil"))
                return "South East Extract";
            return extractionName;
        }

        private string ItemNameToSimpleName(string name)
        {
            if (name.Contains("gphone"))
                return "GPhone";
            if (name.Contains("bitcoin"))
                return "Bitcoin";
            if (name.Contains("rolex"))
                return "Roler";
            if (name.Contains("lion"))
                return "Lion";
            if (name.Contains("clock"))
                return "Clock";
            if (name.Contains("chain_gold"))
                return "Gold Chain";
            if (name.Contains("wallet"))
                return "Wallet";
            if (name.Contains("cpu"))
                return "CPU";
            if (name.Contains("video"))
                return "Graphics Card";
            if (name.Contains("insulation"))
                return "Tape";
            if (name.Contains("apollo"))
                return "Apollo";
            if (name.Contains("wilston"))
                return "Wilston";
            if (name.Contains("malbor"))
                return "Malboro";
            if (name.Contains("strike"))
                return "Strike";
            if (name.Contains("powder"))
                return "Gun Powder";
            if (name.Contains("battery_d"))
                return "D Battery";
            if (name.Contains("crickent"))
                return "Crickent";
            if (name.Contains("splint"))
                return "Splint";
            if (name.Contains("wiper"))
                return "Wiper";
            if (name.Contains("matches"))
                return "Matches";
            if (name.Contains("analg"))
                return "Painkillers";
            if (name.Contains("screw_bol"))
                return "Bolts";
            if (name.Contains("Roulette"))
                return "MTape";
            if (name.Contains("condensed"))
                return "Condensed Milk";
            if (name.Contains("maxEnergy"))
                return "Energy Drink";
            if (name.Contains("bandage"))
                return "Bandage";
            if (name.Contains("screwdriver"))
                return "Screwdriver";
            if (name.Contains("powerbank"))
                return "Power Bank";
            if (name.Contains("ironkey"))
                return "Flashdrive";
            if (name.Contains("item_barter_tools_pliers_elite"))
                return "Elite Pliers";
            if (name.Contains("item_tools_pliers"))
                return "Pliers";
            return name;
        }
    }
}
