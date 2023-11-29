using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] enemy;
    public int enemyCount;

    public Player playerA;
    public Player2 playerB;


    void Awake()
    {
        enemyCount = enemy.Length;
    }

    private void Update()
    {
        if(enemyCount == 0) {
            StartCoroutine(WinGame());
        }

        if((playerA.playerDead && playerB.playerDead) || gameManager.timeOver)
        {
            StartCoroutine(LoseGame());
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.3f);
        gameManager.isFinishGame = true;
        gameManager.PVEWinGame();

        StopCoroutine(WinGame());
    }

    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
