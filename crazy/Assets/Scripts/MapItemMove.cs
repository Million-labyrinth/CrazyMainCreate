using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapItemMove : MonoBehaviour
{
    public float lerpTime = 1.0f;
    public Vector3 ObjPos;
    Vector3 firstPos;
    public float elapsedTime;


    void OnEnable()
    {
        // 처음부터 맵에 설치되어 있는 아이템때문에 필요 (프리팹은 비활성화 해 놓아야 하기에 맵에 있는 아이템들은 따로 활성화 해줘야 함)
        ObjPos = transform.position;    

        StartCoroutine(StartMove());
   
    }

    // 블럭 파괴 시, 아이템의 ObjPos 를 설정하는데 딜레이가 필요해서 다시 함수로 묶음.
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(0.1f);
        firstPos = ObjPos + new Vector3(0, 0.15f, 0);
        StartCoroutine(lerpUpCoroutine(ObjPos, firstPos, lerpTime));
    }

    IEnumerator lerpUpCoroutine(Vector3 current, Vector3 nextPos, float time)
    {
        yield return null;
        while (gameObject.activeInHierarchy)
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "upWater" || other.gameObject.tag == "downWater" || other.gameObject.tag == "leftWater" || other.gameObject.tag == "rightWater" || other.gameObject.tag == "hitCollider" || other.gameObject.tag == "Block")
        {

            gameObject.SetActive(false);

            Debug.Log(other.name);

        }
    }
}
