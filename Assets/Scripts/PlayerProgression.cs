using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    public static PlayerProgression local;
    public static float Player_MaxHp = 1, Player_ShootSpd = 0, Player_MoveSpd = 0, Player_BulletSpd = 0;

    public static float Player_MaxHpCap= 100, Player_ShootSpdCap = 100, Player_MoveSpdCap = 100, Player_BulletSpdCap = 100;
    public static int Player_Level, Player_EXP;
    public bool LevelUp = false;
    [SerializeField]
    private int Player_TargetEXP = 50;
    [SerializeField]
    private float Player_MaxHpIncrement = 1;
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
    }

    // Update is called once per frame
    public void IncreaseEXP(int amount)
    {
        Player_EXP = amount;

        if (Player_EXP >= Player_TargetEXP)
        {
            Player_EXP = 0;
            Player_Level++;
            LevelUp = true;
        }
    }

   public void IncreaseStats(int index)
    {
        switch (index)
        {
            case 0: Player_MaxHp += Player_MaxHpIncrement;          break;
            case 1: Player_ShootSpd += Player_ShootSpdIncrement;    break;
            case 2: Player_MoveSpd += Player_MoveSpdIncrement;      break;
            case 3: Player_BulletSpd += Player_BulletSpdIncrement;  break;
        }

    }
}
