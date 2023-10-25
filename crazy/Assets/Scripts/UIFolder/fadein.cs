using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadein : MonoBehaviour
{
    public UnityEngine.UI.Image fade_in;
    float fades = 0.0f;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (fades < 1.0f && time >= 0.1f)
        {
            fades += 0.1f;
            fade_in.color = new Color(0, 0, 0, fades);
            time = 0;
            
        }
        else if (fades >= 1.0f)
        {
            time = 0;
            SceneManager.LoadScene("BattelFieldVillage");
        }
    }
}