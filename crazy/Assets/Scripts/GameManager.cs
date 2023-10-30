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
    public void Awake()
    {
        awin_Ani = GetComponent<Animator>();
        bwin_Ani = GetComponent<Animator>();
        draw_Ani = GetComponent<Animator>();
        Invoke("TimeEND", 180);

        Block redBlockLogic = redBlock.GetComponent<Block>();
        Block orangeBlockLogic = orangeBlock.GetComponent<Block>();
        Block villageBoxLogic = villageBox.GetComponent<Block>();
        redBlockLogic.objectManager = objectManager;
        orangeBlockLogic.objectManager = objectManager;
        villageBoxLogic.objectManager = objectManager;

    }

    public void Death(string playername)//Player A Death
    {
        if (playername == "A")
        {
            Debug.Log("player A Hit");
            plA = false;
            Invoke("DeathTimeFinish", 3f);

            // 바늘 사용 시, plA 값 true 로 초기화 필요
        }

        if (playername == "B")
        {
            Debug.Log("player B Hit");
            plB = false;
            Invoke("DeathTimeFinish", 3f);

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
        if (plA == false && plB == false)
        {
            Debug.Log("Draw");
            // draw_Ani.SetBool("draw", true);//드로우 애니메이션 실행
            screen.SetActive(true);
            draw.SetActive(true);
        }
        else if (plA == false && plB == true)
        {
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
    public void TimeEND()
    {
        //UI활성화 또는 SEEN교체
        Debug.Log("Time Out Draw");
        Debug.Log("Draw");
        //draw_Ani.SetBool("draw", true);
        screen.SetActive(true);
        draw.SetActive(true);

    }
}
