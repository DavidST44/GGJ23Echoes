using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Waves : MonoBehaviour
{
    public bool BastardMode = false;
    public GameObject PlayerRef;
    public GameObject bulletPrefab;
    public GameObject AmmoPrefab;
    public List<EnemyWave> EnemyWaveList = new List<EnemyWave>();
    Queue<EnemyType> Enemies;

    //Queue<GameObject> backup;
    int currentWave;
    public Text DebugText;
    public Image shitpost;
    public Image UnknownUIElement;
    public GameObject IconContainer;

    float nextWaveTimer = 0;
    [SerializeField] private float nextWaveTimerMax = 3;

    public GameObject GameOver;
    private bool alive = true;
    float DieTimer = 2;
    float RestartTimer = 3;

    private int audioCollectionIndex = 0;
    private int index = 0;

    private float SpawnAmmo = 0;
    private float SpawnAmmoMax = 2;

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
                    int randomselection = Random.Range(0, EnemyWaveList[currentWave].enemyList.Length);
                    EnemySpawner enemySpawner = EnemyWaveList[currentWave].enemyList[randomselection];

                    if (EnemyWaveList[currentWave].enemyList[randomselection].randomPos)
                    {
                        GameObject Enemy = Instantiate(enemySpawner.enemy, new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                        CreateObj(Enemy, enemySpawner.Move, enemySpawner.Shoot, enemySpawner);
                    }
                    else
                    {
                        GameObject Enemy = Instantiate(enemySpawner.enemy, enemySpawner.Position, Quaternion.identity);
                        CreateObj(Enemy, enemySpawner.Move, enemySpawner.Shoot, enemySpawner);
                    }
                }
            }
            else
            {
                for (int i = 0; i < EnemyWaveList[currentWave].enemyList.Length; i++)
                {
                    EnemySpawner enemySpawner = EnemyWaveList[currentWave].enemyList[i];
                    if (EnemyWaveList[currentWave].enemyList[i].randomPos)
                    {
                        GameObject Enemy = Instantiate(enemySpawner.enemy, new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                        CreateObj(Enemy, enemySpawner.Move, enemySpawner.Shoot, enemySpawner);
                    }
                    else
                    {
                        GameObject Enemy = Instantiate(enemySpawner.enemy, enemySpawner.Position, Quaternion.identity);
                        CreateObj(Enemy, enemySpawner.Move, enemySpawner.Shoot, enemySpawner);
                    }
                }
            }
        }
        else
            shitpost.gameObject.SetActive(true);
    }

    void CreateObj(GameObject Enemy, bool move, bool shoot, EnemySpawner spawner )
    {
        if (!spawner.HideUI)
        {
            if (!BastardMode)
            {
                GameObject UIElement = Instantiate(Enemy.GetComponent<Enemy>().UIElement);
                UIElement.transform.parent = IconContainer.transform;
            }
            else
            {
                GameObject UITextMode = Instantiate(Enemy.GetComponent<Enemy>().UITextMode);
                Debug.Log(UITextMode);
                UITextMode.transform.SetParent(IconContainer.transform);
                UITextMode.GetComponent<Text>().text = Enemy.GetComponent<Enemy>().ColourName.ToString();
                UITextMode.GetComponent<Text>().color = new Color(Enemy.GetComponent<Enemy>().wrongColour.r, Enemy.GetComponent<Enemy>().wrongColour.g, Enemy.GetComponent<Enemy>().wrongColour.b, 1);

            }
        }
        else
        {
            GameObject UIElement = Instantiate(UnknownUIElement.gameObject);
            UIElement.transform.parent = IconContainer.transform;
        }
        Enemies.Enqueue(Enemy.GetComponent<Enemy>().ColourName);
        Enemy.GetComponent<Enemy>().Target = PlayerRef;

        print("Enemy Spawned: " + Enemy.GetComponent<Enemy>().ColourName);
        if (move)
        {
            Enemy.AddComponent<MoveRandom>();
            Enemy.GetComponent<MoveRandom>().Speed = spawner.Speed;
            Enemy.GetComponent<MoveRandom>().MaxDistance = spawner.MaxDistance;
            Enemy.GetComponent<MoveRandom>().Range = spawner.Range;

        }
        if (shoot)
        {
            Enemy.AddComponent<EnemyContinuousShot>();
            Enemy.GetComponent<EnemyContinuousShot>().EnemyRef = Enemy.GetComponent<Enemy>();
            Enemy.GetComponent<EnemyContinuousShot>().bulletPrefab = bulletPrefab;
        }
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
            //GameObject a = Instantiate(audioSource);

            //Enemies = new Queue<GameObject>(backup);
            return false;
        }
    }

    public void PlaySound()
    {
        //GameObject a = Instantiate(audioSource);

        index++;
        //if (index > AudioSequenceList[audioCollectionIndex].AudioSequenceCollection[index].length)
        //{
        //    index = 0;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            DieTimer -= Time.deltaTime;
            if (DieTimer<=0)
            {
                GameOver.transform.gameObject.SetActive(true);
                RestartTimer -= Time.deltaTime;
                if (RestartTimer<=0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        if (PlayerRef.GetComponent<PlayerController>().Ammo <=0)
        {
            bool exists = GameObject.Find("AmmoCrate");
            if (!exists)
            {
                if (SpawnAmmo < SpawnAmmoMax)
                    SpawnAmmo += Time.deltaTime;
                else
                {
                    GameObject a =Instantiate(AmmoPrefab, new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                    a.name = "AmmoCrate";
                    SpawnAmmo = 0;
                }
            }
        }

        if (Enemies.Count > 0 || currentWave > EnemyWaveList.Count)
        {
            return;
        }
        if (nextWaveTimer >= nextWaveTimerMax)
        {
            currentWave++;
            SpawnWave(currentWave);
            nextWaveTimer = 0;
        }
        else
            nextWaveTimer += Time.deltaTime;
    }

    public bool Alive
    {
        set { alive = value; }
    }

}
[System.Serializable]
public class AudioSequence
{
    public List<AudioClip> AudioSequenceCollection = new List<AudioClip>();

}
[System.Serializable]
public class EnemyWave
{
    public bool RandomWaveFromPool;
    public EnemySpawner[] enemyList;
    public int waveSize = 5;
}
[System.Serializable]
public class EnemySpawner
{
    public GameObject enemy;
    public bool randomPos = true;
    [ShowIf("@randomPos == false")]
    public Vector3 Position; 
    public bool HideUI;
    public bool Move;
    [ShowIf("@Move == true")]
    public float Speed =2;
    [ShowIf("@Move == true")]
    public float Range = 3;
    [ShowIf("@Move == true")]
    public float MaxDistance = 6;
    public bool Shoot;
}
