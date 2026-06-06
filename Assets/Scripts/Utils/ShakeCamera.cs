using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Singleton;
using Unity.Cinemachine;


public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineCamera virtualCamera;
    public float shakeTime;
    private CinemachineBasicMultiChannelPerlin c;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    private void Start()
    {
      c = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
    }

    [NaughtyAttributes.Button]
    public void ShakeCam()
    {
       ShakeCam(amplitude, frequency, time);
    }


   public void ShakeCam(float amplitude, float frequency, float time)
   {
     c.AmplitudeGain = amplitude;
     c.FrequencyGain = frequency;

     shakeTime = time;
   }

   private void Update () 
   {
    if(shakeTime > 0)
    {
       shakeTime -= Time.deltaTime;
    }
    else
    {
       c.AmplitudeGain = 0f;
       c.FrequencyGain = 0f;
    }
   }

}
