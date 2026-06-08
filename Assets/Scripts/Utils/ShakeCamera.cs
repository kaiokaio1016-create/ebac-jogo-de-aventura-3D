using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Singleton;
using Cinemachine; // Voltamos para o namespace clássico da v2

public class ShakeCamera : Singleton<ShakeCamera>
{
    // Alterado para CinemachineVirtualCamera para bater com a sua versão do pacote
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;
    private CinemachineBasicMultiChannelPerlin c;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Pega o componente de ruído na v2
            c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    [NaughtyAttributes.Button]
    public void ShakeCam()
    {
        ShakeCam(amplitude, frequency, time);
    }

    public void ShakeCam(float amplitude, float frequency, float time)
    {
        if (c == null) return;

        // Na v2 clássica, as variáveis podem ser AmplitudeGain ou m_AmplitudeGain.
        // Se der erro em uma, a Unity aceitará a outra. Geralmente aceita com m_ nas propriedades internas.
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (c == null) return;

        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            c.m_AmplitudeGain = 0f;
            c.m_FrequencyGain = 0f;
        }
    }
}