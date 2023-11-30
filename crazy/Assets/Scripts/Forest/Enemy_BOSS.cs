using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : MonoBehaviour
{
    public GameObject[] Boss_Attack; //보스 공격패턴 배열
    public bool Boss_Life = true;

   IEnumerator AttackDelay()
    {

        while (Boss_Life = true)
        {
            int ran = UnityEngine.Random.Range(0, 3);

            if (ran == 0)
            {
                
            }
            else if (ran == 1)
            {
               
            }
            else if (ran == 2)
            {

            }
            else if (ran == 3)
            {

            }
        }
        yield return;  
    }
}
