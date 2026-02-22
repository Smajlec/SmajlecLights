using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using SmajlecLights.Patches;
using UnityEngine;

namespace SmajlecLights;

[BepInPlugin("com.smajlec.lights", "SmajlecLights", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    // Config
    public static ConfigEntry<bool> FixEnabled { get; set; }

    // Plugin stuff
    public static ManualLogSource LogSource;

    // On the fly toggle
    public delegate void LightsToggleDelegate(bool enable);

    public static event LightsToggleDelegate LightsToggled;

    // Unity Hooks
    private void Awake()
    {
        // Logger
        LogSource = Logger;

        // Patches
        new PrismEffectsPatch().Enable();
    }

    private void Start()
    {
        FixEnabled = Config.Bind("Main", "Enabled", true);
        FixEnabled.SettingChanged += FixEnabledOnSettingChanged;
    }

#if DEBUG
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Insert)) return;

        FixEnabled.Value = !FixEnabled.Value;
    }
#endif

    private void OnDestroy()
    {
        FixEnabled.SettingChanged -= FixEnabledOnSettingChanged;
    }

    private void FixEnabledOnSettingChanged(object sender, EventArgs e) => LightsToggled?.Invoke(FixEnabled.Value);

    // Data
    public static MapData GetMapData(string mapSceneName)
    {
        switch (mapSceneName)
        {
            case "Sandbox_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(25f, 0.2f, 25f),
                    SecondaryTonemap = new Vector3(0f, 1.1f, 0f)
                };
            case "City_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(25f, 0.2f, 25f),
                    SecondaryTonemap = new Vector3(0f, 1.1f, 0f)
                };
            case "Laboratory_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.4f, 20f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "custom_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 20f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "Factory_Rework_Day_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(25f, 0.6f, 25f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "Factory_Rework_Night_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(25f, 0.6f, 25f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "Lighthouse_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 20f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "Shopping_Mall_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 18f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "woods_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 20f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            case "Reserve_Base_Scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 20f),
                    SecondaryTonemap = new Vector3(0f, 0.85f, 0f)
                };
            case "shoreline_scripts":
                return new MapData
                {
                    Tonemap = new Vector3(20f, 0.2f, 20f),
                    SecondaryTonemap = new Vector3(0f, 1f, 0f)
                };
            default:
                return new MapData
                {
                    Tonemap = new Vector3(8f, -0.1f, 8f),
                    SecondaryTonemap = new Vector3(0f, 0.85f, 0f)
                };
        }
    }
}