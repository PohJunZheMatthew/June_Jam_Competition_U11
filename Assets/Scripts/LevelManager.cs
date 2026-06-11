using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Levels")]
    [SerializeField] private int maxLevels = 10;

    [Header("Timer")]
    [SerializeField] private float maxTimeLeft = 60f;

    private float timeLeft;

    private const string LEVEL_KEY = "CurrentLevel";

    public int CurrentLevel { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevelProgress();
        LoadCurrentLevel();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            TimeUp();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timeLeft = maxTimeLeft;
    }

    private void TimeUp()
    {
        timeLeft = maxTimeLeft;

        Debug.Log("Time's up! Reloading level.");

        LoadCurrentLevel();
    }

    public float GetTimeRemaining()
    {
        return timeLeft;
    }

    public void SaveLevelProgress(int level)
    {
        CurrentLevel = Mathf.Clamp(level, 0, maxLevels);
        PlayerPrefs.SetInt(LEVEL_KEY, CurrentLevel);
        PlayerPrefs.Save();
    }

    public void LoadLevelProgress()
    {
        CurrentLevel = PlayerPrefs.GetInt(LEVEL_KEY, 0);
    }

    public void LoadCurrentLevel()
    {
        if (CurrentLevel == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Lvl" + CurrentLevel);
        }
    }

    public void CompleteLevel()
    {
        if (CurrentLevel < maxLevels)
        {
            CurrentLevel++;
            SaveLevelProgress(CurrentLevel);
        }

        LoadCurrentLevel();
    }

    public void ResetProgress()
    {
        CurrentLevel = 0;
        SaveLevelProgress(CurrentLevel);
        LoadCurrentLevel();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}