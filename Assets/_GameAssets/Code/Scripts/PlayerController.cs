using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private int ammo;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            if (firerate >= firerateMax)
            { 
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
    public int Ammo { set => ammo = value; }
    public float Firerate { get { return firerate; } }
    public float FirerateMax { get { return firerateMax; } }
}
