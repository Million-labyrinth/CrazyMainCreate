using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    public Animator anim;
    public ObjectManager objectManager;
    public bool blockBreak = false;
   

    void Awake() {
        
        anim = GetComponent<Animator>();
        // 현재 활성화된 Scene에서 ObjectManager 클래스의 인스턴스를 찾아서 가져와서 초기화
        objectManager = FindObjectOfType<ObjectManager>();
    }

    void Hit() {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false);

        Invoke("SpawnItem", 0.3f);
    }

    void SpawnItem()
    {
        // Item 드랍률
        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < 58)
        { // Not Item 40%
            Debug.Log("Not Item");
        }
        else if (ran < 73)
        {   // bubbleItem 15%
            GameObject bubbleItem = objectManager.MakeItem("BubbleItem");
            bubbleItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z);
        }
        else if (ran < 83)
        {   // flulidItem 10%
            GameObject flulidItem = objectManager.MakeItem("FluidItem");
            flulidItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z);
        }
        else if (ran < 88)
        {   // rollerItem 5%
            GameObject rollerItem = objectManager.MakeItem("RollerItem");
            rollerItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z);
        }
        else if (ran < 97)
        {   // shieldItem 5%
            GameObject shieldItem = objectManager.MakeItem("ShieldItem");
            shieldItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z); 
        }
        /*else if (ran < 99)
        {   // niddleItem 5%
            GameObject niddleItem = objectManager.MakeItem("NiddleItem");
            niddleItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z);
        }*/
        else if (ran < 100)
        {   // ultraFluidItem 5%
            GameObject ultraFluidItem = objectManager.MakeItem("UltraFluidItem");
            ultraFluidItem.transform.position = new Vector3((float)Math.Round(this.transform.position.x), (float)Math.Round(this.transform.position.y), this.transform.position.z);
        }
    }

    // 이제 필요 없음
    void OnTriggerEnter2D(Collider2D  obj) {
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.5f);
        }
    }


}
