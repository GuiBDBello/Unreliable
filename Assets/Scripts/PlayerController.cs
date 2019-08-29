using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public GameObject playerBullet;
    public Text textHealth;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    private int health;

    // Start is called before the first frame update
    private void Start()
    {
        this.characterController = GetComponent<CharacterController>();
        this.health = 3;
        this.textHealth.text = this.health.ToString();
    }

    private void Update()
    {
        // Shoot with the left mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.Shoot();
        }
    }

    private void FixedUpdate()
    {
        this.MovePlayer();

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
        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + this.characterController.radius);
        Instantiate(playerBullet, position, Quaternion.identity);
    }

    public void TakeHit(Vector3 direction)
    {
        this.characterController.Move(direction.normalized * Time.deltaTime * 200.0F);

        this.health--;
        this.textHealth.text = health.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
