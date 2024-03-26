using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager1 : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Tutorials;


    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");

    }

    public void TurnOnTutorials()
    {
        Menu.SetActive(false);
        Tutorials.SetActive(true);
    }
    public void menu()
    {
        Menu.SetActive(true);
        Tutorials.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();

    }

    public void TutorialGoal()
    {
        SceneManager.LoadScene("Tutorial_Goal");

    }
    public void TutorialFighting()
    {
        SceneManager.LoadScene("Tutorial_Fighting");

    }
    public void TutorialHealthBars()
    {
        SceneManager.LoadScene("Tutorial_HealtOrbs");

    }



}
