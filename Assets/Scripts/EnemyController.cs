using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;

    private GameObject player;
    private PlayerController playerController;

    // Adjust the speed for the application.
    public float moveSpeed = 0F;

    private float moveDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Move our position a step closer to the target.
        this.moveDistance = this.moveSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.target.transform.position, moveDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Player)
        {
            playerController.TakeDamage(this.transform.position);
        }
    }
}
