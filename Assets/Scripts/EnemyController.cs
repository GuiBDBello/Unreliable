using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public int enemyLevel;

    public float moveSpeed;
    public float kamikazeSpeed;
    public GameObject enemyBullet;

    private float moveDistance;
    private bool isShooting;
    private bool isKamikazeing;
    private GameObject target;
    private Renderer renderer;
    private Rigidbody rigidbody;

    private void Start()
    {
        this.enemyLevel = 1;
        this.moveSpeed = 5F;
        this.moveSpeed = Random.Range(moveSpeed / 2F, moveSpeed * 2F);
        this.kamikazeSpeed = 5000F;
        this.isShooting = false;
        this.isKamikazeing = false;

        this.target = GameObject.FindGameObjectWithTag(Tags.Player);
        this.renderer = this.GetComponent<Renderer>();
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawLine(this.transform.position, target.transform.position);

        if (this.renderer.material.color.g <= 0F)
            this.Upgrade();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (this.enemyLevel)
        {
            case 1:
                this.MoveTowardsTarget();
                break;
            case 2:
                this.MoveTowardsTarget();
                this.StartShootCoroutine();
                break;
            case 3:
                this.MoveTowardsTarget();
                this.StartKamikazeCoroutine();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == Tags.Player)
        {
            other.GetComponent<PlayerController>().TakeHit(this.transform.position);
        } else if (other.tag == Tags.Enemy)
        {
            if (this.enemyLevel == 3)
            {
                if (other.GetComponent<EnemyController>().enemyLevel == 1)
                {
                    UIController.score += 10;
                } else if (other.GetComponent<EnemyController>().enemyLevel == 2)
                {
                    UIController.score += 50;
                } else {
                    UIController.score += 250;
                }
                UIController.UpdateScore();
                Destroy(other);
            }
        }
    }

    private void StartShootCoroutine()
    {
        if (!this.isShooting)
            this.StartCoroutine(this.ShootCoroutine(2F));
    }

    private IEnumerator ShootCoroutine(float waitTime)
    {
        this.isShooting = true;
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyBullet, (this.transform.position + this.transform.forward), Quaternion.identity);
        this.isShooting = false;
    }

    private void StartKamikazeCoroutine()
    {
        if (!this.isKamikazeing)
            this.StartCoroutine(this.KamikazeCoroutine(5F));
    }

    private IEnumerator KamikazeCoroutine(float waitTime)
    {
        this.isKamikazeing = true;
        yield return new WaitForSeconds(waitTime);
        this.rigidbody.AddForce((target.transform.position - this.transform.position).normalized * kamikazeSpeed);
        this.isKamikazeing = false;
    }

    private void MoveTowardsTarget()
    {
        // Move our position a step closer to the target.
        this.moveDistance = this.moveSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, this.moveDistance);
        this.transform.LookAt(target.transform.position);
    }

    private void Upgrade()
    {
        if (this.enemyLevel < 3)
            this.enemyLevel++;

        switch (this.enemyLevel)
        {
            case 2:
                this.moveSpeed /= 2;
                this.renderer.material.color = new Color(this.renderer.material.color.r, 1F, this.renderer.material.color.b);
                this.transform.localScale += new Vector3(1F, 1F, 1F);
                break;
            case 3:
                this.moveSpeed /= 2;
                this.renderer.material.color = Color.gray;
                this.transform.localScale += new Vector3(1F, 1F, 1F);
                this.StopAllCoroutines();
                break;
        }
    }

    public void TakeHit()
    {
        if (this.renderer.material.color != Color.gray)
        {
            this.renderer.material.color = new Color(this.renderer.material.color.r, this.renderer.material.color.g - 0.2F, this.renderer.material.color.b);
        }
    }
}
