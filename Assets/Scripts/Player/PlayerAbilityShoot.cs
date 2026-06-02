using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Weapon Settings")]
    public List<GunBase> gunPrefabs;
    public Transform gunPosition;

    private GunBase _currentGun;
    private int _currentWeaponIndex = 0;

    protected override void Init()
    {
        base.Init();

        if (inputs != null)
        {
            inputs.Gameplay.Enable();

            inputs.Gameplay.Shoot.performed += ctx => StartShoot();
            inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

            inputs.Gameplay.EquipWeapon1.performed += ctx => SwitchWeapon(0);
            inputs.Gameplay.EquipWeapon2.performed += ctx => SwitchWeapon(1);

            Debug.Log("Inputs do Gameplay ativados e vinculados com sucesso!");
        }
        else
        {
            Debug.LogError("O objeto 'inputs' nativo está nulo!");
        }

        if (gunPrefabs != null && gunPrefabs.Count > 0)
        {
            CreateGun(gunPrefabs[_currentWeaponIndex]);
        }
    }

    protected void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Gameplay.Shoot.performed -= ctx => StartShoot();
            inputs.Gameplay.Shoot.canceled -= ctx => CancelShoot();

            inputs.Gameplay.EquipWeapon1.performed -= ctx => SwitchWeapon(0);
            inputs.Gameplay.EquipWeapon2.performed -= ctx => SwitchWeapon(1);

            inputs.Gameplay.Disable();
        }
    }

    private void CreateGun(GunBase prefab)
    {
        if (prefab == null || gunPosition == null) return;

        if (_currentGun != null)
        {
            _currentGun.StopShoot();
            Destroy(_currentGun.gameObject);
        }

        _currentGun = Instantiate(prefab, gunPosition);
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localRotation = Quaternion.identity;
    }

    private void SwitchWeapon(int index)
    {
        if (gunPrefabs == null || index < 0 || index >= gunPrefabs.Count) return;
        if (index == _currentWeaponIndex && _currentGun != null) return;

        _currentWeaponIndex = index;
        CreateGun(gunPrefabs[_currentWeaponIndex]);
        Debug.Log("Arma trocada para o índice: " + index);
    }

    private void StartShoot()
    {
        if (_currentGun != null) _currentGun.StartShoot();
    }

    private void CancelShoot()
    {
        if (_currentGun != null) _currentGun.StopShoot();
    }
}