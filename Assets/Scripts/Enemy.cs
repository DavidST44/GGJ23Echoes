using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType ColourName;
    public GameObject UIElement;
    public GameObject UITextMode;
    public Color wrongColour;
    public GameObject Target;
    public AudioClip enemyAudioSource;

    public GameObject audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<Bullet>().Shooter != gameObject)
            {
                if (GameObject.FindAnyObjectByType<Waves>().IsCorrectTarget(gameObject))
                {
                    GameObject a = Instantiate(audioSource);
                    audioSource.GetComponent<AudioSource>().clip = enemyAudioSource;

                    Destroy(gameObject);
                }
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
public enum EnemyType { Green, Blue, Red };