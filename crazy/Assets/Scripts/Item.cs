using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//github test222222222 kim in seob


public class Item : MonoBehaviour
{
    public Player player;


    private int previousBombPower;
    private int previousBombRange;
    private float previousPlayerSpeed;
    private bool isSupermanActive = false;

    public GameObject[] speedItem;
    public GameObject[] bombItem;
    public GameObject[] rangeItem;
    public GameObject[] Activeitem;

    CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Debug.Log(Activeitem[0].name);
        Activeitem[0] = null;
    }


    public void PowerAdd()
    {
        if (player.bombPower < player.bombPowerMax)
        {
            player.bombPower++;
        }

    }

    public void SpeedAdd()
    {
        if (player.playerSpeed < player.playerSpeedMax)
        {
            player.playerSpeed++;
        }
    }

    public void RangeAdd(string name)
    {
        if (player.bombRange < player.bombRangeMax)
        {
            // 일반 물줄기 아이템
            if (name.Contains("basicFluid"))
            {
                player.bombRange++;
            }
            // 울트라 물줄기 아이템
            else if (name.Contains("ultraFluid"))
            {
                player.bombRange = player.bombRangeMax;
            }
        }
    }

    public void RedDeVil()
    {
        player.playerSpeed = player.playerSpeedMax;
    }


    public void LoadState()
    {
        player.bombPower = previousBombPower;
        player.bombRange = previousBombRange;
        player.playerSpeed = previousPlayerSpeed;
    }



    public void SuperMan(string name)
    {
        if (name.Contains("superMan") && !isSupermanActive)
        {

            isSupermanActive = true;


            previousBombPower = player.bombPower;
            previousBombRange = player.bombRange;
            previousPlayerSpeed = player.playerSpeed;




            player.bombPower = player.bombPowerMax;
            player.bombRange = player.bombRangeMax;
            player.playerSpeed = player.playerSpeedMax;


            StartCoroutine(RestoreAbilitiesAfterDelay(5f));
        }
    }



    public void AddActiveItem(GameObject item, int index)
    {
        if (index >= 0 && index < Activeitem.Length)
        {
            Activeitem[index] = item;
        }
    }


    public void ActiveUseItem(string name)
    {
        if (Activeitem[0].name.Contains("niddle"))
        {
            Debug.Log("niddle item touch");
            player.playerHealth = 0f;
        }
        else if (Activeitem[0].name.Contains("shield"))
        {
            Debug.Log("shield item touch");


        }




        for (int i = 0; i < Activeitem.Length; i++)
        {
            if (Activeitem[i] != null && Activeitem[i].name == name)
            {
                Activeitem[i] = null;
                break;
            }
        }
    }


    private IEnumerator RestoreAbilitiesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);


        player.bombPower = previousBombPower;
        player.bombRange = previousBombRange;
        player.playerSpeed = previousPlayerSpeed;


        isSupermanActive = false;
    }
}
