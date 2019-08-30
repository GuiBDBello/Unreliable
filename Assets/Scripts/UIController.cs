using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static int score;
    public GameObject mainMenu;
    public GameObject playerHUD;
    public GameObject gameOverMenu;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private CameraController cameraController;
    private GameObject enemyGenerators;
    private GameObject music;
    private Notification notification;
    private PlayerController playerController;

    private void Awake()
    {
        mainMenu.SetActive(true);
        playerHUD.SetActive(false);
        gameOverMenu.SetActive(false);

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
    }

    public void OnButtonPlayPressed()
    {
        mainMenu.SetActive(false);
        playerHUD.SetActive(true);

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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowGameOverMenu()
    {
        if (playerController.isDead)
        {
            gameOverMenu.SetActive(true);
        }
    }
}
