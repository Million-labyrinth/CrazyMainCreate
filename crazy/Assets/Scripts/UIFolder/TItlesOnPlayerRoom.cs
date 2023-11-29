using System.Data;
using UnityEngine;
using UnityEngine.UI;


public class TitlesOnPlayerRoom : MonoBehaviour
{
    public GameObject playerRoom;
    public IDMgr idmgr;
    public static bool shouldInitialize = true; // �ʱ�ȭ�� �������� ���θ� �����ϴ� ����


    // public GameObject Player2;

    void Awake()
    {
        if (shouldInitialize == true)
        {
            // �ʱ�ȭ �ڵ� ����
            idmgr.DeletePlayer1();
            idmgr.DeletePlayer2();

            // �ʱ�ȭ�� �������Ƿ� false�� ����
            shouldInitialize = false;
        }

        if (playerRoom != null)
        {
            int activatePlayerRoom = PlayerPrefs.GetInt("PlayerRoomState", 0);

            if (activatePlayerRoom == 2)
            {
                playerRoom.SetActive(true);

                // ���� �ʱ�ȭ
                PlayerPrefs.SetInt("PlayerRoomState", 0);
                PlayerPrefs.Save();


            }
        }
    }
}