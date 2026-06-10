using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionShoot;
    public float timeBetweenShoot = 0.3f;
    private Coroutine _currentCoroutine;
    public float speed = 50f;
    
    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
        

    protected virtual IEnumerator ShootCoroutine()
    {
       while(true) 
       {
        Shoot();
        yield return new WaitForSeconds(timeBetweenShoot);
       }
    
    }
    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.transform.rotation = positionShoot.rotation;
        projectile.speed = speed;

        // Proteção para o tiro funcionar mesmo se a câmera falhar
        if (ShakeCamera.Instance != null)
        {
            ShakeCamera.Instance.ShakeCam();
        }
        else
        {
            Debug.LogWarning("ShakeCamera.Instance está nulo! Verifique se o script está em um GameObject ativo na cena.");
        }
    }

    public void StartShoot()
    {
       StopShoot();
       _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
         if(_currentCoroutine !!= null) 
            {
                StopCoroutine(_currentCoroutine);
            }
    }
}
