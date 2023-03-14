using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 1;
    public int EXPDrop = 5;
    public EnemyType ColourName;
    public GameObject UIElement;
    public GameObject UITextMode;
    public Color wrongColour;
    public GameObject Target;


    [SerializeField] FMODUnity.EventReference[] WrongTarget, CorrectTarget; // pickup & drop ingredient

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<Bullet>().Shooter != gameObject)
            {
                if (FindAnyObjectByType<Waves>().IsCorrectTarget(gameObject))
                {
                    int index = FindAnyObjectByType<Waves>().index;
                    FMODUnity.RuntimeManager.PlayOneShot(CorrectTarget[index]);
                    FindAnyObjectByType<Waves>().index++;
                    if (FindAnyObjectByType<Waves>().index > CorrectTarget.Length-1)
                    {
                        FindAnyObjectByType<Waves>().index = 0;
                    }
                    Health--;

                }
                else
                {
                    int index = FindAnyObjectByType<Waves>().index;
                    FMODUnity.RuntimeManager.PlayOneShot(WrongTarget[index]);
                    FindAnyObjectByType<Waves>().index++;
                    if (FindAnyObjectByType<Waves>().index > WrongTarget.Length-1)
                    {
                        FindAnyObjectByType<Waves>().index = 0;
                    }

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
        if (Target != null)
        {
            Vector3 dir = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (Health <= 0)
            {
                PlayerProgression.local.IncreaseEXP(EXPDrop);
                Destroy(gameObject);
                
            }
        }
    }
}
public enum EnemyType { Green, Blue, Red };