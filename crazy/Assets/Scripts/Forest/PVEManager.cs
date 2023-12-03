using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVEManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] enemy;
    public int enemyCount;

    public Player playerA;
    public Player2 playerB;
    public GameObject bazzi;

    bool is2P;
    public ReStartButton restart;

    void Start()
    {
        enemyCount = enemy.Length;

        if(bazzi.activeInHierarchy)
        {
            is2P = true;
        } else
        {
            is2P = false;
        }
    }

    private void Update()
    {
        if(enemyCount == 0 && !gameManager.isFinishGame) {
            StartCoroutine(WinGame());
        }

        if(is2P && !gameManager.isFinishGame)
        {
            if ((playerA.playerDead && playerB.playerDead) || gameManager.timeOver)
            {
                StartCoroutine(LoseGame());
            }
        } else if(!is2P)
        {
            if (playerA.playerDead || gameManager.timeOver)
            {
                StartCoroutine(LoseGame());
            }
        }

    }

    IEnumerator WinGame()
    {
        gameManager.isFinishGame = true;
        yield return new WaitForSeconds(0.3f);
        gameManager.isFinishGame = true;
        gameManager.PVEWinGame();

        StopCoroutine(WinGame());

        yield return new WaitForSeconds(5f);
        // 다음 스테이지로 씬 이동
        if (SceneManager.GetActiveScene().name.Contains("1"))
        {
            SceneManager.LoadScene("ForestStage2");
        }
        else if (SceneManager.GetActiveScene().name.Contains("2"))
        {
            SceneManager.LoadScene("ForestStage3");
        }
        else if (SceneManager.GetActiveScene().name.Contains("3"))
        {
            goTitle();
        }
        else 
        {
           // gameManager.Wintitle();
        }
    }
    public void goTitle()
    {
        restart.GoPlayerRoom();
    }

    IEnumerator LoseGame()
    {
        gameManager.isFinishGame = true;
        yield return new WaitForSeconds(0.3f);
        gameManager.isFinishGame = true;
        gameManager.PVELoseGame();
        StopCoroutine(LoseGame());
        Debug.Log("lose");

        yield return new WaitForSeconds(5f);
        goTitle();
        // 맵 선택 창으로 이동

    }
}
