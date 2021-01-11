using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.SceneManagement;


public class SceneLoader : EditorWindow
{
    [MenuItem("SceneLoader/ Load Character Setup")]
    static void LoadCharacterSetup()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/CharacterScene/CharacterSetup.unity", OpenSceneMode.Additive);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
