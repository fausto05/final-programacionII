using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Victoria()
    {
        SceneManager.LoadScene(0);
    }

    public void Derrota()
    {
        SceneManager.LoadScene(0);
    }
}
