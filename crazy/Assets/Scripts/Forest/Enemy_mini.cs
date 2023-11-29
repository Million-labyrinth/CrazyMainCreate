using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mini : MonoBehaviour
{
    public GameObject enemy;
    public PVEManager pveManager;

    private void Start()
    {
        pveManager = FindObjectOfType<PVEManager>();
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        pveManager.enemyCount--;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater" || collision.gameObject.tag == "hitCollider")
        {
            // ��� �ִϸ��̼� �߰� �ʿ�
            StartCoroutine("Die");

        }
    }
}
