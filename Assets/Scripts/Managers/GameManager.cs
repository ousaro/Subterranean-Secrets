using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] AudioSource music;


    private void Start()
    {
        instance = this;    
        music.Play();
    }

    public void StartGame(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
        Time.timeScale = 1f;

    }

    public void QuitGame()
    {
        Application.Quit();
        print("You have quit the game.");
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
