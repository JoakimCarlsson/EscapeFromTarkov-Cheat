using EFT.Interactive;
using UnityEngine;

namespace EscapeFromTarkovCheat.ESP
{
    public class ItemESP
    {
        private readonly CheatBehaviour _cheatBehaviour;

        public ItemESP(CheatBehaviour cheatBehaviour)
        {
            _cheatBehaviour = cheatBehaviour;
        }

        public void DrawItemESP()
        {
            if (_cheatBehaviour.LootItems == null)
                return;

            foreach (LootItem lootItem in _cheatBehaviour.LootItems)
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