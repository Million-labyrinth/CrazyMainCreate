using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    public ObjectManager objectManager;
    bool plA = true;
    bool plB = true;

    public GameObject redBlock;
    public GameObject orangeBlock;

    void Awake(){
        Invoke("TimeEND",180);

        Block redBlockLogic = redBlock.GetComponent<Block>();
        Block orangeBlockLogic = orangeBlock.GetComponent<Block>();
        redBlockLogic.objectManager = objectManager;
        redBlockLogic.playerA = playerA;
        redBlockLogic.playerB = playerB;
        orangeBlockLogic.objectManager = objectManager;
        orangeBlockLogic.playerA = playerA;
        orangeBlockLogic.playerB = playerB;
    }

    public void Death(string playername)//Player A Death
    {
       if(playername == "A")
        {
           Debug.Log("player A Hit");
           plA = false;

            // 바늘 사용 시, plA 값 true 로 초기화 필요
        }

        if (playername == "B")
        {
            Debug.Log("player B Hit");
            plB = false;

            // 바늘 사용 시, plB 값 true 로 초기화 필요
        }

        Invoke("DeathTimeFinish", 1f);

    }

    // 물풍선의 갇혀 있는 시간이 끝난 후 둘 중 하나라도 탈출을 못하면 승부 판정
    void DeathTimeFinish()
    {
        if(plA == false || plB == false)
        {
            Judgment();
        }
    }

    public void Judgment()
    {
        //ui 승패 애니메이션 출력
        if (plA == false && plB == false)
        {
            Debug.Log("Draw");
        }
        else if (plA == false && plB == true)
        {
            Debug.Log("player B Win");
        }
        else if (plA == true && plB == false)
        {
            Debug.Log("player A Win");
        }

    }
    void TimeEND(){
        //UI활성화 또는 SEEN교체
        Debug.Log("Time Out Draw");

    }
}
