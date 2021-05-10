using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GetStart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
