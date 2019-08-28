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

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeed / 2F, moveSpeed * 2F);

        this.enemyLevel = 1;
        this.target = GameObject.FindGameObjectWithTag(Tags.Player);
        this.renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //this.transform.forward = GameObject.FindGameObjectWithTag(Tags.Player).transform.position;//this.target.transform.position;
        Debug.DrawLine(this.transform.position, this.transform.forward);

        if (this.renderer.material.color.g <= 0)
            this.Upgrade();

        switch (enemyLevel)
        {
            case 1:
                this.MoveTowardsTarget();
                break;
            case 2:
                this.MoveTowardsTarget();
                this.Shoot();
                break;
            default:
                this.MoveTowardsTarget();
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
        //this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, moveDistance);
        this.transform.LookAt(target.transform.position);
    }

    private void Upgrade()
    {
        switch(enemyLevel)
        {
            default:
                moveSpeed /= 2;
                break;
        }
        this.renderer.material.color = new Color(renderer.material.color.r, 1F, renderer.material.color.b);
        this.transform.localScale += new Vector3(1F, 1F, 1F);

        enemyLevel++;
    }

    public void TakeHit()
    {
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g - 0.1F, renderer.material.color.b);
    }

    public void Shoot()
    {
        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (Vector3.forward.magnitude * this.transform.localScale.z));
        Instantiate(enemyBullet, (this.transform.position + this.transform.forward), Quaternion.identity);
    }
}
