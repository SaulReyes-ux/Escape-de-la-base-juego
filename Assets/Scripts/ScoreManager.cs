using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int levelStartScore = 0; // Puntaje al inicio del nivel actual

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
        LoadScore();
        UpdateScoreText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText();
        SetLevelStartScore(); // Establecer el puntaje inicial para el nivel actual
        UpdateScoreText();
    }

    private void FindScoreText()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        SaveScore();
        UpdateScoreText();
    }

    public void ResetScoreOnDeath()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) // Primer nivel
        {
            ResetScore(); // Reinicia completamente el puntaje
        }
        else
        {
            score = levelStartScore; // Reinicia al puntaje guardado del inicio del nivel
        }
        SaveScore();
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        SaveScore();
        UpdateScoreText();
    }

    private void SetLevelStartScore()
    {
        levelStartScore = score; // Guarda el puntaje actual como el inicio del nivel
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Score");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
