using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    public UnityEngine.UI.Image fade_out;
    float fades = 1.0f;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if(fades > 0.0f && time >= 0.2f)
        {
            fades -= 0.1f;
            fade_out.color = new Color(0, 0, 0, fades);
            time = 0;
        }
        else if(fades <= 0.0f)
        {
            time = 0;
        }
    }
}
