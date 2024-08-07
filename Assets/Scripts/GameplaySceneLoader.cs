using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneLoader : MonoBehaviour
{
    void Awake()
    {
		SceneManager.LoadScene("Gameplay");
	}
}
