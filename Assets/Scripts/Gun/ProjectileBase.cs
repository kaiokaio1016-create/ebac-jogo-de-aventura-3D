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
        if(other.transform.tag == t)
        {
         var damageable = other.transform.GetComponent<IDamageable>();

         if(damageable != null) 
         {
            Vector3 dir = other.transform.position - transform.position;
            dir = -dir.normalized;
            dir.y = 0;

            damageable.Damage(damageAmount, dir);
         }

          break;

        }
      }
         //Ignora colisoes com outros projeteis
         if(!other.gameObject.CompareTag("Projectile")) 
         {   
          Destroy(gameObject);
         }
    }
}
