using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float initialY; 
    public float minY; 
    public float maxY; 

    private int direction = 1; 

    void Update()
    {

        Invoke("MoveItem", 0.3f);
    }

    void MoveItem()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.y += moveSpeed * direction * Time.deltaTime;

        if (currentPosition.y >= maxY)
        {
            currentPosition.y = maxY;
            direction = -1;
        }
        else if (currentPosition.y <= minY)
        {
            currentPosition.y = minY;
            direction = 1;
        }

        transform.position = currentPosition;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "upWater" || other.gameObject.tag == "downWater" || other.gameObject.tag == "leftWater" || other.gameObject.tag == "rightWater" || other.gameObject.tag == "hitCollider" || other.gameObject.tag == "Block")
        {

            gameObject.SetActive(false);

            Debug.Log(other.name);

        }
    }
}
