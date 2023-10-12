using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    int pla = 0;
    int plb = 0;

    public void Death(string playername)
    {
       if(playername == "A")
        {
           // Debug.Log("player B Win");
            pla = 1;
            Judgment();
        }
        
    }
    public void Death2(string playername2)
    {
        if (playername2 == "B")
        {
            
            //Debug.Log("player A Win");
            plb = 1;
            Judgment();
        }
    }
    public void Judgment()
    {
        if(pla == plb)
        {
            Debug.Log("Draw");
        }
        if (pla == 1)
        {
            Debug.Log("player B Win2");
        }
        if (plb == 1)
        {
            Debug.Log("player A Win2");
        }

    }
}
