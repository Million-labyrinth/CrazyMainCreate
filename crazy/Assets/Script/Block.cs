using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Player playerA;
    public Player2 playerB;
    Animator anim;
    public ObjectManager objectManager;
    public bool blockBreak = false;

    public float nextPushTime; // 블럭 밀기 최대(설정) 쿨타임
    public float curPushTime; // 블럭 밀기 현재(충전) 쿨타임

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Hit() {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false);

        // Item 드랍률
        int ran = Random.Range(0, 100);
        if (ran < 20)
        { // Not Item 20%
            Debug.Log("Not Item");
        }
        else if (ran < 40)
        {   // bubbleItem 20%
            GameObject bubbleItem = objectManager.MakeItem("BubbleItem");
            bubbleItem.transform.position = transform.position;
        }
        else if (ran < 60)
        {   // flulidItem 20%
            GameObject flulidItem = objectManager.MakeItem("FluidItem");
            flulidItem.transform.position = transform.position;
        }
        else if (ran < 70)
        {   // rollerItem 10%
            GameObject rollerItem = objectManager.MakeItem("RollerItem");
            rollerItem.transform.position = transform.position;
        }
        else if (ran < 80)
        {   // shieldItem 10%
            GameObject shieldItem = objectManager.MakeItem("ShieldItem");
            shieldItem.transform.position = transform.position;
        }
        else if (ran < 90)
        {   // niddleItem 100%
            GameObject niddleItem = objectManager.MakeItem("NiddleItem");
            niddleItem.transform.position = transform.position;
        }
        else if (ran < 100)
        {   // ultraFluidItem 10%
            GameObject ultraFluidItem = objectManager.MakeItem("UltraFluidItem");
            ultraFluidItem.transform.position = transform.position;
        }
    }


    void OnTriggerEnter2D(Collider2D  obj) {
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.2f);
        }
    }

    void OnCollisionStay2D(Collision2D obj) {
        if(obj.gameObject.tag == "Player") {
            curPushTime += Time.deltaTime;

            if(curPushTime > nextPushTime) {

                curPushTime = 0;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        curPushTime = 0;
    }

}
