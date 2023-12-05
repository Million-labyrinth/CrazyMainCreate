using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    public ObjectManager objectManager;

    public GameObject awin;
    public GameObject bwin;
    public GameObject draw;
    public GameObject screen;
    public GameObject Namespace;
    public GameObject dao;
    public GameObject bazzi;
    public GameObject AObj;
    public GameObject BObj;

    public GameObject PVEClear;
    public GameObject PVELose;

    public GameObject redBlock;
    public GameObject orangeBlock;
    public GameObject villageBox;

    public Animator awin_Ani;
    public Animator bwin_Ani;
    public Animator draw_Ani;

    AudioSource audiosource;
    public AudioClip winSound;
    public AudioClip enemyWinSound;
    public AudioClip loseSound;
    public bool isBlinking = false; // 애니메이션 깜빡임 상태
    public float blinkInterval = 0.2f; // 깜빡이는 간격

    public string gameMode;

    public Text bossMap;//보스맥 확인
    //timer
    [SerializeField] private Text text;
    [SerializeField] private float time;
    [SerializeField] private float curTime;
    public bool timeOver;
    int minute;
    int second;

    public Transform[] spawnPoints; // 플레이어 스폰 포인트

    public bool isFinishGame;
    public GameObject loadingCanvas;
    public bool startedGame;

    public ReStartButton restart;


    public void Awake()
    {
        awin_Ani = GetComponent<Animator>();
        bwin_Ani = GetComponent<Animator>();
        draw_Ani = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();

        time = 180;
        StartCoroutine(StartTimer());

        isFinishGame = false;
        startedGame = false;
        loadingCanvas.SetActive(true);
        timeOver = false;
    }

    void Start()
    {
        // 플레이어 스폰
        int randomA = Random.Range(0, spawnPoints.Length);
        int randomB;

        // 중복 제거
        do
        {
            randomB = Random.Range(0, spawnPoints.Length);

            if (randomA != randomB)
            {
                playerA.transform.position = spawnPoints[randomA].position;
                playerB.transform.position = spawnPoints[randomB].position;
                break;
            }
        } while (randomA == randomB);

        playerA.collider.enabled = false;
        playerB.collider.enabled = false;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        startedGame = true;
        loadingCanvas.SetActive(false);
        playerA.collider.enabled = true;
        playerB.collider.enabled = true;
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
                audiosource.clip = loseSound; // lose Sound 필요
                audiosource.Play();
                //Wintitle();
                //draw_Ani.SetBool("draw", true);
                StartWinAnimation(draw);
                draw.SetActive(true);
                curTime = 0;
                isFinishGame = true;
                timeOver = true;
                yield return new WaitForSeconds(0.3f);
                Judgment();
                yield break;
            }
        }
    }

    public void Death()
    {

        Invoke("Judgment", 0.3f);
    }

    // PVP 판정
    public async void Judgment()
    {
        
        

        // Draw
        if ((playerA.playerDead == true && playerB.playerDead == true ) || timeOver)
        {
            audiosource.clip = loseSound; // lose Sound 필요
            audiosource.Play();
            Debug.Log("Draw");
            // draw_Ani.SetBool("draw", true);//드로우 애니메이션 실행
            StartWinAnimation(draw);
            draw.SetActive(true);
            
        }
        // B Win
        else if (playerA.playerDead == true && playerB.playerDead == false)
        {
            //ui 승패 애니메이션 출력
            Namespace.SetActive(true);
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("player B Win");
            //bwin_Ani.SetBool("b", true);//플레이어b 애니메이션 실행
            StartWinAnimation(bwin);
            bwin.SetActive(true);
            BObj.SetActive(true);
            playerB.playerSpeed = 0;

            playerB.dyingTime = 0;

            if (playerB.isDying)
            {
                playerB.isDying = false;
            }

            playerB.anim.SetTrigger("finishGame"); // 애니메이션 추가 필요
        }
        // A Win
        else if (playerA.playerDead == false && playerB.playerDead == true)
        {
            //ui 승패 애니메이션 출력
            Namespace.SetActive(true);
            audiosource.clip = winSound;
            audiosource.Play();
            Debug.Log("player A Win");
            // awin_Ani.SetBool("a", true);//플레이어a 애니메이션 실행
            StartWinAnimation(awin);
            awin.SetActive(true);
            AObj.SetActive(true);
            playerA.dyingTime = 0;
            playerA.playerSpeed = 0;

            if (playerA.isDying)
            {
                playerA.isDying = false;
            }

            playerA.anim.SetTrigger("finishGame"); // 애니메이션 추가 필요
        }

        Wintitle();
        // 끝나면 바뀐 값들 초기화(나중에 함수로 만들어서 재시작시 초기화)
        isFinishGame = true;
        playerA.anim.SetBool("isDamaged", false);
        playerB.anim.SetBool("isDamaged", false);
    }
    public void GoRoom()
    {
        restart.GoPlayerRoom();
    }
    public void Wintitle()
    {
        Invoke("GoRoom", 5f);
    }
    public void PVEWinGame()
    {
        audiosource.clip = winSound;
        audiosource.Play();
        Debug.Log("PVEClear");
        StartWinAnimation(PVEClear);
        PVEClear.SetActive(true);

        if (!playerA.playerDead)
        {
            playerA.anim.SetTrigger("finishGame");
            playerA.playerSpeed = 0;
        }
        if(!playerB.playerDead)
        {
            playerB.anim.SetTrigger("finishGame");
            playerB.playerSpeed = 0;
        }
    }

    public void PVELoseGame()
    {
        if (bossMap.text.Contains("03"))
        {
            audiosource.clip = enemyWinSound; // lose Sound 필요
            audiosource.Play();
        }
        else
        {
            audiosource.clip = loseSound; // lose Sound 필요
            audiosource.Play();
        }


        Debug.Log("PVELose");
        StartWinAnimation(PVELose);
        PVELose.SetActive(true);
    }

    Coroutine coroutineWin = null;
    private void StartWinAnimation(GameObject go, float stopTime = 2f) //승리 애니메이션 나오는 코드(코루틴이 두번 실행되서 고친코드)
    {
        if (coroutineWin != null)
            StopCoroutine(coroutineWin);

        coroutineWin = StartCoroutine(BlinkAnimation(go));
    }

    //깜빡이는 기능
    private IEnumerator BlinkAnimation(GameObject objToBlink, float stopTime = 2f)
    {
        screen.SetActive(true);
        objToBlink.SetActive(true);
        float time = 0;
        while (time < stopTime)
        {
            time += Time.unscaledDeltaTime;

            objToBlink.SetActive(!objToBlink.activeSelf); // 활성/비활성 교대로 변경
            yield return new WaitForSeconds(blinkInterval);
        }

        objToBlink.SetActive(true);
        coroutineWin = null;
    }


    public void StopBlinkingAnimation()// 깜빡임 애니메이션을 중지
    {
        isBlinking = false;  // 깜빡임 애니메이션을 중지

    }

}
