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
    public GameObject screen;

    public GameObject redBlock;
    public GameObject orangeBlock;

    public Animator anim;


    void Awake()
    {
        Invoke("TimeEND", 180);

        Block redBlockLogic = redBlock.GetComponent<Block>();
        Block orangeBlockLogic = orangeBlock.GetComponent<Block>();
        redBlockLogic.objectManager = objectManager;
        redBlockLogic.playerA = playerA;
        redBlockLogic.playerB = playerB;
        orangeBlockLogic.objectManager = objectManager;
        orangeBlockLogic.playerA = playerA;
        orangeBlockLogic.playerB = playerB;
        anim = GetComponent<Animator>();
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
            anim.SetBool("draw", true);
            screen.SetActive(true);
        }
        else if (plA == false && plB == true)
        {
            Debug.Log("player B Win");
            anim.SetBool("b", true);
            screen.SetActive(true);
        }
        else if (plA == true && plB == false)
        {
            Debug.Log("player A Win");
            anim.SetBool("a", true);
            screen.SetActive(true);
        }

    }
    void TimeEND()
    {
        //UI활성화 또는 SEEN교체
        Debug.Log("Time Out Draw");

    }
}
