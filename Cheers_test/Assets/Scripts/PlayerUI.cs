using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Slider slider;
    public TMP_Text counterText;
    public TMP_Text ammoText;

    void Update()
    {
        counterText.text = GameManager.killscounter.ToString();
    }
    public void SetAmmo(int clip, int ammo)
    {
        ammoText.text = clip.ToString() + "/" + ammo.ToString();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}