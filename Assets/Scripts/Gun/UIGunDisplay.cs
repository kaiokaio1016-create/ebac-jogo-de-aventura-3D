using UnityEngine;
using UnityEngine.UI;

public class UIGunDisplay : MonoBehaviour
{
    public Image gunImage1;
    public Image gunImage2;
    public Color selectedColor = Color.blue;
    private Color defaultColor = Color.white;

    private void Start()
    {
        UpdateGunDisplay(1);
    }

    private void Update()
    {
        // Verifica a arma atual baseada na tag
        GameObject currentGun = GetCurrentGun();
        if (currentGun != null)
        {
            string gunTag = currentGun.tag;
            if (gunTag == "Gun1")
            {
                UpdateGunDisplay(1);
            }
            else if (gunTag == "Gun2")
            {
                UpdateGunDisplay(2);
            }
        }
    }

    private GameObject GetCurrentGun()
    {
        // Aqui você deve retornar a referência da arma atualmente equipada pelo jogador
        // Dependendo da implementação do seu jogo, ajuste este método para obter a arma corretamente
        // Este exemplo busca a arma pelo componente PlayerAbilityShoot
        PlayerAbilityShoot playerAbilityShoot = FindObjectOfType<PlayerAbilityShoot>();
        if (playerAbilityShoot != null)
        {
            return playerAbilityShoot.GetCurrentGun()?.gameObject;
        }
        return null;
    }

    public void UpdateGunDisplay(int selectedGunIndex)
    {
        if (selectedGunIndex == 1)
        {
            gunImage1.color = selectedColor;
            gunImage2.color = defaultColor;
        }
        else if (selectedGunIndex == 2)
        {
            gunImage1.color = defaultColor;
            gunImage2.color = selectedColor;
        }
    }
}
