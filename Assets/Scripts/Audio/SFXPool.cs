using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton; // Certifique-se de que este namespace está correto

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;
    public int poolSize = 10;
    private int _index = 0;

    private void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(this.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        // Verifica se o tipo é nulo ou NONE
        if (sfxType == SFXType.NONE) return;
        if (SoundManager.Instance == null) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        // Verifica se o som foi encontrado antes de tocar
        if (sfx != null && _audioSourceList != null)
        {
            _audioSourceList[_index].clip = sfx.audioClip;
            _audioSourceList[_index].Play();

            _index++;
            if (_index >= _audioSourceList.Count) _index = 0;
        }
    }
}