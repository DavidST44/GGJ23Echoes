using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class HeadsUpDisplay : MonoBehaviour
{
    //https://medium.com/swlh/game-dev-how-to-make-health-bars-in-unity-from-beginner-to-advanced-9a1d728d0cbf
    public Image healthBarImage;
    public Image EXPBarImage;
    public Text AmmoAmount;
    public Text PlayerLevel;
    [HideInInspector]
    public PlayerController player;

    public void UpdateHud()
    {
        UpdateHealthBar();
        UpdateEXPBar();
        UpdateAmmo();
        UpdateLevel();
    }

    public void UpdateHealthBar()
    {
        Debug.Log(player.Health);
        Debug.Log(PlayerProgression.Player_MaxHp);
        healthBarImage.fillAmount = Mathf.Clamp((float)player.Health / PlayerProgression.Player_MaxHp, 0, 1f);
    }
    public void UpdateEXPBar()
    {
        EXPBarImage.fillAmount = Mathf.Clamp((float)PlayerProgression.Player_EXP / PlayerProgression.Player_TargetEXP, 0, 1f);
    }

    public void UpdateAmmo()
    {
        AmmoAmount.text = player.Ammo.ToString();
    }
    public void UpdateLevel()
    {
        PlayerLevel.text = PlayerProgression.Player_Level.ToString();
    }
}
