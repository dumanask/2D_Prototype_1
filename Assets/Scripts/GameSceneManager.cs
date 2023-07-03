using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string gamingScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gamingScene);
    }
}
