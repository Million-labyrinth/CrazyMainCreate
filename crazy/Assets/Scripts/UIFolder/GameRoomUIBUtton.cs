using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomUIBUtton : MonoBehaviour
{
    public GameObject MapSelectButton;
    public GameObject GameExitAlert;
    public GameObject GameTitleAlert;
    public GameObject PlayerRoom;
    public GameObject P1UION;

    public void GameExitAlertButton()
    {
        GameExitAlert.SetActive(true);
    }
    public void GameExitAlertInButton()
    {
        Application.Quit();
        Debug.Log("게임종료!");
    }

    public void MapSelectAlertButton()
    {
        MapSelectButton.SetActive(true);

    }

    public void GoTitleAlert()
    {
        GameTitleAlert.SetActive(true);
    }
    public void GoTitleAlertInButton()
    {
        PlayerRoom.SetActive(false);
        GameTitleAlert.SetActive(false);
        P1UION.SetActive(true);
    }

}
