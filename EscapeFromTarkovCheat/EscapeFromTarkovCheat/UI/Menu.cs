using System;
using System.Runtime.InteropServices;
using EscapeFromTarkovCheat;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace Menu.UI
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _miscVisualWindow;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeautersVisualWindow;

        private bool _visible = true;
        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeautersVisible;


        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(20f, 220f, 250f, 150f);
            _miscVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _aimbotVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _miscFeautersVisualWindow = new Rect(20f, 260f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

            if (Input.GetKeyDown(KeyCode.Delete))
                Loader.Unload();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 200, 60), "Carlsson");
            GUI.Label(new Rect(20, 40, 200, 60), "Escape From Tarkov Verison 0.3");

            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, "Menu");

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _miscVisualWindow = GUILayout.Window(2, _miscVisualWindow, RenderUi, "Misc Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeautersVisible)
                _miscFeautersVisualWindow = GUILayout.Window(4, _miscFeautersVisualWindow, RenderUi, "Misc");
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    GUILayout.Label("Insert For Menu");
                    GUILayout.Label("Delete For Unload Menu");

                    if (GUILayout.Button("Player Visual"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Misc Visual"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Misc"))
                        _miscFeautersVisible = !_miscFeautersVisible;
                    break;

                case 1:
                    Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, "Draw Players");
                    Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
                    Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
                    Settings.DrawPlayerSkeleton = GUILayout.Toggle(Settings.DrawPlayerSkeleton, "Draw Player Skeleton");
                    Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health");
                    GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersDistance} m");
                    Settings.DrawPlayersDistance = GUILayout.HorizontalSlider(Settings.DrawPlayersDistance,0f, 2000f);
                    break;

                case 2:
                    Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, "Draw Loot Items");
                    GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
                    Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 2000f);

                    Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, "Draw Containers");
                    GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
                    Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 2000f);

                    Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, "Draw Exits");
                    break;

                case 3:
                    Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Aimbot");
                    Settings.AimbotDrawFov = GUILayout.Toggle(Settings.AimbotDrawFov, "Aimbot Draw FOV");
                    GUILayout.Label($"Aimbot FOV {(int)Settings.AimbotFOV} m");
                    Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 360);

                    Settings.AimbotSmooth = GUILayout.Toggle(Settings.AimbotSmooth, "Aimbot Smooth");
                    GUILayout.Label($"Aimbot Smooth {(int)Settings.AimbotSmoothValue} m");
                    Settings.AimbotSmoothValue = GUILayout.HorizontalSlider(Settings.AimbotSmoothValue, 0f, 360);
                    break;

                case 4:
                    Settings.NoRecoil = GUILayout.Toggle(Settings.NoRecoil, "No Recoil");
                    Settings.NoSway = GUILayout.Toggle(Settings.NoRecoil, "No Sway");
                    Settings.DoorUnlocker = GUILayout.Toggle(Settings.NoRecoil, "Door Unlocker");
                    break;
            }
            GUI.DragWindow();
        }
    }
}
