using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    public Camera playerCam;
    public Camera mainMenuCam;
    public GameObject hudCanvas;

    public void StartGame()
    {
        DeactivateMainMenu();
    }

    public void QuitToMenu()
    {
        ActivateMainMenu();
    }



    public void ActivateMainMenu()
    {
        mainMenuCam.gameObject.SetActive(true);
    }

    public void DeactivateMainMenu()
    {
        mainMenuCam.gameObject.SetActive(false);
    }
}
