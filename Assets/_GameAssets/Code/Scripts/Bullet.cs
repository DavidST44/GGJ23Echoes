using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Shooter;
    public AudioClip enemyAudioSource;

    public GameObject audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Shooter )
        {
            //GameObject a = Instantiate(audioSource);
            //audioSource.GetComponent<AudioSource>().clip = enemyAudioSource;
            //enemyAudioSource = collision.GetComponent<AudioSource>();
            //enemyAudioSource.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player")&& collision.gameObject != Shooter)
        {
            if (collision.gameObject.GetComponent<PlayerController>().Health < 0)
            {
                Destroy(collision.gameObject);
                FindAnyObjectByType<Waves>().Alive = false;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().Health--;
                collision.gameObject.GetComponent<PlayerController>().Invincible = true;
            }
        }
    }
}
