using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    bool plA;
    bool plB;

    void Awake(){
        Invoke("TimeEND",180);
    }

    public void Death(string playername)//Player A Death
    {
       if(playername == "A")
        {
           // Debug.Log("player B Win");
            plA = false;
            Judgment();
        }
        
    }
    public void Death2(string playername2)//Player B Death
    {
        if (playername2 == "B")
        {
            
            //Debug.Log("player A Win");
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
        else if (plA == false)
        {
            Debug.Log("player B Win2");
        }
        else if (plB == false)
        {
            Debug.Log("player A Win2");
        }

    }
    void TimeEND(){
        //UI활성화 또는 SEEN교체
        Debug.Log("Time Out Draw");

    }
}
