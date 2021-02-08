using UnityEngine;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations or Menus)
/// </summary>
[CreateAssetMenu(menuName = "SceneManagement/SceneSO")]
public class GameSceneSO : ScriptableObject
{
	[Header("Information")]
	public string sceneName;
	public string path;

}
