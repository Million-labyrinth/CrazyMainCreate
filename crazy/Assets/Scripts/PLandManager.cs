using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLandManager : MonoBehaviour
{
    public GameObject OctopusBalloon;

    void Awake()
    {
        OctopusBalloon.SetActive(false);
    }

    void Start()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);
        OctopusBalloon.SetActive(true);

        StopCoroutine("StartGame");
    }
}
