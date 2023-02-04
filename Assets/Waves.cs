using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    Queue<EnemyType> Enemies;
    Queue<EnemyType> backup;
    public GameObject[] enemyList;
    public int waveSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new Queue<EnemyType>(); 
        for(int i = 0; i < waveSize; i++)
        {
            GameObject Enemy = Instantiate(enemyList[Random.Range(0,enemyList.Length)],new Vector3(Random.Range(25,-25), Random.Range(15, -15), 0), Quaternion.identity);
            Enemies.Enqueue(Enemy.GetComponent<Enemy>().ColourName);

            Enemy.name = Enemy.name + " " + i;
        }

        backup = new Queue<EnemyType>(Enemies);
        foreach (EnemyType a in Enemies)
            Debug.Log(a);
    }

    public bool IsCorrectTarget(GameObject enemy)
    {
        if (Enemies.Peek() == enemy.GetComponent<Enemy>().ColourName)
        {
            Enemies.Dequeue();
            return true;
        }
        else
        {
            //Enemies = new Queue<GameObject>(backup);
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
