using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ObjectPool pool;
    public ItemSpawner itemSpawner;
    public SpawnCarManager spawnCarManager;
   
    public string bestScoreKey = "bestScore";
    public float bestScore = 0; // 점수는 플레이 후 경과한 시간을 저장하도록 함
    public float curScore = 0;
    public int PizzaCount = 0;
    public GameObject Pizza;
    public SpawnPizza SpawnPizza;
    public bool havePizza;
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemSpawner = GetComponent<ItemSpawner>();
        SpawnPizza = SpawnPizza.GetComponent<SpawnPizza>();
        itemSpawner.Initialize();
        itemSpawner.StartSpawningItems();
    }

    public void GameOver() // 플레이어가 다른 오브젝트와 충돌했을 때 호출
    {
        isGameOver = true;
        Cursor.lockState = CursorLockMode.None;
        setBestScore();
        UIManager.Instance.SetEndPanel();
    }

    private void setBestScore()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            bestScore = PlayerPrefs.GetFloat(bestScoreKey);
            if (bestScore < curScore)
            {
                PlayerPrefs.SetFloat(bestScoreKey, curScore);
                bestScore = curScore;
            }
        }
        else
        {
            PlayerPrefs.SetFloat(bestScoreKey, curScore);
            bestScore = curScore;
        }
    }

    public void ResetGame()
    {
        isGameOver = false;
        havePizza = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
