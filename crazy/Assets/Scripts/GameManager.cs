using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    public ObjectManager objectManager;
    public bool plA = true;
    public bool plB = true;

    public GameObject awin;
    public GameObject bwin;
    public GameObject draw;
    public GameObject screen;

    public GameObject redBlock;
    public GameObject orangeBlock;
    public GameObject villageBox;

    public Animator awin_Ani;
    public Animator bwin_Ani;
    public Animator draw_Ani;

    AudioSource audiosource;
    public AudioClip winSound;
    

    //timer
    [SerializeField] private Text text;
    [SerializeField] private float time;
    [SerializeField] private float curTime;
    int minute;
    int second;

    public Transform[] spawnPoints; // 플레이어 스폰 포인트

    bool isFinishGame = false;

    public void Awake()
    {
        awin_Ani = GetComponent<Animator>();
        bwin_Ani = GetComponent<Animator>();
        draw_Ani = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();

        time = 180;
        StartCoroutine(StartTimer());

    }

    void Start()
    {
        // 맵 제작 후 switch 문으로 변경 필요 (랜덤 변수 범위 설정)
        int randomA = Random.Range(0, spawnPoints.Length);
        int randomB = Random.Range(0, spawnPoints.Length);

        // 중복 제거
        if(randomA == randomB)
        {
            if(randomB == 0)
            {
                randomB = spawnPoints.Length - 1;
            } else
            {
                randomB--;
            }
        }

        playerA.transform.position = spawnPoints[randomA].position;
        playerB.transform.position = spawnPoints[randomB].position;
    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime <= 0.9)
            {
                Debug.Log("Time out Draw");
                //draw_Ani.SetBool("draw", true);
                screen.SetActive(true);
                draw.SetActive(true);
                curTime = 0;
                yield break;
            }
        }
    }

    public void Death()
    {
        Invoke("Judgment", 0.3f);
    }

    public async void Judgment()
    {
        //ui 승패 애니메이션 출력

        // Draw
        if (playerA.playerDead == true && playerB.playerDead == true || curTime <= 0.9)
        {
            
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("Draw");
            // draw_Ani.SetBool("draw", true);//드로우 애니메이션 실행
            screen.SetActive(true);
            draw.SetActive(true);
        }
        // B Win
        else if (playerA.playerDead == true && playerB.playerDead == false)
        {
            
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("player B Win");
            //bwin_Ani.SetBool("b", true);//플레이어b 애니메이션 실행
            screen.SetActive(true);
            bwin.SetActive(true);

            playerB.dyingTime = 0;

            if (playerB.isDying)
            {
                playerB.anim.SetTrigger("finishGame"); // 애니메이션 추가 필요
                playerB.isDying = false;
            }
        }
        // A Win
        else if (playerA.playerDead == false && playerB.playerDead == true)
        {
            Debug.Log("player A Win");
            // awin_Ani.SetBool("a", true);//플레이어a 애니메이션 실행
            screen.SetActive(true);
            awin.SetActive(true);

            playerA.dyingTime = 0;

            if(playerA.isDying)
            {
                playerA.anim.SetTrigger("finishGame"); // 애니메이션 추가 필요
                playerA.isDying = false;
            }
        }

        isFinishGame = true;
        playerA.anim.SetBool("isDamaged", false);
        playerB.anim.SetBool("isDamaged", false);
        playerA.playerSpeed = 4.0f;
        playerB.playerSpeed = 4.0f;
    }

}
