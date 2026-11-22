using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState { Ready, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Ready;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject startPanel;
    public GameObject gameOverPanel;

    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip hitSound;
    AudioSource audioSource;

    int score;

    void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        scoreText.text = "0";
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (State == GameState.Ready)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                StartGame();
        }
        else if (State == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
        }
    }

    void StartGame()
    {
        State = GameState.Playing;
        startPanel.SetActive(false);
        PlayFlap();
    }

    public void PlayFlap()
    {
        if (flapSound != null) audioSource.PlayOneShot(flapSound);
    }

    public void AddScore()
    {
        if (State != GameState.Playing) return;
        score++;
        scoreText.text = score.ToString();
        if (scoreSound != null) audioSource.PlayOneShot(scoreSound);
    }

    public void GameOver()
    {
        if (State == GameState.GameOver) return;
        State = GameState.GameOver;
        scoreText.gameObject.SetActive(false);

        if (hitSound != null) audioSource.PlayOneShot(hitSound);

        int best = PlayerPrefs.GetInt("Best", 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Best", best);
        }
        finalScoreText.text = "Score: " + score;
        bestScoreText.text = "Best: " + best;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}   