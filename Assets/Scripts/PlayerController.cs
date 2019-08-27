using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    public Text textHealth;

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    private int health = 3;

    // Start is called before the first frame update
    private void Start()
    {
        this.characterController = GetComponent<CharacterController>();
        this.textHealth.text = health.ToString();
    }

    private void FixedUpdate()
    {
        // Move the player
        this.MovePlayer();

        // Shoot with the left mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.Shoot();
        }

        if (this.health <= 0)
        {
            this.GameOver();
        }
    }

    private void MovePlayer()
    {
        if (this.characterController.isGrounded)
        {
            this.moveDirection = (this.transform.forward * Input.GetAxis("Vertical"))
                + (this.transform.right * Input.GetAxis("Horizontal"));
            this.moveDirection *= this.speed;

            if (Input.GetButton("Jump"))
                this.moveDirection.y = this.jumpSpeed;
        }
        this.moveDirection.y -= this.gravity * Time.deltaTime;
        this.characterController.Move(this.moveDirection * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(this.bullet, this.transform.position, Quaternion.identity);
    }

    public void TakeDamage(Vector3 direction)
    {
        this.health--;
        this.characterController.Move(direction.normalized * Time.deltaTime * 200.0F);
        this.textHealth.text = health.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
