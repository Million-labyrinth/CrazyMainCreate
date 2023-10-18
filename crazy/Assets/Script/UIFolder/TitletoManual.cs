using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitletoManual : MonoBehaviour
{
    public Button ttg;
    public Button ttm;

    public GameObject title;
    public GameObject manual;
    public GameObject map;
    // Start is called before the first frame update

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
