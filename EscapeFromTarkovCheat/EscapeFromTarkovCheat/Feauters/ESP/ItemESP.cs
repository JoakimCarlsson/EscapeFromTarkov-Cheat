using System;
using System.Collections;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using JsonType;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
{
    public class ItemESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 2.5f;
        private static readonly float MaximumLootItemDistance = 1000f;
        private float _nextLootItemCacheTime;

        private static readonly Color _specialColor = new Color(1f, 0.2f, 0.09f);
        private static readonly Color _questColor = Color.yellow;
        private static readonly Color _commonColor = Color.white;
        private static readonly Color _rareColor = new Color(0.38f, 0.43f, 1f);
        private static readonly Color _superRareColor = new Color(1f, 0.29f, 0.36f);

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

                    if (gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Common)
                        Render.DrawString(new Vector2(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y), lootItemName, _commonColor);
                    if (gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Rare)
                        Render.DrawString(new Vector2(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y), lootItemName, _rareColor);
                    if (gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Superrare)
                        Render.DrawString(new Vector2(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y), lootItemName, _superRareColor);
                }
            }

        }
    }
}