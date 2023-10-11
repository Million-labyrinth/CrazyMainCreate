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

    RaycastHit2D rayHit;


    void Update()
    {
        // Ray
        switch (gameObject.tag)
        {
            case "upWater":
                for (int i = Array.IndexOf(upWater, gameObject); i < upWater.Length; i++)
                {
                    Debug.DrawRay(transform.position, Vector3.up * 0.7f, new Color(0, 1, 0));
                    rayHit = Physics2D.Raycast(transform.position, Vector3.up, 0.7f, LayerMask.GetMask("Block"));
                }

                Invoke("BackCondition", 0.5f);
                break;
            case "downWater":
                for (int i = Array.IndexOf(downWater, gameObject); i < downWater.Length; i++)
                {
                    Debug.DrawRay(transform.position, Vector3.down * 0.7f, new Color(0, 1, 0));
                    rayHit = Physics2D.Raycast(transform.position, Vector3.down, 0.7f, LayerMask.GetMask("Block"));

                }

                Invoke("BackCondition", 0.5f);
                break;
            case "leftWater":
                for (int i = Array.IndexOf(leftWater, gameObject); i < leftWater.Length; i++)
                {
                    Debug.DrawRay(transform.position, Vector3.left * 0.7f, new Color(0, 1, 0));
                    rayHit = Physics2D.Raycast(transform.position, Vector3.left, 0.7f, LayerMask.GetMask("Block"));
                }

                Invoke("BackCondition", 0.5f);
                break;
            case "rightWater":
                for (int i = Array.IndexOf(rightWater, gameObject); i < rightWater.Length; i++)
                {
                    Debug.DrawRay(transform.position, Vector3.right * 0.7f, new Color(0, 1, 0));
                    rayHit = Physics2D.Raycast(transform.position, Vector3.right, 0.7f, LayerMask.GetMask("Block"));
                }

                Invoke("BackCondition", 0.5f);
                break;
        }

        // Á¶°Ç¹® 
        if (rayHit.collider != null)
        {
            switch (gameObject.tag)
            {
                case "upWater":
                    for (int i = Array.IndexOf(upWater, gameObject) + 1; i < upWater.Length; i++)
                    {
                        upWater[i].SetActive(false);
                        Debug.Log(i + " up");
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "downWater":
                    for (int i = Array.IndexOf(downWater, gameObject) + 1; i < downWater.Length; i++)
                    {
                        downWater[i].SetActive(false);
                        Debug.Log(i + " down");

                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "leftWater":
                    for (int i = Array.IndexOf(leftWater, gameObject) + 1; i < leftWater.Length; i++)
                    {
                        leftWater[i].SetActive(false);
                        Debug.Log(i + " left");
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
                case "rightWater":
                    for (int i = Array.IndexOf(rightWater, gameObject) + 1; i < rightWater.Length; i++)
                    {
                        rightWater[i].SetActive(false);
                        Debug.Log(i + " right");
                    }

                    Invoke("BackCondition", 0.5f);
                    break;
            }
        }
    }

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
        if (obj.gameObject.tag == "Block")
        {
            gameObject.SetActive(false);

        }
    }
}
