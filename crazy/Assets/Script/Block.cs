using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject player;
    Player playerLogic;
    Animator anim;


    public float nextPushTime; // 블럭 밀기 최대(설정) 쿨타임
    public float curPushTime; // 블럭 밀기 현재(충전) 쿨타임

    void Awake()
    {
        playerLogic = player.GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void Hit()
    {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false);

    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater")
        {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.2f);
        }
    }

    void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            curPushTime += Time.deltaTime;

            if (curPushTime > nextPushTime)
            {

                curPushTime = 0;
            }
        }
    }
        void OnCollisionExit2D(Collision2D other)
    {
        curPushTime = 0;
    }

}
