using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{

    public RaiseLoadingEvent loadingEvent;

    public BinaryCrossSceneReference mainMenuLoadCall;

    // Start is called before the first frame update
    public void Load(bool obj)
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Start()
    {
        mainMenuLoadCall.BinaryMessage += Load;
    }
}
