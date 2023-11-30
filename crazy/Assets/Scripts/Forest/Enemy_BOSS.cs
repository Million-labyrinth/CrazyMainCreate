using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : MonoBehaviour
{
    public GameObject[] Boss_Attack; //보스 공격패턴 배열
    public bool Boss_Life = true;
    public GameManager gamemanager;
    bool attack;

    void Start()
    {
        attack = false;
    }
    void Update()
    {
        if (!attack)
        {
            StartCoroutine("AttackDelay");
        }
    }

    IEnumerator AttackDelay()
    {
        attack = true;
        yield return new WaitForSeconds(0.5f);
        int ran = UnityEngine.Random.Range(0, 4);
        if (ran == 0)
        {
            Boss_Attack[0].SetActive(true);
            yield return new WaitForSeconds(0.45f);
            Boss_Attack[0].SetActive(false);
            attack = false;
        }
        else if (ran == 1)
        {
            Boss_Attack[1].SetActive(true);
            yield return new WaitForSeconds(0.45f);
            Boss_Attack[1].SetActive(false);
            attack = false;
        }
        else if (ran == 2)
        {
            Boss_Attack[2].SetActive(true);
            yield return new WaitForSeconds(0.45f);
            Boss_Attack[2].SetActive(false);
            attack = false;
        }
        else if (ran == 3)
        {
            Boss_Attack[3].SetActive(true);
            yield return new WaitForSeconds(0.45f);
            Boss_Attack[3].SetActive(false);
            attack = false;
        }

    }
}
