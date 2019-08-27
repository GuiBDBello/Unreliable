using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    
    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        this.characterController = GetComponent<CharacterController>();
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
    }

    private void MovePlayer()
    {
        if (this.characterController.isGrounded)
        {
            this.moveDirection = (this.transform.forward * Input.GetAxis("Vertical")) + (this.transform.right * Input.GetAxis("Horizontal"));
            this.moveDirection *= this.speed;

            if (Input.GetButton("Jump"))
                this.moveDirection.y = this.jumpSpeed;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        this.moveDirection.y -= this.gravity * Time.deltaTime;

        // Move the controller
        this.characterController.Move(this.moveDirection * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(this.bullet, this.transform.position, Quaternion.Euler(90F, 0F, 0F));
    }
}
