using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0F;
    public GameObject enemyBullet;

    private float moveDistance;
    private int enemyLevel;
    private GameObject target;
    private Renderer renderer;
    private bool isShooting;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeed / 2F, moveSpeed * 2F);

        this.enemyLevel = 1;
        this.target = GameObject.FindGameObjectWithTag(Tags.Player);
        this.renderer = this.GetComponent<Renderer>();
        this.isShooting = false;
    }

    private void Update()
    {
        if (this.renderer.material.color.g <= 0)
            this.Upgrade();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (enemyLevel)
        {
            case 1:
                this.MoveTowardsTarget();
                break;
            case 2:
                this.MoveTowardsTarget();
                this.StartShootCoroutine();
                break;
            default:
                this.MoveTowardsTarget();
                this.StopCoroutine("ShootCoroutine");
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Player)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeHit(this.transform.position);
        }
    }

    private void MoveTowardsTarget()
    {
        // Move our position a step closer to the target.
        this.moveDistance = moveSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, moveDistance);
        this.transform.LookAt(target.transform.position);
    }

    private void Upgrade()
    {
        enemyLevel++;

        switch (enemyLevel)
        {
            case 2:
                moveSpeed /= 2;
                break;
            default:
                break;
        }

        this.renderer.material.color = new Color(renderer.material.color.r, 1F, renderer.material.color.b);
        this.transform.localScale += new Vector3(1F, 1F, 1F);
    }

    public void TakeHit()
    {
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g - 0.1F, renderer.material.color.b);
    }

    private IEnumerator ShootCoroutine(float waitTime)
    {
        this.isShooting = true;
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyBullet, (this.transform.position + this.transform.forward), Quaternion.identity);
        this.isShooting = false;
    }

    private void StartShootCoroutine()
    {
        if (!this.isShooting)
            this.StartCoroutine(this.ShootCoroutine(2F));
    }
}
