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
            GameObject a = Instantiate(audioSource);
            audioSource.GetComponent<AudioSource>().clip = enemyAudioSource;
            //enemyAudioSource = collision.GetComponent<AudioSource>();
            //enemyAudioSource.Play();
            Destroy(gameObject);
        }
    }
}
