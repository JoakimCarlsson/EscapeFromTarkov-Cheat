using System.Collections.Generic;
using Comfort.Common;
using UnityEngine;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.ESP;

namespace EscapeFromTarkovCheat
{

    public class CheatBehaviour : MonoBehaviour
    {
        public ExfiltrationPointsESP ExfiltrationPointsEsp { get; }
        public PlayerESP PlayerEsp { get; }
        public LootableContainerESP LootableContainerEsp { get; }
        public ItemESP ItemEsp { get; }

        public IEnumerable<LootableContainer> LootableContainers { set; get; }

        public IEnumerable<LootItem> LootItems { set; get; }
        public IEnumerable<Player> Players { set; get; }

        internal IEnumerable<ExfiltrationPoint> ExfiltrationPoints { get; set; }

        public CheatBehaviour()
        {
            ExfiltrationPointsEsp = new ExfiltrationPointsESP(this);
            PlayerEsp = new PlayerESP(this);
            LootableContainerEsp = new LootableContainerESP(this);
            ItemEsp = new ItemESP(this);
        }

        public void Awake()
        {
            //Debug.unityLogger.logEnabled = false;
        }

        public void Start()
        {
            InvokeRepeating(nameof(Trainer), 10f, 10f);
        }

        public void OnGUI()
        {
            if (Menu.DrawLootItems)
            {
                ItemEsp.DrawItemESP();
            }

            if (Menu.DrawExfiltrationPoints)
            {
                ExfiltrationPointsEsp.DrawExfiltrationPoints();
            }

            if (Menu.DrawLootableContainers)
            {
                LootableContainerEsp.DrawLootableContainers();
            }

            if (Menu.DrawPlayers)
            {
                PlayerEsp.DrawPlayerESP();
            }
        }



        public void Trainer()
        {
            GameWorld world = Singleton<GameWorld>.Instance;

            if (world != null)
            {
                if (Menu.DrawExfiltrationPoints)
                    ExfiltrationPoints = FindObjectsOfType<ExfiltrationPoint>();

                if (Menu.DrawLootableContainers)
                    LootableContainers = FindObjectsOfType<LootableContainer>();

                if (Menu.DrawLootItems)
                    LootItems = FindObjectsOfType<LootItem>();

                if (Menu.DrawPlayers)
                    Players = FindObjectsOfType<Player>();

            }
        }
    }
}
