using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayer : MonoBehaviour
{
    public SpriteRenderer meRenderer;
    public float layerMultiplier = 10.0f;

    void Start()
    {
        meRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float x = 7 - (float)Mathf.RoundToInt(transform.position.y);

        int newOrderInLayer = Mathf.RoundToInt(x * layerMultiplier);

        meRenderer.sortingOrder = newOrderInLayer;

        /*if (5.5f <= x && x < 6.5f)
        {
            meRenderer.sortingOrder = 1;
        }
        else if (4.5f <= x && x < 5.5f)
        {
            meRenderer.sortingOrder = 2;
        }
        else if (3.5f <= x && x < 4.5f)
        {
            meRenderer.sortingOrder = 3;
        }
        else if (2.5f <= x && x < 3.5f)
        {
            meRenderer.sortingOrder = 4;
        }
        else if (1.5f <= x && x < 2.5f)
        {
            meRenderer.sortingOrder = 5;
        }
        else if (0.5f <= x && x < 1.5f)
        {
            meRenderer.sortingOrder = 6;
        }
        else if (-0.5f <= x && x < 0.5f)
        {
            meRenderer.sortingOrder = 7;
        }
        else if (-1.5f <= x && x < -0.5f)
        {
            meRenderer.sortingOrder = 8;
        }
        else if (-2.5f <= x && x < -1.5f)
        {
            meRenderer.sortingOrder = 9;
        }
        else if (-3.5f <= x && x < -2.5f)
        {
            meRenderer.sortingOrder = 10;
        }
        else if (-4.5f <= x && x < -3.5f)
        {
            meRenderer.sortingOrder = 11;
        }
        else if (-5.5f <= x && x < -4.5f)
        {
            meRenderer.sortingOrder = 12;
        }
        else if (-6.5f <= x && x < -5.5f)
        {
            meRenderer.sortingOrder = 13;
        }*/
    }
}