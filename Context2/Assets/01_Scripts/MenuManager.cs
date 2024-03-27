using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private enum Role { Politician, Scooter }
    [SerializeField] private Role role;

    [SerializeField] private GameObject UICanvas;
    [SerializeField] private NetworkingUIManager2 UIManager2;

    [SerializeField] private string scooterScene;
    [SerializeField] private string politicianScene;

    public void StartGame()
    {
        if(role == Role.Politician)
        {
            UIManager2.ClickedOnClientButton();
            Debug.Log("spawnPolitician");
        }

        if(role == Role.Scooter)
        {
            UICanvas.SetActive(false);
            UIManager2.ClickedOnHostButton();
            Debug.Log("spawnScooter");
        }
    }

    public void ExitGame()
    {
        Application.Quit();

    }

    public void LoadScooterScene()
    {
        SceneManager.LoadScene(scooterScene);
    }
    
    public void LoadPoliticianScene()
    {
        SceneManager.LoadScene(politicianScene);
    }
    public void TutorialHealthBars()
    {
        SceneManager.LoadScene("Tutorial_HealtOrbs");

    }
}
