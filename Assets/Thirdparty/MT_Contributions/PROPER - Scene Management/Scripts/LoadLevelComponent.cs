using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelComponent : MonoBehaviour
{

    [SerializeField]


    public GameSceneSO activeLevel;
    public GameSceneSO[] managementScenes;

    


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    public void Load()
    {

        scenesToLoad.Clear();

        scenesToLoad.Add(SceneManager.LoadSceneAsync(activeLevel.sceneName));

        for (int i = 0; i < managementScenes.Length; i++)
        {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(managementScenes[i].sceneName, LoadSceneMode.Additive));       
        }

        StartCoroutine(Loading());

    }

    IEnumerator Loading()
    {
        float totalProgress = 0;

        for (int i = 0; i < scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;

                yield return null;
            }
        }
    }
}
