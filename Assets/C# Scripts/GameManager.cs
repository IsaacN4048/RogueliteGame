using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //SINGLETON

    [Header("Settings Toggles")]
    public bool damageNumbers;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
            
    }

    public void ToggleDamageNumbers() //confusing imagery when called by button, fix this
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
   
}
