using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;
    public float damage = 10f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        //  SOLUÇÃO DEFINITIVA: Anda na direção para onde a bala está apontada
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica se a colisão está acontecendo
        Debug.Log("Colidi com: " + other.gameObject.name);

        // 2. Tenta encontrar a interface
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("Sucesso! Componente IDamageable encontrado.");
            damageable.OnDamage(this.damage);
            Destroy(gameObject);
        }
        else
        {
            // 3. Se cair aqui, o script EnemyBase não está no objeto que o projétil atingiu
            Debug.LogWarning("O objeto atingido NÃO possui o script com IDamageable!");

            // DICA: Tente buscar no pai do objeto, caso o collider esteja em um filho
            damageable = other.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("Encontrei o IDamageable no PAI do objeto!");
                damageable.OnDamage(this.damage);
                Destroy(gameObject);
            }
        }
    }
}