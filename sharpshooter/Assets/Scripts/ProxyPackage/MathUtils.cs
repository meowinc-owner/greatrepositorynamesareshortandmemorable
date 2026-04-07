using UnityEngine;

public static class MathUtils
{
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ Remap ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    
    public static float Remap(float oldmin, float oldmax, float newmin, float newmax, float value, float totalmin,
        float totalmax)
    {
        float t = Mathf.InverseLerp(oldmin, oldmax, value);
        float v = Mathf.Lerp(newmin, newmax, t);
        return Mathf.Clamp(v, totalmin, totalmax);
    }
    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ SmoothStepFromValue ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘

    public static float SmoothStepFromValue(float min, float max, float value)
    {
        float t = Mathf.InverseLerp(min, max, value);
        return Mathf.SmoothStep(min, max, t);
    }
}
