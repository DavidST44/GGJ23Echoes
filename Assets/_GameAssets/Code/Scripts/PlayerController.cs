using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Weapon weapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    [SerializeField]
    private float fireForce = 20f;
    [SerializeField]
    private float firerate = 0, firerateMax = 2;

    private int health;
    private int ammo;
    private bool invincible = false;
    private float InvincibleTimer = 0;
    private float InvincibleTimeMax = 3;

    private float BeginRegenHealth = 0;
    private float BeginRegenHealthMax = 5;

    private float hpRegenTimer = 0;
    private float hpRegenTimerTimeMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        ammo = PlayerProgression.Player_MaxAmmo;
        health = PlayerProgression.Player_MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            if (InvincibleTimer < InvincibleTimeMax)
                InvincibleTimer += Time.deltaTime;
            else
            {
                InvincibleTimer = 0;
                invincible= false;
            }
        }

        if (health < PlayerProgression.Player_MaxHp)
        {
            BeginRegenHealth += Time.deltaTime;

            if (BeginRegenHealth >= BeginRegenHealthMax)
            {
                if (hpRegenTimer < hpRegenTimerTimeMax)
                    hpRegenTimer += Time.deltaTime;
                else
                {
                    hpRegenTimer = 0;
                    health++;
                    PlayerProgression.local.HUD.UpdateHealthBar();
                }
            }
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            if (firerate >= firerateMax && ammo > 0)
            {
                ammo--;
                PlayerProgression.local.HUD.UpdateAmmo();
                weapon.Fire();
                firerate = 0;
            }
        }

        firerate += Time.deltaTime;
        if (firerate >= (firerateMax - PlayerProgression.Player_ShootSpd))
            firerate = (2 - PlayerProgression.Player_ShootSpd);

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * (moveSpeed + PlayerProgression.Player_MoveSpd), moveDirection.y * (moveSpeed + PlayerProgression.Player_MoveSpd));

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ammo")
        {
            ammo = PlayerProgression.Player_MaxAmmo;
            PlayerProgression.local.HUD.UpdateAmmo();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<Bullet>().Shooter != this.gameObject)
        {
            HurtPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HurtPlayer();
        }
    }

    void HurtPlayer()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            FindAnyObjectByType<Waves>().Alive = false;
        }
        else
        {

            if (!invincible)
            {
                health--;
                PlayerProgression.local.HUD.UpdateHealthBar();
                BeginRegenHealth = 0;
                invincible = true;
            }
        }
    }
    public int Ammo { set => ammo = value; get { return ammo; } }
    public int Health { set => health = value; get { return health; } }
    public float Firerate { get { return firerate; } }
    public float FirerateMax { get { return firerateMax; } }
    public bool Invincible { set => invincible = value; get { return invincible; } }
}
