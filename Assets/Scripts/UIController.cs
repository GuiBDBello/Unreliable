using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static int score;
    public static Text textScore;

    public GameObject mainMenu;
    public GameObject playerHUD;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private CameraController cameraController;
    private GameObject enemyGenerators;
    private GameObject music;
    private Notification notification;
    private PlayerController playerController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;

        mainMenu.SetActive(true);
        playerHUD.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        this.cameraController = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraController>();
        this.cameraController.enabled = false;

        this.enemyGenerators = GameObject.FindGameObjectWithTag(Tags.EnemyGenerator);
        this.enemyGenerators.gameObject.SetActive(false);

        this.music = GameObject.FindGameObjectWithTag(Tags.Music);
        this.music.GetComponent<AudioSource>().clip = menuMusic;
        this.music.GetComponent<AudioSource>().Play();

        this.notification = GameObject.FindGameObjectWithTag(Tags.Notification).GetComponent<Notification>();

        this.playerController = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

    private void Update()
    {
        this.notification.StartShowNotificationCoroutine(0.5F);
        this.ShowGameOverMenu();

        // Show cursor
        if (Input.GetKeyDown(KeyCode.Escape) && playerHUD.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            pauseMenu.SetActive(true);

            this.cameraController.enabled = false;
        }
    }

    public void OnButtonPlayPressed()
    {
        Cursor.lockState = CursorLockMode.Locked;

        mainMenu.SetActive(false);
        playerHUD.SetActive(true);
        pauseMenu.SetActive(false);

        textScore = GameObject.FindGameObjectWithTag(Tags.Score).GetComponent<Text>();

        this.cameraController.enabled = true;
        this.enemyGenerators.gameObject.SetActive(true);
        this.music.GetComponent<AudioSource>().clip = gameMusic;
        this.music.GetComponent<AudioSource>().Play();
    }

    public void OnButtonExitPressed()
    {
        Application.Quit();
    }

    public void OnButtonRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonResumePressed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        pauseMenu.SetActive(false);

        this.cameraController.enabled = true;
    }

    private void ShowGameOverMenu()
    {
        if (playerController.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            playerHUD.SetActive(false);
            gameOverMenu.SetActive(true);

            textScore = GameObject.FindGameObjectWithTag(Tags.Score).GetComponent<Text>();
            UIController.UpdateScore();

            this.cameraController.enabled = false;
            this.enemyGenerators.gameObject.SetActive(false);
        }
    }

    public static void UpdateScore()
    {
        UIController.textScore.text = "Score: " + UIController.score.ToString();
    }
}
