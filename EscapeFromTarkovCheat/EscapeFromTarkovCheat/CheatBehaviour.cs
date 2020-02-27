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

        private bool _showLootItems = true;
        private bool _showExfiltrationPoints = true;
        private bool _showLootableContainer = true;
        private bool _showPlayers = true;

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
            if (_showLootItems)
            {
                ItemEsp.DrawItemESP();
            }

            if (_showExfiltrationPoints)
            {
                ExfiltrationPointsEsp.DrawExfiltrationPoints();
            }

            if (_showLootableContainer)
            {
                LootableContainerEsp.DrawLootableContainers();
            }

            if (_showPlayers)
            {
                PlayerEsp.DrawPlayerESP();
            }
        }



        public void Trainer()
        {
            GameWorld world = Singleton<GameWorld>.Instance;

            if (world != null)
            {
                ExfiltrationPoints = FindObjectsOfType<ExfiltrationPoint>();
                LootableContainers = FindObjectsOfType<LootableContainer>();
                LootItems = FindObjectsOfType<LootItem>();
                Players = FindObjectsOfType<Player>();
            }
        }
    }
}
