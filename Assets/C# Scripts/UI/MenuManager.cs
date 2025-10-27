using Player;
using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanvas;

    private bool isPaused;


    private void Start()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        if(InputManager.instance.MenuOpenCloseInput)
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        PlayerManager.instance.FreeCursor();

        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        PlayerManager.instance.SetFirstPersonCursor();

        CloseAllMenus();
    }

    private void CloseAllMenus()
    {
       _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
    }

    private void OpenMainMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsMenuCanvas.SetActive(false);
    }



}
