using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    public void StartGame()
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Lore()
    {
        clickSound.Play();
        SceneManager.LoadScene("Lore");
    }
    public void MainMenu()
    {
        clickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        clickSound.Play();
        Application.Quit();
    }
}
