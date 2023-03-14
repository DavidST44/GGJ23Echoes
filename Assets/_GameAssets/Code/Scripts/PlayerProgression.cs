using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerProgression : MonoBehaviour
{
    
    [SerializeField]
    public FMOD.Studio.EventInstance Music;
    [SerializeField]
    FMODUnity.EventReference MusicRef;

    public static PlayerProgression local;
    public PlayerController player;
    public static float Player_ShootSpd = 0, Player_MoveSpd = 0, Player_BulletSpd = 0;

    public static float Player_ShootSpdCap = 100, Player_MoveSpdCap = 100, Player_BulletSpdCap = 100;
    public static int Player_Level = 0, Player_EXP = 0, Player_TargetEXP=10, Player_MaxAmmo = 5, Player_MaxAmmoCap = 10, Player_MaxHp = 3, Player_MaxHpCap = 100;
    public bool LevelUp = false;
    public bool Pause = false;
    public HeadsUpDisplay HUD;
    public Transform LevelUpScreen;
    [SerializeField]
    private int Player_MaxHpIncrement = 1;
    [SerializeField]
    private float Player_ShootSpdIncrement = 0.01f;
    [SerializeField]
    private float Player_MoveSpdIncrement = 0.01f;    
    [SerializeField]
    private float Player_BulletSpdIncrement = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        local = this;
        HUD.player = player;
        HUD.UpdateHud();
        HUD.UpdateStats();
        LevelUpScreen.gameObject.SetActive(false);
        Music = RuntimeManager.CreateInstance(MusicRef);
        Music.start();
        //StopSong();
    }

    // Update is called once per frame
    public void IncreaseEXP(int amount)
    {

       
        Player_EXP += amount;
        HUD.UpdateEXPBar();
        if (Player_EXP >= Player_TargetEXP)
        {
            Debug.Log("Hello");
            Player_EXP -= ( Player_TargetEXP);
            Player_Level += 1;
            HUD.UpdateLevel();
            LevelUp = true;
        }
    }

   public void IncreaseStats(int index)
    {
        if (Player_Level <= 0)
            return;
        switch (index)
        {
            case 0: Player_MaxHp += Player_MaxHpIncrement;          break;
            case 1: Player_BulletSpd += Player_BulletSpdIncrement;  break;
            case 2: Player_MoveSpd += Player_MoveSpdIncrement; break;
            case 3: Player_ShootSpd += Player_ShootSpdIncrement; break;
        }
        Player_Level--;
        HUD.UpdateStats();

    }


    public void StopSong()
    {
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
