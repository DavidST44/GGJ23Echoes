using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Shooter;
    private AudioSource enemyAudioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Shooter || collision.gameObject.CompareTag("bullet"))

            enemyAudioSource = collision.GetComponent<AudioSource>();
            enemyAudioSource.Play();
            Destroy(gameObject);
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
