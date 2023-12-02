using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idcheck : MonoBehaviour
{
    public GameObject IdInput1;
    public GameObject IdInput2;
    void Update()
    {
        if (IdInput2.activeSelf == true)
        {
            IdInput1.SetActive(false);
        }
        else if (IdInput1.activeSelf == true)
        {
            IdInput2.SetActive(false);
        }

    }
}
