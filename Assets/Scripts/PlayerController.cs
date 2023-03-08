using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 
    public Weapon weapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    public float fireForce = 20f;
    public float Firerate = 0, FirerateMax = 2;

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
            if (Firerate >= FirerateMax)
            { 
                weapon.Fire();
                Firerate = 0;
            }
        }

        Firerate += Time.deltaTime;
        if (Firerate >= FirerateMax)
            Firerate = 2;

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
}
