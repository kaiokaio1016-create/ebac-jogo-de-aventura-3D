using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Items
{
    public class CollectableBase : MonoBehaviour
    {
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

        protected virtual void OnCollect()
        {
            // Toca a partícula se ela existir
            if (particleSystem != null) particleSystem.Play();

            // Toca o som se ele existir
            if (audioSource != null) audioSource.Play();

            // Verifica de forma segura se o ItemManager existe antes de tentar adicionar o item
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
