using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLandOrderLayer : MonoBehaviour
{
    public SpriteRenderer meRenderer;
    public float layerMultiplier = 10.0f;


    void Awake()
    {
        meRenderer = gameObject.GetComponent<SpriteRenderer>();
        meRenderer.sortingOrder = 20;
    }

    void Update()
    {
        StartCoroutine("BalloonOrderLayer");
    }

    IEnumerator BalloonOrderLayer()
    {
        yield return new WaitForSeconds(0.4f);

        float x = 7 - Mathf.Round(transform.position.y);
        meRenderer.sortingOrder = (int)x;
    }
}
