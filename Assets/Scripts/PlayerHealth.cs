using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.playerController.health
        if (playerController.health < 3)
        {
            heart3.gameObject.SetActive(false);
        }
        if (playerController.health < 2)
        {
            heart2.gameObject.SetActive(false);
        }
        if (playerController.health < 1)
        {
            heart2.gameObject.SetActive(false);
        }
    }
}
