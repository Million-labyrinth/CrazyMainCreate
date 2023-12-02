using System.Data;
using UnityEngine;
using UnityEngine.UI;


public class TitlesOnPlayerRoom : MonoBehaviour
{
    public string PVPorPVE;
    public GameObject playerRoom;
    public GameObject Player2;
    public GameObject Player2XIMG;
    public GameObject PVPMap1;
    public GameObject PVPMap2;
    public GameObject PVPMap3;
    public GameObject PVPMap4;
    public GameObject PVPMap5;
    public GameObject PVEMap;
    public IDMgr idmgr;
    public static bool shouldInitialize = true; // 초기화를 진행할지 여부를 결정하는 변수
    public GameObject mapSelectBtn;
    public GameObject mapSelectBtnClose;
    public GameObject PVEMapSelect;
    public GameObject Pvemapicon;
    public GameObject random;
    public GameObject randommapicon;

    // public GameObject Player2;

    void Awake()
    {
        if (shouldInitialize == true)
        {
            // 초기화 코드 실행
            idmgr.DeletePlayer1();
            idmgr.DeletePlayer2();

            // 초기화가 끝났으므로 false로 변경
            shouldInitialize = false;
        }

        if (playerRoom != null)
        {
            int activatePlayerRoom = PlayerPrefs.GetInt("PlayerRoomState", 0);

            if (activatePlayerRoom == 2)
            {
                playerRoom.SetActive(true);
                Player2.SetActive(false);
                Player2XIMG.SetActive(true);
                PVPorPVE = "PVE";
                idmgr.DeletePlayer2();

                PlayerPrefs.SetString("PVPorPVE", PVPorPVE);
                PlayerPrefs.Save();
                mapSelectBtn.SetActive(false);
                mapSelectBtnClose.SetActive(true);
                PVEMapSelect.SetActive(true);
                random.SetActive(false);
                Pvemapicon.SetActive(true);
                randommapicon.SetActive(false);

                PVEMap.SetActive(true);
                DeactivePVPMap();

                // 값을 초기화
                PlayerPrefs.SetInt("PlayerRoomState", 0);
                PlayerPrefs.Save();


            }
            else if (activatePlayerRoom == 3)
            {
                playerRoom.SetActive(true);
            }

        }
    }
    public void DeactivePVPMap()
    {
        PVPMap1.SetActive(false);
        PVPMap2.SetActive(false);
        PVPMap3.SetActive(false);
        PVPMap4.SetActive(false);
        PVPMap5.SetActive(false);

    }
}