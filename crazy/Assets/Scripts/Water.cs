using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Balloon ballon;

    public GameObject[] upWater;
    public GameObject[] downWater;
    public GameObject[] leftWater;
    public GameObject[] rightWater;

    void BackCondition()
    {
        switch (gameObject.tag)
        {
            case "upWater":
                for (int i = Array.IndexOf(upWater, gameObject); i < upWater.Length; i++)
                {
                    upWater[i].SetActive(true);
                }
                break;
            case "downWater":
                for (int i = Array.IndexOf(downWater, gameObject); i < downWater.Length; i++)
                {
                    downWater[i].SetActive(true);
                }
                break;
            case "leftWater":
                for (int i = Array.IndexOf(leftWater, gameObject); i < leftWater.Length; i++)
                {
                    leftWater[i].SetActive(true);
                }
                break;
            case "rightWater":
                for (int i = Array.IndexOf(rightWater, gameObject); i < rightWater.Length; i++)
                {
                    rightWater[i].SetActive(true);
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)  {
        if (obj.gameObject.tag == "Block" || obj.gameObject.tag == "Object")
        {
            switch (gameObject.tag)
            {
                case "upWater":
                    for (int i = Array.IndexOf(upWater, gameObject); i < upWater.Length; i++)
                    {
                        upWater[i].SetActive(false);
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "downWater":
                    for (int i = Array.IndexOf(downWater, gameObject); i < downWater.Length; i++)
                    {
                        downWater[i].SetActive(false);
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "leftWater":
                    for (int i = Array.IndexOf(leftWater, gameObject); i < leftWater.Length; i++)
                    {
                        leftWater[i].SetActive(false);
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "rightWater":
                    for (int i = Array.IndexOf(rightWater, gameObject); i < rightWater.Length; i++)
                    {
                        rightWater[i].SetActive(false);
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
            }

        }
    }
}
