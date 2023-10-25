using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitletoManual : MonoBehaviour 
{
    public Button ttg1;
    public Button ttm1;

    public GameObject title;
    public GameObject manual;
    public GameObject map;
    public GameObject ttg_1;
    public GameObject ttm_1;
    GameObject FadePannel;

    // Start is called before the first frame update

    private void Start()
    {
        title.SetActive(true);
        manual.SetActive(false);
        map.SetActive(false);
    }
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

    //페이드 아웃
    public IEnumerator FadeInStart()
    {
        FadePannel.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);
    }

    //페이드 인
    public IEnumerator FadeOutStart()
    {
        FadePannel.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
