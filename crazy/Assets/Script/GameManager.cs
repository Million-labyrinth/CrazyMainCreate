using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    public ObjectManager objectManager;
    bool plA;
    bool plB;

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
           Debug.Log("player B Win");
            plA = false;
            Judgment();
            //ui 승패 애니메이션 출력
        }
        
    }
    public void Death2(string playername2)//Player B Death
    {
        if (playername2 == "B")
        {
            
            Debug.Log("player A Win");
            plB = false;
            Judgment();
        }
    }
    public void Judgment()
    {
        if(plA == plB)
        {
            Debug.Log("Draw");
        }
        if (plA == false)
        {
            Debug.Log("player B Win2");
        }
        if (plB == false)
        {
            Debug.Log("player A Win2");
        }

    }
    void TimeEND(){
        //UI활성화 또는 SEEN교체
        Debug.Log("Time Out Draw");

    }
}
