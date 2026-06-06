using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Ebac.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public Volume volume;
    private Vignette vignette;
    public Color defaltColor = Color.black;
    public float duration = 1f;



    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        // Ensure Vignette exists in the Volume
        if (!volume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette effect not found in Volume Profile!");
            yield break;
        }

        float time = 0;
        while (time < duration)
        {
            vignette.color.value = Color.Lerp(defaltColor, Color.red, time / duration);
            time += Time.deltaTime;
            yield return null; // More optimized than WaitForEndOfFrame()
        }

        time = 0;
        while (time < duration)
        {
            vignette.color.value = Color.Lerp(Color.red, defaltColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure final color is fully black at the end
        vignette.color.value = defaltColor;
    }
}
