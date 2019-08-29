using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playerHUD;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    CameraController cameraController;
    GameObject enemyGenerators;
    GameObject music;

    private void Awake()
    {
        mainMenu.SetActive(true);
        playerHUD.SetActive(false);

        this.cameraController = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraController>();
        this.cameraController.enabled = false;

        this.enemyGenerators = GameObject.FindGameObjectWithTag(Tags.EnemyGenerator);
        this.enemyGenerators.gameObject.SetActive(false);

        this.music = GameObject.FindGameObjectWithTag(Tags.Music);
        this.music.GetComponent<AudioSource>().clip = menuMusic;
        this.music.GetComponent<AudioSource>().Play();
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

}
