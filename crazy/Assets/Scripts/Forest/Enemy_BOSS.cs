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
    bool isDying;
    bool isDead;


    public bool ray_active;
    public Image realHpBar;
    float maxHp;
    float nowHp;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Start()
    {
        attack = false;
        damaged = false;
        ray_active = false;
        isDying = false;
        isDead = false;

        maxHp = 10;
        nowHp = 1;
    }
    void Update()
    {
        if (!attack && !isDying && !damaged)
        {
            StartCoroutine("AttackDelay");
        }
    }

    IEnumerator AttackDelay()
    {
        attack = true;
        yield return new WaitForSeconds(2f);
        int pick = Random.Range(0, 2);
        if (pick == 0)
        {
            int ran = Random.Range(0, Boss_Attack.Length);
            Boss_Attack[ran].SetActive(true);
            yield return new WaitForSeconds(1.4f);
            ray_active = true;
            anim.SetBool("useSkills", true);
            yield return new WaitForSeconds(0.4f);
            ray_active = false; // 판정 때문에 좀 더 빠르게 Ray만 종료
            anim.SetBool("useSkills", false);
            yield return new WaitForSeconds(0.1f);
            Boss_Attack[ran].SetActive(false);
            attack = false;
        }
        else if(pick == 1)
        {
            int randomA = Random.Range(0, Boss_Attack.Length);
            int randomB;

            // 중복 제거
            do
            {
                randomB = Random.Range(0, Boss_Attack.Length);

                if (randomA != randomB)
                {
                    Boss_Attack[randomA].SetActive(true);
                    Boss_Attack[randomB].SetActive(true);
                    yield return new WaitForSeconds(1.4f);
                    ray_active = true;
                    anim.SetBool("useSkills", true);
                    yield return new WaitForSeconds(0.4f);
                    ray_active = false; // 판정 때문에 좀 더 빠르게 Ray만 종료
                    anim.SetBool("useSkills", false);
                    yield return new WaitForSeconds(0.1f);
                    attack = false;
                    Boss_Attack[randomA].SetActive(false);
                    Boss_Attack[randomB].SetActive(false);
                }
            } while (randomA == randomB);
        }
    }

    IEnumerator canDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    void Dead()
    {
        anim.SetTrigger("isDead");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("B_dead") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater") && !damaged)
        {
            damaged = true;
            nowHp -= 1;
            realHpBar.fillAmount = (float)nowHp / (float)maxHp;
            anim.SetTrigger("isDamaged");

            if (nowHp == 0)
            {
                anim.SetTrigger("isDying");
                isDying = true;

                Dead();
            }

            StartCoroutine(canDamaged());
        }

        if(isDying)
        {
            if(collision.gameObject.tag == "PlayerA" || collision.gameObject.tag == "PlayerB")
            {
                Dead();
            }
        }
    }
}
