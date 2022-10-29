using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool gameStarted;

    public GameObject menuUI;
    public GameObject platformSpawner;
    public GameObject gamePlayUI;

    public TextMeshProUGUI scoreText;
    public Text highScoreText;

    int score = 0;
    int highScore;

    AudioSource audioSource;
    public AudioClip[] gameMusic;
    //public AudioClip gameMenuMusic;
    //public AudioClip soundEffect;

    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {

        highScore = PlayerPrefs.GetInt("HighScore");

        highScoreText.text = "BestScore: " + highScore;

    }

    // Update is called once per frame
    void Update()
    {

        if (!gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }

    }

    public void GameStart()
    {
        gameStarted = true;

        platformSpawner.SetActive(true);

        menuUI.SetActive(false);
        gamePlayUI.SetActive(true);

        StartCoroutine("UpdateScore");
    }

    public void GameOver()
    {
        platformSpawner.SetActive(false);

        Invoke("ReloadLevel", 1f);

        SaveHighScore();

        StopCoroutine("UpdateScore");
    }

    void ReloadLevel()
    {

        SceneManager.LoadScene("Game");

    } 

    IEnumerator UpdateScore()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            score++;

            scoreText.text = score.ToString();

            //print(score);
        }

    }
    
    public void IncrementScore()
    {

        score += 20;

        scoreText.text = score.ToString();
        //score = score + 20

        audioSource.PlayOneShot(gameMusic[1]);
    }


    void SaveHighScore()
    {

        if (PlayerPrefs.HasKey("HighScore"))
        {
            //we already have a highscore

            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

        }

        else
        {
            //playing for the first time

            PlayerPrefs.SetInt("HighScore", score);

        }

    }

}
