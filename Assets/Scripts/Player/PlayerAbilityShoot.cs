using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase1;
    public GunBase gunBase2;
    public Transform gunPosition;
    private GunBase _currentGun;
   protected override void Init()
   {
     base.Init();
     CreateGun(gunBase1);
     
     inputs.Gameplay.Shoot.performed += cts => StartShoot();
     inputs.Gameplay.Shoot.performed += cts => CancelShoot();

     inputs.Gameplay.SwitchGun1.performed += cts => SwitchGun(gunBase1);
     inputs.Gameplay.SwitchGun2.performed += cts => SwitchGun(gunBase2);

   }

   private void CreateGun(GunBase gunBase)
   {
    if (_currentGun != null) 
    {
      Destroy(_currentGun.gameObject);
    }

     _currentGun = Instantiate(gunBase, gunPosition);
     _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
   }

   private void StartShoot()
   {
    _currentGun.StartShoot();
     Debug.Log("Start Shoot");
   }

   private void CancelShoot()
   {
    _currentGun.StopShoot();
     Debug.Log("Cancel Shoot");
   }

    private void SwitchGun(GunBase newGunBase)
    {
        CreateGun(newGunBase);
        Debug.Log("Switched Gun");
    }

    protected override void RegisterListeners()
    {
        inputs.Gameplay.SwitchGun1.performed += cts => SwitchGun(gunBase1);
        inputs.Gameplay.SwitchGun2.performed += cts => SwitchGun(gunBase2);
    }

    protected override void RemoveListeners()
    {
        inputs.Gameplay.SwitchGun1.performed -= cts => SwitchGun(gunBase1);
        inputs.Gameplay.SwitchGun2.performed -= cts => SwitchGun(gunBase2);
    }

    public GunBase GetCurrentGun()
    {
       return _currentGun;
    }
}
