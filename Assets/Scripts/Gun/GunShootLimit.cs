using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;

    
    protected virtual void Start()
    {
        GetAllUIs();
        UpdateUI();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
                // Proteção para o loop não travar a Unity caso a arma precise recarregar
                yield return null;
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    private IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;

           
            uIGunUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));

            yield return new WaitForEndOfFrame();
        }

        _currentShoots = 0;
        _recharging = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        
        uIGunUpdaters = Object.FindObjectsByType<UIGunUpdater>(FindObjectsSortMode.None).ToList();
    }
}