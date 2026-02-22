using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;

namespace SmajlecLights.Patches;

internal class PrismEffectsPatch : ModulePatch
{
    private static readonly string[] CameraObjects = ["Cam2_obshaga_zalupa4(Clone)", "Cam2_fps_hideout(Clone)", "Cam2_obshaga_zalupa4_DistantShadows_OFF(Clone)"]; // Nice object names

    protected override MethodBase GetTargetMethod()
    {
        return typeof(PrismEffects).GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public); // It may not be the best place to hook, but it works
    }

    [PatchPostfix]
    private static void PatchPostFix(ref PrismEffects __instance)
    {
        Plugin.LogSource.LogDebug($"PrismEffect Awake called for {__instance.gameObject.name}");
        if (!CameraObjects.Contains(__instance.gameObject.name)) return;

        Plugin.LogSource.LogDebug("Camera matched");
        __instance.gameObject.AddComponent<LightingFixer>();
    }
}