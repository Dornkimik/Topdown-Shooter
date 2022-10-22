using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // General
    private GameObject player;

    // UI
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;

    // Time

    public static bool isGamePaused = false;



    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }



    private void Update()
    {


        // Other
        ActivateGameOverScreen();
        GamePauseLogic();
    }



    private void GamePauseLogic()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    private void ActivateGameOverScreen()
    {
        if (player == null)
        {
            deathScreen.SetActive(true);
        } else
        {
            deathScreen.SetActive(false);
        }
    }




}
