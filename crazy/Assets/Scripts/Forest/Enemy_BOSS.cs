using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_BOSS : MonoBehaviour
{
    public GameObject[] Boss_Attack; //보스 공격패턴 배열
    public bool Boss_Life = true;
    public GameManager gamemanager;
    bool attack;
    bool damaged;

    public Image realHpBar;
    float maxHp;
    float nowHp;

    void Start()
    {
        attack = false;
        damaged = false;

        maxHp = 10;
        nowHp = 10;
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

    IEnumerator canDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater") && !damaged)
        {
            damaged = true;
            nowHp -= 1;
            realHpBar.fillAmount = (float)nowHp / (float)maxHp;

            if(nowHp == 0)
            {
                Debug.Log("Die");
            }

            StartCoroutine(canDamaged());
        }    
    }
}
