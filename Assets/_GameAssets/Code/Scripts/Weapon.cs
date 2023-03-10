using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint; 
    public float fireForce = 20f;



    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * (fireForce + PlayerProgression.Player_BulletSpd), ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().Shooter = transform.parent.gameObject;
        Destroy(bullet,5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
