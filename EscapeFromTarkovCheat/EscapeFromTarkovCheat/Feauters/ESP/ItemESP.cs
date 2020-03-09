using System;
using System.Collections;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
{
    public class ItemESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 2.5f;
        private static readonly float MaximumLootItemDistance = 1000f;
        private float _nextLootItemCacheTime;

        private List<GameLootItem> _gameLootItems;
        private void Start()
        {
            _gameLootItems = new List<GameLootItem>();
            AllocConsoleHandler.Open();
        }

        public void Update()
        {
            if (Time.time >= _nextLootItemCacheTime)
            {
                GameWorld gameWorld = Singleton<GameWorld>.Instance;

                if ((gameWorld != null) && (gameWorld.LootItems != null))
                {
                    _gameLootItems.Clear();

                    for (int i = 0; i < gameWorld.LootItems.Count; i++)
                    {
                        LootItem lootItem = gameWorld.LootItems.GetByIndex(i);

                        if (!GameUtils.IsLootItemValid(lootItem) || (Vector3.Distance(Camera.main.transform.position, lootItem.transform.position) > MaximumLootItemDistance))
                            continue;

                        _gameLootItems.Add(new GameLootItem(lootItem));
                    }

                    _nextLootItemCacheTime = (Time.time + CacheLootItemsInterval);
                }
            }

            foreach (GameLootItem gameLootItem in _gameLootItems)
                gameLootItem.RecalculateDynamics();
        }

        private void OnGUI()
        {
            if (Settings.DrawLootItems)
            {
                foreach (var gameLootItem in _gameLootItems)
                {
                    if (!GameUtils.IsLootItemValid(gameLootItem.LootItem) || !gameLootItem.IsOnScreen || gameLootItem.Distance > Settings.DrawLootItemsDistance)
                        continue;

                    string lootItemName = $"{gameLootItem.LootItem.Item.ShortName.Localized()} [{gameLootItem.FormattedDistance}]";
                    GUI.Label(new Rect(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y, 100f, 50f), lootItemName);
                }
            }

        }
    }
}