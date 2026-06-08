using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
       transform.Translate(Vector3.forward * speed * Time.deltaTime );
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (var t in tagsToHit)
        {
            if (other.transform.tag == t)
            {
                var damageable = other.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    // Calcula a direção do impacto (empurrando o player)
                    Vector3 dir = other.transform.position - transform.position;
                    dir = dir.normalized;
                    dir.y = 0;

                    // Aplica o dano usando o nome correto da variável: damageAmount
                    damageable.Damage(damageAmount, dir);

                    // Destrói o projétil imediatamente após causar dano
                    Destroy(gameObject);
                    break;
                }
            }
        }

        // Se bater em qualquer outra coisa (cenário/paredes) que não seja outro projétil, se destrói
        if (!other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

}
