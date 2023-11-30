using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : MonoBehaviour
{
    public GameObject[] Boss_Attack; //보스 공격패턴 배열
    public bool Boss_Life = true;

   IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.5f);

        while (Boss_Life = true)
        {
            int ran = UnityEngine.Random.Range(0, 3);
            while (Boss_Life = true)
            if (ran == 0)
            {
              Boss_Attack[0].SetActive(true);
            }
            else if (ran == 1)
            {
               Boss_Attack[1].SetActive(true);
            }
            else if (ran == 2)
            {
               Boss_Attack[2].SetActive(true);
            }
            else if (ran == 3)
            {
               Boss_Attack[3].SetActive(true);
             }
        }
    }
}
