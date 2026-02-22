using Prism.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmajlecLights;

public class LightingFixer : MonoBehaviour
{
    private CameraSettings _originalSettings;

    public LightingFixer()
    {
        _originalSettings = CameraSettings.Read(gameObject);
    }

    // Hooks
    public void Start()
    {
        Plugin.LightsToggled += PluginOnLightsToggled;
        _originalSettings = CameraSettings.Read(gameObject);
        Apply();
    }

    private void PluginOnLightsToggled(bool enable)
    {
        if (enable)
            Apply();
        else
            Restore();
    }

    public void OnDestroy()
    {
        Plugin.LightsToggled -= PluginOnLightsToggled;
    }

    // Fixes
    public void Apply()
    {
        var mapData = Plugin.GetMapData(SceneManager.GetActiveScene().name);
        
        var newSettings = new CameraSettings
        {
            ToneType = TonemapType.ACES,
            Tonemap = mapData.Tonemap + new Vector3(0f, Plugin.Brightness.Value, 0f),
            SecondaryTonemap = mapData.SecondaryTonemap,
            ExposureUpperLimit = .9f + Plugin.Exposure.Value,
            UseVignette = false,

            TodScatteringDensity = 0.002f,
            
            CCSharpen = false,
            CCVintage = false,
        };

        newSettings.Write(gameObject);
    }

    public void Restore()
    {
        _originalSettings.Write(gameObject);
    }
}