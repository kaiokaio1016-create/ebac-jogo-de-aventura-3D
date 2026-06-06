using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uIGunUpdater;
    public float maxShoot = 5;
    public float timeToCharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    { 
       if(_recharging) yield break;
        
       while(true) 
       {
          if(_currentShoots < maxShoot) 
          {
            Shoot();
            _currentShoots++;
            CheckRecharge();
            UpdateUI();
            yield return new WaitForSeconds(timeBetweenShoot);
          }
       }
    }

    private void CheckRecharge()
    {
        if(_currentShoots >= maxShoot) 
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

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToCharge) 
        {
            time += Time.deltaTime;
            uIGunUpdater.ForEach(i => i.UpdateValue(time/timeToCharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdater.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        uIGunUpdater = GameObject.FindObjectsOfType<UIFillUpdater>().Where(ui => ui.updateType == UIFillUpdateType.Ammo).ToList();
    }
}
