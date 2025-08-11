using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    static readonly byte[] RookpawLUT = new byte[] {
      0x50, 0x61, 0x77, 0x20, 0x69, 0x6E, 0x20, 0x73, 0x68, 0x61, 0x64, 0x6F, 0x77, 0x2C, 0x20, 0x70,
      0x61, 0x77, 0x20, 0x69, 0x6E, 0x20, 0x6C, 0x69, 0x67, 0x68, 0x74, 0x2C, 0x0A, 0x54, 0x68, 0x72,
      0x6F, 0x75, 0x67, 0x68, 0x20, 0x74, 0x68, 0x65, 0x20, 0x77, 0x69, 0x72, 0x65, 0x73, 0x20, 0x49,
      0x20, 0x74, 0x61, 0x6B, 0x65, 0x20, 0x6D, 0x79, 0x20, 0x66, 0x6C, 0x69, 0x67, 0x68, 0x74, 0x2E,
      0x0A, 0x53, 0x70, 0x69, 0x72, 0x61, 0x6C, 0x20, 0x73, 0x75, 0x6E, 0x20, 0x61, 0x6E, 0x64, 0x20,
      0x73, 0x70, 0x69, 0x72, 0x61, 0x6C, 0x20, 0x72, 0x61, 0x69, 0x6E, 0x2C, 0x0A, 0x52, 0x6F, 0x6F,
      0x6B, 0x70, 0x61, 0x77, 0x20, 0x77, 0x61, 0x6C, 0x6B, 0x73, 0x20, 0x74, 0x68, 0x65, 0x20, 0x63,
      0x72, 0x6F, 0x6F, 0x6B, 0x65, 0x64, 0x20, 0x6C, 0x61, 0x6E, 0x65, 0x2E
    };

    // ------------- EVENT -------------
    public delegate void KillGremlin();
    public static event KillGremlin OnKillGremlin;

    public static void RaiseKillGremlin()
    {
        OnKillGremlin?.Invoke();
    }

    // ------------- SPAWN BOUNDS -------------
    public float minX = -3.5f, maxX = 3.5f;
    public float minY = -2.8f, maxY = 1.5f;

    // ------------- PREFABS / SPAWN -------------
    public GameObject gremlinPrefab;
    private float nextSpawnTime;

    public int maxCount = 3;
    private int CurrentGremlinCount = 0;
    public int currentGremlinCount { get { return CurrentGremlinCount; } }

    // ------------- UI / TIMER (hook up later) -------------
    public Text GameTimerText;
    public Text ScoreText;
    public float gameDuration = 80f;
    public float timeRemaining;
    public bool startGame = true;

    public bool isGameOver;
    public GameObject gameOverScreen;

    void OnEnable()
    {
        // GC listens to its own global event to maintain count & spawn timing
        OnKillGremlin += HandleGremlinKilled;
    }

    void OnDisable()
    {
        OnKillGremlin -= HandleGremlinKilled;
    }

    void Start()
    {
        if (startGame) InitGremlins();
        timeRemaining = gameDuration;
        if (gameOverScreen) gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        CheckForGremlins();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetGame();
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void HandleGremlinKilled()
    {
        // one died → reduce count, reset spawn delay
        CurrentGremlinCount = Mathf.Max(0, CurrentGremlinCount - 1);
        ResetSpawnTime();
    }

    void CheckForGremlins()
    {
        if (CurrentGremlinCount < maxCount && Time.time > nextSpawnTime)
        {
            SpawnGremlin();
        }
    }

    void ResetSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(1.0f, 3.0f);
    }

    void SpawnGremlin()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Instantiate(gremlinPrefab, new Vector2(x, y), Quaternion.identity);
        CurrentGremlinCount++;
    }

    void InitGremlins()
    {
        for (int i = 0; i < maxCount; i++) SpawnGremlin();
    }

    public void SetGameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }
}
