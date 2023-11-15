using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDMgr : MonoBehaviour
{
    protected string strPlayer_1 = string.Empty;
    protected string strPlayer_2 = string.Empty;  

    protected virtual void Awake()
    {
        // 씬이 시작될 때 저장된 값을 불러옴
        strPlayer_1 = PlayerPrefs.GetString("PlayerName1", "");
        strPlayer_2 = PlayerPrefs.GetString("PlayerName2", "");
    }

    protected void DeletePlayer1()
    {
        PlayerPrefs.DeleteKey("PlayerName1");
    }

    protected void DeletePlayer2()
    {
        PlayerPrefs.DeleteKey("PlayerName2");
    }
}
