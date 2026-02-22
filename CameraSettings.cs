using Prism.Utils;
using UnityEngine;

namespace SmajlecLights;

public struct CameraSettings
{
    public TonemapType ToneType;
    public Vector3 Tonemap;
    public Vector3 SecondaryTonemap;
    public float ExposureUpperLimit;
    public bool UseVignette;

    public float TodScatteringDensity;

    public bool CCSharpen;
    public bool CCVintage;

    public static CameraSettings Read(GameObject cameraObject)
    {
        var prismEffects = cameraObject.GetComponent<PrismEffects>();
        var todScattering = cameraObject.GetComponent<TOD_Scattering>();

        var ccSharpen = cameraObject.GetComponent<CC_Sharpen>();
        var ccVintage = cameraObject.GetComponent<CC_Vintage>();

        return new CameraSettings
        {
            ToneType = prismEffects?.tonemapType ?? TonemapType.Filmic,
            Tonemap = prismEffects?.toneValues ?? Vector3.zero,
            SecondaryTonemap = prismEffects?.secondaryToneValues ?? Vector3.zero,
            ExposureUpperLimit =  prismEffects?.exposureUpperLimit ?? 1f,
            UseVignette =  prismEffects?.useVignette ?? true,

            TodScatteringDensity = todScattering?.GlobalDensity ?? 0f,

            CCSharpen = ccSharpen?.enabled ?? true,
            CCVintage = ccVintage?.enabled ?? true,
        };
    }

    public readonly void Write(GameObject cameraObject)
    {
        PrismEffects prismEffects;
        if ((prismEffects = cameraObject.GetComponent<PrismEffects>()) is not null)
        {
            prismEffects.tonemapType = ToneType;
            prismEffects.toneValues = Tonemap;
            prismEffects.secondaryToneValues = SecondaryTonemap;
            prismEffects.exposureUpperLimit = ExposureUpperLimit;
            prismEffects.useVignette = UseVignette;
        }

        TOD_Scattering todScattering;
        if ((todScattering = cameraObject.GetComponent<TOD_Scattering>()) is not null)
        {
            todScattering.GlobalDensity = TodScatteringDensity;
        }

        CC_Sharpen ccSharpen;
        if ((ccSharpen = cameraObject.GetComponent<CC_Sharpen>()) is not null)
            ccSharpen.enabled = CCSharpen;

        CC_Vintage ccVintage;
        if ((ccVintage = cameraObject.GetComponent<CC_Vintage>()) is not null)
            ccVintage.enabled = CCVintage;
    }
}