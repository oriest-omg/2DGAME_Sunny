using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StarGame()
    {
        SceneManager.LoadScene("Jeu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
