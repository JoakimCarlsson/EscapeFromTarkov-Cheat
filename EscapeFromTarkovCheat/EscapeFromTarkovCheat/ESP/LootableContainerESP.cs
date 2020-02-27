using UnityEngine;

namespace EscapeFromTarkovCheat.ESP
{
    public class LootableContainerESP
    {
        private readonly CheatBehaviour _cheatBehaviour;

        public LootableContainerESP(CheatBehaviour cheatBehaviour)
        {
            _cheatBehaviour = cheatBehaviour;
        }

        public void DrawLootableContainers()
        {
            if (_cheatBehaviour.LootableContainers == null)
                return;

            foreach (var lootableContainer in _cheatBehaviour.LootableContainers)
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
    }
}