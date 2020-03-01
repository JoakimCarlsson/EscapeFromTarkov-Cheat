using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
{
    public class LootableContainerESP : MonoBehaviour
    {
        private IEnumerable<LootableContainer> _lootableContainers;

        private void Start()
        {
            InvokeRepeating(nameof(GetLootableContainers), 10f, 10f);
        }

        private void OnGUI()
        {
            if (Settings.DrawLootableContainers)
            {
                DrawLootableContainers();
            }
        }
        public void DrawLootableContainers()
        {
            if (_lootableContainers == null)
                return;

            foreach (var lootableContainer in _lootableContainers)
            {
                if (lootableContainer == null)
                    continue;

                float distance = Vector3.Distance(Camera.main.transform.position, lootableContainer.transform.position);
                var boundingVector = Camera.main.WorldToScreenPoint(lootableContainer.transform.position);
                if (boundingVector.z > 0.01 && distance <= Settings.DrawLootableContainersDistance)
                {
                    GUI.color = Color.magenta;
                    string boxText = $"{lootableContainer.name} - [{(int)distance}]m";
                    GUI.Label(new Rect(boundingVector.x - 50f, Screen.height - boundingVector.y, 100f, 50f), boxText);
                }
            }
        }

        private void GetLootableContainers()
        {
            GameWorld world = Singleton<GameWorld>.Instance;

            if (world != null)
            {
                if (Settings.DrawLootableContainers)
                    _lootableContainers = FindObjectsOfType<LootableContainer>();
            }
        }
    }
}