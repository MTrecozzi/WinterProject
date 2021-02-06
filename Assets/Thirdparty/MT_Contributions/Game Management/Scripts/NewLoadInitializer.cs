using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLoadInitializer : MonoBehaviour
{
    public GameSceneSO CharacterSetUpScene;
    public GameSceneSO UIScene;
    public GameSceneSO GameManagementScene;

    void Awake()
    {
        SceneManager.LoadScene(CharacterSetUpScene.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(UIScene.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(GameManagementScene.sceneName, LoadSceneMode.Additive);
    }
}
