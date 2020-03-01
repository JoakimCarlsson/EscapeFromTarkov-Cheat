using EFT.InventoryLogic;
using EscapeFromTarkovCheat.Feauters.ESP;
using UnityEngine;

namespace EscapeFromTarkovCheat
{
    public class Loader
    {
        public static GameObject HookObject
        {
            get
            {
                var result = GameObject.Find("Application (Main Client)");
                if (result == null)
                {
                    result = new GameObject("Trainer");
                    Object.DontDestroyOnLoad(result);
                }
                return result;
            }
        }

        public static void Load()
        {
            HookObject.AddComponent<Menu.UI.Menu>();
            HookObject.AddComponent<PlayerESP>();
            HookObject.AddComponent<ItemESP>();
            HookObject.AddComponent<LootableContainerESP>();
            HookObject.AddComponent<ExfiltrationPointsESP>();
        }

        public static void Unload()
        {
            Object.Destroy(HookObject);
        }
    }

}
