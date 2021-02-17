using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnTriggerEnter : MonoBehaviour
{

    [SerializeField]
    public GameSceneSO[] scenes;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Load();
        }
    }

    public void Load()
    {

        scenesToLoad.Clear();

        for (int i = 0; i < scenes.Length; i++)
        {
            if (i == 0)
            {
                scenesToLoad.Add(SceneManager.LoadSceneAsync(scenes[i].sceneName));
            }
            else scenesToLoad.Add(SceneManager.LoadSceneAsync(scenes[i].sceneName, LoadSceneMode.Additive));

            
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
