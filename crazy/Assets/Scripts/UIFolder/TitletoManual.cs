using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitletoManual : MonoBehaviour 
{
    public Button ttg1;
    public Button ttm1;
    public Button ttg2;
    public Button ttm2;

    public GameObject title;
    public GameObject manual;
    public GameObject map;
    public GameObject ttg_1;
    public GameObject ttg_2;
    public GameObject ttm_1;
    public GameObject ttm_2;
    public GameObject playerRoom;

    public void title_to_manual()
    {
        title.SetActive(false);
        manual.SetActive(true);

    }
    public void title_to_game()
    {
        title.SetActive(false);
        manual.SetActive(false);
        map.SetActive(true);
    }
}
