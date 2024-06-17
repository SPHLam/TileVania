using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int playerLives = 3;
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
    }

    void ResetGameSession()
    {
        StartCoroutine(RestartGameDelay());
    }

    IEnumerator LoadSceneDelay(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator RestartGameDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
