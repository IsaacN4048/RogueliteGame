using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //SINGLETON

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        ActivateMainMenu();
    }

    //MAIN MENU STUFF
    [Header("Main Menu Items")]
    public GameObject player;
    public Camera playerCam;
    public Camera mainMenuCam;
    public GameObject mainMenuCanvas;
    public GameObject hudCanvas;
    public GameObject inputManager;

    [Header("Settings Toggles")]
    public bool damageNumbers;


  

    public void ToggleDamageNumbers() //confusing imagery when called by button, fix this with a bool-checkbox
    {
        if(damageNumbers == true)
        {
            damageNumbers = false;
        }
        else if (damageNumbers == false)
        {
            damageNumbers = true;
        }
        else
        {
            return;
        }
    }



   

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
        mainMenuCanvas.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        hudCanvas.SetActive(false);
        inputManager.SetActive(false);
    }

    public void DeactivateMainMenu()
    {
        mainMenuCam.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        hudCanvas.SetActive(true);
        inputManager.SetActive(true);
    }

}
