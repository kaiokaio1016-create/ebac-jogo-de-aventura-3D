using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Items
{
    public class CollectableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        [Header("Sounds & Particles")]
        public ParticleSystem particleSystem;
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            // Esconde o objeto visualmente e desativa colisões ao coletar
            gameObject.SetActive(false);
            OnCollect();
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void OnCollect()
        {
            PlaySFX();
            if (particleSystem != null) particleSystem.Play();

            
            if (audioSource != null) audioSource.Play();

            
            if (ItemManager.Instance != null)
            {
                ItemManager.Instance.AddByType(itemType);
            }
            else
            {
                Debug.LogError("[CollectableBase] O ItemManager.Instance está nulo! Garanta que ele está na cena.");
            }
        }
    }
}
