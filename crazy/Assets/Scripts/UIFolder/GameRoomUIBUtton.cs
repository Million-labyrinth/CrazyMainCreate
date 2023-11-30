using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameRoomUIBUtton : MonoBehaviour
{
    public GameObject MapSelectButton;
    public GameObject GameExitAlert;
    public GameObject GameTitleAlert;
    public GameObject PlayerRoom;
    public GameObject P1UION;
    public GameObject BackBoard;
    public GameObject bazzi;

    public bool player_1 = true;


    public static GameRoomUIBUtton gameRoom;



    private void Start()
    {
        if (bazzi.activeSelf == true)
        {
            UI_IDinput_Title.inputTitle.is1P = false;
            UI_IDinput_Title.inputTitle.is2P = true;
        }
        else
        {
            UI_IDinput_Title.inputTitle.is1P = true;
            UI_IDinput_Title.inputTitle.is2P = false;
        }

    }

    public void GameExitAlertButton()
    {
        GameExitAlert.SetActive(true);
        BackBoard.SetActive(true);

    }
    public void GameExitAlertInButton()
    {
        Application.Quit();
        Debug.Log("게임종료!");
    }

    public void MapSelectAlertButton()
    {
        MapSelectButton.SetActive(true);
        BackBoard.SetActive(true);
    }

    public void GoTitleAlert()
    {
        GameTitleAlert.SetActive(true);
        BackBoard.SetActive(true);
    }
    public void GoTitleAlertInButton()
    {
        PlayerRoom.SetActive(false);
        GameTitleAlert.SetActive(false);
        P1UION.SetActive(true);
    }

}
