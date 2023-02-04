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
    public Image shitpost;

    public GameObject IconContainer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(0);

        //backup = new Queue<GameObject>(Enemies);
    }

    void SpawnWave(int currentWave)
    {
        if (currentWave < EnemyWaveList.Count)
        {
            Enemies = new Queue<EnemyType>();
            if (EnemyWaveList[currentWave].RandomWaveFromPool)
            {
                for (int i = 0; i < EnemyWaveList[currentWave].waveSize; i++)
                {
                    GameObject Enemy = Instantiate(EnemyWaveList[currentWave].enemyList[Random.Range(0, EnemyWaveList[currentWave].enemyList.Length)], new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                    GameObject UIElement = Instantiate(Enemy.GetComponent<Enemy>().UIElement);
                    UIElement.transform.parent = IconContainer.transform;


                    Enemies.Enqueue(Enemy.GetComponent<Enemy>().ColourName);

                    print("Enemy Spawned: " + Enemy.GetComponent<Enemy>().ColourName);
                    DebugText.text = DebugText.text + " " + Enemy.GetComponent<Enemy>().ColourName;
                }
            }
            else
            {
                for (int i = 0; i < EnemyWaveList[currentWave].enemyList.Length; i++)
                {
                    GameObject Enemy = Instantiate(EnemyWaveList[currentWave].enemyList[i], new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                    GameObject UIElement = Instantiate(Enemy.GetComponent<Enemy>().UIElement);
                    UIElement.transform.parent = IconContainer.transform;
                    Enemies.Enqueue(Enemy.GetComponent<Enemy>().ColourName);

                    print("Enemy Spawned: " + Enemy.GetComponent<Enemy>().ColourName);
                    DebugText.text = DebugText.text + " " + Enemy.GetComponent<Enemy>().ColourName;
                }
            }
        }
        else
            shitpost.gameObject.SetActive(true);
    }

    public bool IsCorrectTarget(GameObject enemy)
    {
        if (Enemies.Peek() == enemy.GetComponent<Enemy>().ColourName)
        {
            Enemies.Dequeue();
            Destroy(IconContainer.transform.GetChild(0).gameObject);
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
        if (Enemies.Count > 0 || currentWave > EnemyWaveList.Count)
        {
            
            return;
        }
        currentWave++;
        DebugText.text = "";
        SpawnWave(currentWave);
    }

}
[System.Serializable]
public class EnemyWave
{
    public bool RandomWaveFromPool;
    public GameObject[] enemyList;
    public int waveSize = 5;
}