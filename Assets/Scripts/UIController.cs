using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playerHUD;

    CameraController cameraController;
    GameObject[] enemyGenerators;

    private void Awake()
    {
        mainMenu.SetActive(true);
        playerHUD.SetActive(false);

        this.cameraController = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraController>();
        this.cameraController.enabled = false;

        this.enemyGenerators = GameObject.FindGameObjectsWithTag(Tags.EnemyGenerator);
    }

    public void OnButtonPlayPressed()
    {
        mainMenu.SetActive(false);
        playerHUD.SetActive(true);

        this.cameraController.enabled = true;
        foreach (GameObject enemyGenerator in this.enemyGenerators)
        {
            enemyGenerator.gameObject.SetActive(true);
        }
    }

    public void OnButtonExitPressed()
    {
        Application.Quit();
    }

}
