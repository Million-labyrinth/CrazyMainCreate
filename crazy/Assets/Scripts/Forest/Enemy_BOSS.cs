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
    public bool isDying;
    public bool isDead;
    float dyingTime;
    public GameObject[] Boss_Move_Attack;


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
        // ü���� 0 �� �� 4�ʰ� ������ ��� ����
        if (dyingTime >= 4f)
        {
            Dead();
        }
    }

    // ���� ������ �� ���� ����
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
            ray_active = false; // ���� ������ �� �� ������ Ray�� ����
            anim.SetBool("useSkills", false);
            yield return new WaitForSeconds(0.1f);
            Boss_Attack[ran].SetActive(false);
            attack = false;
        }
        else if (pick == 1)
        {
            int randomA = Random.Range(0, Boss_Attack.Length);
            int randomB;

            // �ߺ� ����
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
                    ray_active = false; // ���� ������ �� �� ������ Ray�� ����
                    anim.SetBool("useSkills", false);
                    yield return new WaitForSeconds(0.1f);
                    attack = false;
                    Boss_Attack[randomA].SetActive(false);
                    Boss_Attack[randomB].SetActive(false);
                }
            } while (randomA == randomB);
        }

        
    }

    // �ǰ� ������ ����
    IEnumerator canDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    // ��� �Լ�
    public void Dead()
    {
        anim.SetTrigger("isDead");
        isDead = true;
        StartCoroutine("Deactivation");
        dyingTime = 0;
        gamemanager.isFinishGame = true;
        gamemanager.PVEWinGame();
    }

    // ��� �� 1�� �� ���ӿ�����Ʈ ��Ȱ��ȭ
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
