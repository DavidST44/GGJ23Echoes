using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContinuousShot : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField]
    private Enemy enemyRef;

    public GameObject bulletPrefab;
    public float fireForce = 20f;


    public float FireRateMax = 2;
    private float fireRate = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (fireRate > FireRateMax)
        {
            fireRate = 0;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(-(transform.position- enemyRef.Target.transform.position).normalized * fireForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().Shooter = gameObject;
        }
        else
            fireRate += Time.deltaTime;

        
    }
}
