using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int playerLives = 3;
    int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numsOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numsOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            PlayerDamage();
        }
        else
        {
            ResetGameSession();
        }
    }

    void PlayerDamage()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneDelay(currentSceneIndex));
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        StartCoroutine(RestartGameDelay());
    }

    void UpdatingUI()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    IEnumerator LoadSceneDelay(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator RestartGameDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        score = 0;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddPoint()
    {
        score += 100;
        scoreText.text = score.ToString();
    }
}
