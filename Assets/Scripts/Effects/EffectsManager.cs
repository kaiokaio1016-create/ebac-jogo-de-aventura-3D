using System.Collections.Generic;
using System.Collections;
using UnityEngine;
// Removemos o 'using UnityEngine.Rendering;' puro para evitar o conflito do ColorParameter
using UnityEngine.Rendering.PostProcessing;
using Ebac.Singleton; // De volta para o Player3D conseguir usar o .Instance

public class EffectsManager : Singleton<EffectsManager> // De volta ao Singleton
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    public float duration = 1f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        // Agora o Unity sabe 100% que este ColorParameter é o do PostProcessing
        ColorParameter c = new ColorParameter();

        float time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
    }
}

