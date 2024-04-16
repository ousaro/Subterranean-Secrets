using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIMenager : MonoBehaviour
{
    public static UIMenager instance;

    [SerializeField] Slider playerHealthSlider;
    [SerializeField] Slider enemyHealthSlider;

    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] GameObject enemySliderHolder;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    [SerializeField] AudioSource gameOverMusic;

    private bool pause = false;


    private void Awake()
    {
        instance = this;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
        playerHealthSlider.maxValue = PlayerController.Instance.MaxHp();
        playerHealthSlider.value = PlayerController.Instance.CurrentHp();
        coinText.text = PlayerController.Instance.CurrentCoin().ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHPUI()
    {
        playerHealthSlider.value = PlayerController.Instance.CurrentHp();
    }

    public void UpdateHpEnemy(int currentHp)
    {
        enemyHealthSlider.value = currentHp;
    }

    public void UpdateCoinText()
    {
        coinText.text = PlayerController.Instance.CurrentCoin().ToString();
    }

    public void PauseResume()
    {
        pause = !pause;
        pausePanel.SetActive(pause);
        if (pause) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }

    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverMusic.Play();
        GameManager.instance.StopMusic();
        Time.timeScale = 0f;
    }

    public void Win()
    {
        winPanel.SetActive(true);
        PlayerController.Instance.StropSFX();
        Time.timeScale = 0f;
    }
    public void ShowEnemyHpSlider(bool active, int currentHp)
    {

        enemySliderHolder.SetActive(active);
        enemyHealthSlider.maxValue = EnemyController.instance.MaxHp();
        enemyHealthSlider.value = currentHp;
    }

    public bool PauseVal() { return pause; }





    

}
