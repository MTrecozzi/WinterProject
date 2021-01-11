using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneManagement/Load Event Channel")]
public class LoadEventChannelSO : ScriptableObject
{
    public event Action<GameSceneSO[]> OnLoadingRequested;

	public void RaiseEvent(GameSceneSO[] locationsToLoad)
	{
		if (OnLoadingRequested != null)
		{
			OnLoadingRequested.Invoke(locationsToLoad);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
