using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public bool BastardMode = false;
    public GameObject PlayerRef;
    public GameObject bulletPrefab;
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
    public GameObject audioSource;

    public GameObject GameOver;
    private bool alive = true;
    float DieTimer = 2;
    float RestartTimer = 3;

    public List<AudioSequence> AudioSequenceList = new List<AudioSequence>();
    public AudioSequence Kaazoo = new AudioSequence();
    private int audioCollectionIndex = 0;
    private int index = 0;

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
                    GameObject Enemy = Instantiate(EnemyWaveList[currentWave].enemyList[randomselection].enemy, new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                    
                    CreateObj(Enemy, EnemyWaveList[currentWave].enemyList[randomselection].Move, EnemyWaveList[currentWave].enemyList[randomselection].Shoot, EnemyWaveList[currentWave].enemyList[randomselection]);
                }
            }
            else
            {
                for (int i = 0; i < EnemyWaveList[currentWave].enemyList.Length; i++)
                {
                    GameObject Enemy = Instantiate(EnemyWaveList[currentWave].enemyList[i].enemy, new Vector3(Random.Range(25, -25), Random.Range(15, -15), 0), Quaternion.identity);
                    CreateObj(Enemy, EnemyWaveList[currentWave].enemyList[i].Move, EnemyWaveList[currentWave].enemyList[i].Shoot, EnemyWaveList[currentWave].enemyList[i]);
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
            GameObject a = Instantiate(audioSource);
            audioSource.GetComponent<AudioSource>().clip = Kaazoo.AudioSequenceCollection[Mathf.RoundToInt(Random.Range(0, Kaazoo.AudioSequenceCollection.Count))];
            audioSource.GetComponent<AudioSource>().Play();
            //Enemies = new Queue<GameObject>(backup);
            return false;
        }
    }

    public void PlaySound()
    {
        GameObject a = Instantiate(audioSource);
        audioSource.GetComponent<AudioSource>().clip = AudioSequenceList[audioCollectionIndex].AudioSequenceCollection[index];
        audioSource.GetComponent<AudioSource>().Play();
        index++;
        if (index > AudioSequenceList[audioCollectionIndex].AudioSequenceCollection[index].length)
        {
            index = 0;
        }
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
    public bool HideUI;
    public bool Move;
    public float Speed =2;
    public float Range = 3;
    public float MaxDistance = 6;
    public bool Shoot;
}
