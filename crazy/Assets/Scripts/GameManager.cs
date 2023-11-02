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


    public void Awake()
    {
        awin_Ani = GetComponent<Animator>();
        bwin_Ani = GetComponent<Animator>();
        draw_Ani = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();

        time = 180;
        StartCoroutine(StartTimer());

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

    public void Death(string playername)//Player A Death
    {
        if (playername == "A")
        {
            Debug.Log("player A Hit");
            plA = false;
            Invoke("DeathTimeFinish", 6f);

            // 바늘 사용 시, plA 값 true 로 초기화 필요
        }

        if (playername == "B")
        {
            Debug.Log("player B Hit");
            plB = false;
            Invoke("DeathTimeFinish", 6f);

            // 바늘 사용 시, plB 값 true 로 초기화 필요
        }

    }

    // 물풍선의 갇혀 있는 시간이 끝난 후 둘 중 하나라도 탈출을 못하면 승부 판정
    void DeathTimeFinish()
    {
        if (playerA.useniddle == true)
        {
            playerA.useniddle = false;
            plA = true;
        }
        else
        {
            if (plA == false || plB == false)
            {
                Judgment();
            }
        }

    }

    public async void Judgment()
    {
        //ui 승패 애니메이션 출력
        if (plA == false && plB == false && curTime <= 0.9)
        {
            
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("Draw");
            // draw_Ani.SetBool("draw", true);//드로우 애니메이션 실행
            screen.SetActive(true);
            draw.SetActive(true);
            
        }
        else if (plA == false && plB == true)
        {
            
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("player B Win");
            //bwin_Ani.SetBool("b", true);//플레이어b 애니메이션 실행
            screen.SetActive(true);
            bwin.SetActive(true);
            
        }
        else if (plA == true && plB == false)
        {
            Debug.Log("player A Win");
            // awin_Ani.SetBool("a", true);//플레이어a 애니메이션 실행
            screen.SetActive(true);
            awin.SetActive(true);
        }

    }

}
