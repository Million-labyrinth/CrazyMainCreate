﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoomMapSelect : MonoBehaviour

//미완성
{
    //맵 이름을 담기 위한 변수
    public GameObject mapNameVillage;
    public GameObject mapNameFactory;
    public GameObject mapNameForest;
    public GameObject mapNamePVE;
    public GameObject mapNameRandom;
    public GameObject mapNamePangLand;

    public string[] RandomSceneNames;


    public void ttv()
    {
        int ran = Random.Range(0, 4);

        if (mapNameVillage.activeSelf == true || mapNameForest.activeSelf == true || mapNameFactory.activeSelf == true || mapNamePangLand.activeSelf == true)
        {
            if (mapNameVillage.activeSelf == true)
            {
                SceneManager.LoadScene("BattelFieldVillage");
                Debug.Log("village map move");
            }
            else if (mapNameForest.activeSelf == true)
            {
                SceneManager.LoadScene("BattleFieldForest");
                Debug.Log("forest map move");

            }
            else if (mapNameFactory.activeSelf == true)
            {
                SceneManager.LoadScene("BattleFieldFactory");
                Debug.Log("factory map move");
            }
            else if (mapNamePVE.activeSelf == true)
            {
                SceneManager.LoadScene("ForestStage1"); //PVEMap
                Debug.Log("PVE map move");
            }
            else if (mapNamePangLand.activeSelf == true)
            {
                SceneManager.LoadScene("BattelFieldPangLand");
                Debug.Log("PangLand map move");
            }


        }
        else if (mapNameRandom.activeSelf == true)
        {
            switch (ran)
            {
                case 0:
                    SceneManager.LoadScene("BattelFieldVillage");
                    Debug.Log("random village map move");
                    break;
                case 1:
                    SceneManager.LoadScene("BattleFieldFactory");
                    Debug.Log("random factory map move");
                    break;
                 case 2:
                    SceneManager.LoadScene("BattleFieldForest");
                    Debug.Log("random Forest map move");
                    break;
                case 3:
                    SceneManager.LoadScene("BattelFieldPangLand");
                    Debug.Log("random Pangland map move");
                    break;
            }

        }
        else
        {
            return;
        }
        // // mapselect에서 MapNameText를 가져와서 Text 컴포넌트를 얻음
        // GameObject mapNameTextObject = GameObject.Find(mapselect + "/MapNameText");

        // if (mapNameTextObject != null)
        // {
        //     Text mapNameTextComponent = mapNameTextObject.GetComponent<Text>();

        //     if (mapNameTextComponent != null)
        //     {
        //         string currentMapName = mapNameTextComponent.text;

        //         string sceneName = ""; // 넘어갈 씬의 이름을 지정해야 합니다.

        //         switch (currentMapName)
        //         {
        //             case "Village":
        //                 sceneName = "BattelFieldVillage";
        //                 break;
        //             case "Factory":
        //                 sceneName = "BattleFieldFactory";
        //                 break;
        //             case "PangLand":
        //                 sceneName = "BattelFieldPangLand";
        //                 break;
        //                 // 추가적인 케이스들을 필요에 따라 지정하세요.
        //         }

        //         SceneManager.LoadScene(sceneName);
        //     }
        //     else
        //     {
        //         Debug.LogError(mapselect + "/MapNameText에 Text 컴포넌트가 없습니다.");
        //     }
        // }
        // else
        // {
        //     Debug.LogError(mapselect + "/MapNameText를 찾을 수 없습니다.");
        // }
    }

}
