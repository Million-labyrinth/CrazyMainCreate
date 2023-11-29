using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] enemy;
    public int enemyCount;


    void Awake()
    {
        enemyCount = enemy.Length;
    }

    private void Update()
    {
        if(enemyCount == 0) {
            StartCoroutine(WinGame());
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.3f);
        gameManager.isFinishGame = true;
        gameManager.PVEJudgment();

        StopCoroutine(WinGame());
    }
}
