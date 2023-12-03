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
    public bool isDying;
    public bool isDead;
    float dyingTime;


    public bool ray_active;
    public GameObject hpBar;
    public Image realHpBar;
    float maxHp;
    public float nowHp;

    public Animator anim;

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
        dyingTime = 0;

        maxHp = 10;
        nowHp = 10;
    }
    void Update()
    {
        if (!attack && !isDying && !damaged && gamemanager.startedGame)
        {
            StartCoroutine("AttackDelay");
        }

        if (isDying)
        {
            dyingTime += Time.deltaTime;
        }
        // 체력이 0 된 후 4초가 지나면 사망 판정
        if (dyingTime >= 4f)
        {
            Dead();
        }
    }

    // 공격 딜레이 및 패턴 설정
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
        else if (pick == 1)
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

    // 피격 딜레이 설정
    IEnumerator canDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    // 사망 함수
    public void Dead()
    {
        anim.SetTrigger("isDead");
        isDead = true;
        StartCoroutine("Deactivation");
        dyingTime = 0;
        gamemanager.isFinishGame = true;
        gamemanager.PVEWinGame();
    }

    // 사망 시 1초 후 게임오브젝트 비활성화
    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        hpBar.SetActive(false);
        isDying = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater") && !damaged)
        {
            damaged = true;
            nowHp -= 1;
            realHpBar.fillAmount = (float)nowHp / (float)maxHp;

            if (!isDying)
            {
                anim.SetTrigger("isDamaged");
            }

            if (nowHp <= 0)
            {
                anim.SetBool("isDying", true);
                isDying = true;
                StopCoroutine("AttackDelay");

            }

            StartCoroutine(canDamaged());
        }
    }
}
