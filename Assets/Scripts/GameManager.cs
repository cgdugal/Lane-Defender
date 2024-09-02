using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalHighScoreText;
    [SerializeField] private GameObject newHighScoreMarker;

    [SerializeField] private PlayerController pc;
    [SerializeField] private EnemyManager em;

    [SerializeField] private AudioSource lifeLostSound;

    private int lives = 3;
    private int score = 0;

    private bool newHighScore = false;

    public List<EnemyBehavior> enemies = new List<EnemyBehavior>();
    
    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveLife()
    {
        lives--;
        lifeLostSound.Play();

        if(lives <= 0)
        {
            lives = 0;
            EndGame();
        }
        
        livesText.text = "Lives: " + lives;
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
            newHighScore = true;
        }

    }
    
    void EndGame()
    {
        em.StopSpawning();
        pc.RemoveControl();

        finalScoreText.text = "Score: " +score;
        finalHighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        
        if(newHighScore)
        {
            newHighScoreMarker.SetActive(true);
        }
        gameOverScreen.SetActive(true);

        livesText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Freeze();
        }
    }
}
