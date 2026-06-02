using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int amountPerShoot = 4;
    public float angle = 15f;
    public float speed = 20f; // <-- A variável foi adicionada aqui para resolver o erro

    public override void Shoot()
    {
        int mult = 0;

        for (int i = 0; i < amountPerShoot; i++)
        {
            if (i % 2 == 0)
            {
                mult++;
            }

            // 1. Instancia o projétil na posição correta e já herdando a rotação do cano (positionToShoot)
            var projectile = Instantiate(prefabProjectile, positionToShoot.position, positionToShoot.rotation);

            // 2. Aplica o espalhamento angular baseado na rotação atualizada do cano
            projectile.transform.Rotate(Vector3.up * (i % 2 == 0 ? angle : -angle) * mult);

            // 3. Configura a velocidade
            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}