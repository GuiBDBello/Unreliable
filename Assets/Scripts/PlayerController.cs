using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool isDead;

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public GameObject playerBullet;
    public Text textHealth;
    public AudioClip shootAudio;
    public AudioClip takeHitAudio;
    public AudioClip dieAudio;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private AudioSource audio;

    // Start is called before the first frame update
    private void Start()
    {
        this.characterController = GetComponent<CharacterController>();
        this.health = 3;
        textHealth.text = "Health: " + this.health.ToString();
        this.isDead = false;
        this.audio = GetComponent<AudioSource>();
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
            UIController.UpdateScore();
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
        Destroy(Instantiate(playerBullet, position, Quaternion.identity), 5F);

        this.audio.PlayOneShot(shootAudio);
    }

    public void TakeHit(Vector3 direction)
    {
        this.characterController.Move(direction.normalized * Time.deltaTime * 200.0F);

        this.health--;
        this.textHealth.text = "Health: " + this.health.ToString();

        this.audio.PlayOneShot(shootAudio);
    }

    private void GameOver()
    {
        this.isDead = true;

        this.audio.PlayOneShot(dieAudio);
    }
}
