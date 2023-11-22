using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapItemMove : MonoBehaviour
{
    public float lerpTime = 1.0f;
    Vector3 ObjPos;
    Vector3 firstPos;
    Vector3 lastPos;
    public float elapsedTime;

    private void Awake()
    {
        ObjPos = transform.position;
        firstPos = transform.position + new Vector3(0, 0.15f, 0);

    }

    void Start()
    {
        StartCoroutine(lerpUpCoroutine(ObjPos, firstPos, lerpTime));
    }

    IEnumerator lerpUpCoroutine(Vector3 current, Vector3 nextPos, float time)
    {
        while(gameObject.activeInHierarchy)
        {
            elapsedTime = 0.0f;

            this.transform.position = current;
            while (elapsedTime < time / 2)
            {
                elapsedTime += (Time.deltaTime);
                this.transform.position
                    = Vector3.Lerp(current, nextPos, elapsedTime / time);
                yield return null;
            }

            yield return new WaitForSeconds(0.15f);

            while(elapsedTime < time)
            {
                elapsedTime += (Time.deltaTime);
                this.transform.position
                    = Vector3.Lerp(nextPos, current, elapsedTime / time);
                yield return null;
            }

            yield return null;

        }
    }
}
