using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public List<EnemyWave> EnemyWaveList = new List<EnemyWave>();
    Queue<EnemyType> Enemies;

    //Queue<GameObject> backup;
    int currentWave;
    public Text DebugText;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(0);

        //backup = new Queue<GameObject>(Enemies);
    }

    void SpawnWave(int currentWave)
    {
        Enemies = new Queue<EnemyType>();

        for (int i = 0; i < EnemyWaveList[currentWave].waveSize; i++)
        {
            GameObject Enemy = Instantiate(EnemyWaveList[currentWave].enemyList[Random.Range(0, EnemyWaveList[currentWave].enemyList.Length)], new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
            Enemies.Enqueue(Enemy.GetComponent<Enemy>().ColourName);

            print("Enemy Spawned: " + Enemy.GetComponent<Enemy>().ColourName);
            DebugText.text = DebugText.text + " " + Enemy.GetComponent<Enemy>().ColourName;
        }
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
        if (Enemies.Count > 0 )
        return;

        currentWave++;
        DebugText.text = "";
        SpawnWave(currentWave);
    }

}
[System.Serializable]
public class EnemyWave
{
    bool RandomWaveFromPool;
    public GameObject[] enemyList;
    public int waveSize = 5;
}