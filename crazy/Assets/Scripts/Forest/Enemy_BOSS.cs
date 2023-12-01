using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_BOSS : MonoBehaviour
{
    public GameObject[] Boss_Attack; //���� �������� �迭
    public bool Boss_Life = true;
    public GameManager gamemanager;
    bool attack;
    bool damaged;


    public bool ray_active;
    public Image realHpBar;
    float maxHp;
    float nowHp;

    void Start()
    {
        attack = false;
        damaged = false;
        ray_active = false;

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
        yield return new WaitForSeconds(2f);
        int pick = UnityEngine.Random.Range(0, 2);
        if (pick == 0)
        {
            int ran = UnityEngine.Random.Range(0, Boss_Attack.Length);
            Boss_Attack[ran].SetActive(true);
            yield return new WaitForSeconds(1f);
            ray_active = true;
            yield return new WaitForSeconds(0.85f);
            Boss_Attack[ran].SetActive(false);
            attack = false;
            ray_active = false;
        }
        else 
        {
            int randomA = Random.Range(0, Boss_Attack.Length);
            int randomB = Random.Range(0, Boss_Attack.Length);

            // �ߺ� ����
            if (randomA == randomB)
            {
                while (randomA != randomB)
                {
                    randomB = Random.Range(0, Boss_Attack.Length);
                }
              }
            else if (randomA != randomB)
            {
                Boss_Attack[randomA].SetActive(true);
                Boss_Attack[randomB].SetActive(true);
                yield return new WaitForSeconds(1f);
                ray_active = true;
                yield return new WaitForSeconds(0.85f);
                Boss_Attack[randomA].SetActive(false);
                Boss_Attack[randomB].SetActive(false);
                attack = false;
                ray_active = false;
            }
        }
    }

    IEnumerator canDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater") && !damaged)
        {
            damaged = true;
            nowHp -= 1;
            realHpBar.fillAmount = (float)nowHp / (float)maxHp;

            if (nowHp == 0)
            {
                Debug.Log("Die");
            }

            StartCoroutine(canDamaged());
        }
    }
}
