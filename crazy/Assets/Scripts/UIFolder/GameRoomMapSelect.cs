using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoomMapSelect : MonoBehaviour

    //미완성
{
    public string[] RandomSceneNames;

    public void ttv(string mapselect)
    {
        // mapselect에서 MapNameText를 가져와서 Text 컴포넌트를 얻음
        GameObject mapNameTextObject = GameObject.Find(mapselect + "/MapNameText");

        if (mapNameTextObject != null)
        {
            Text mapNameTextComponent = mapNameTextObject.GetComponent<Text>();

            if (mapNameTextComponent != null)
            {
                string currentMapName = mapNameTextComponent.text;

                string sceneName = ""; // 넘어갈 씬의 이름을 지정해야 합니다.

                switch (currentMapName)
                {
                    case "Village":
                        sceneName = "BattelFieldVillage";
                        break;
                    case "Factory":
                        sceneName = "BattleFieldFactory";
                        break;
                    case "PangLand":
                        sceneName = "BattelFieldPangLand";
                        break;
                        // 추가적인 케이스들을 필요에 따라 지정하세요.
                }

                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError(mapselect + "/MapNameText에 Text 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError(mapselect + "/MapNameText를 찾을 수 없습니다.");
        }
    }

    public void LoadRandomScene()
    {
        if (RandomSceneNames.Length > 0)
        {
            int randomIndex = Random.Range(0, RandomSceneNames.Length);
            string randomSceneName = RandomSceneNames[randomIndex];
            SceneManager.LoadScene(randomSceneName);
        }
        else
        {
            Debug.LogError("씬 이름이 지정되지 않았습니다.");
        }
    }
}
