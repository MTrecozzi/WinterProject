using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : EditorWindow
{
    [MenuItem("SceneLoader/ Load Character Setup/ V 1")]
    static void LoadCharacterSetup()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/CharacterScene/CharacterSetup.unity", OpenSceneMode.Additive);
    }
    [MenuItem("SceneLoader/ Load Character Setup/ V 2")]
    static void LoadCharacterSetupV2()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/CharacterScene/CharacterSetup V 2.unity", OpenSceneMode.Additive);
    }

    [MenuItem("SceneLoader/ Load Grapple GUI")]
    static void LoadGrappleGUI()
    {
        EditorSceneManager.OpenScene("Assets/Thirdparty/MT_Contributions/Grapple Snapple/Grapple UI.unity", OpenSceneMode.Additive);
    }

    [MenuItem("SceneLoader/ LoadMTWorkStation")]
    static void LoadMTWorkStation()
    {
        LoadScenesFromSOArray("Assets/Scene Management/Scene Arrays/MT Work Station.asset");
    }

    static void LoadScenesFromSOArray(string assetPath)
    {
        var scenesToLoad = AssetDatabase.LoadAssetAtPath<GameSceneSOArray>(assetPath);

        for (int i = 0; i < scenesToLoad.scenes.Length; i++)
        {

        }
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
