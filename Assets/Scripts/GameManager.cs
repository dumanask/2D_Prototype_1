using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        pauseMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        pauseMenu.gameObject.SetActive(false);
    }

    public void MenuState()
    {
        currentState = GameState.Menu;
    }

    public void AbilitySelecting()
    {
        currentState = GameState.AbilityChoosing;
    }
}
