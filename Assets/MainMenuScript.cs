using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{  
    public void LoadGame()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
