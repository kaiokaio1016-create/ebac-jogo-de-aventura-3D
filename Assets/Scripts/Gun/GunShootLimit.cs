using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    [Header("UI Settings (Auto Managed)")]
    public List<MonoBehaviour> uiGunUpdaters;

    [Header("Base Settings")]
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
                // Chama o Shoot() herdado da GunBase, que agora está corrigido!
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();

                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
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
            float progress = time / timeToRecharge;

            foreach (var ui in uiGunUpdaters)
            {
                if (ui != null)
                {
                    var method = ui.GetType().GetMethod("UpdateValue", new System.Type[] { typeof(float) });
                    if (method != null) method.Invoke(ui, new object[] { progress });
                }
            }
            yield return new WaitForEndOfFrame();
        }

        _currentShoots = 0;
        _recharging = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        object[] parameters = new object[] { maxShoot, _currentShoots };
        foreach (var ui in uiGunUpdaters)
        {
            if (ui != null)
            {
                var method = ui.GetType().GetMethod("UpdateValue", new System.Type[] { typeof(float), typeof(float) });
                if (method != null) method.Invoke(ui, parameters);
            }
        }
    }

    private void GetAllUIs()
    {
        uiGunUpdaters = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .Where(x => x.GetType().GetMethods().Any(m => m.Name == "UpdateValue")).ToList();
    }
}