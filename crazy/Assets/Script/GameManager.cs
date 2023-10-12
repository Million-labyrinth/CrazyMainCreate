using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;


    public void Death(string playername)
    {
       if(playername == "A")
        {
            Debug.Log("player B Win");
           
        }
        
    }
    public void Death2(string playername2)
    {
        if (playername2 == "B")
        {
            Debug.Log("player A Win");
        }
    }
    public void Draw()
    {
        /*if (playername == "A" && playername2 == 1)
        {
            Debug.Log("Draw");
        }*/
    }
}
