using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0F;
    public GameObject target;
    public GameObject bullet;

    private float moveDistance;
    private int enemyLevel;
    private Renderer renderer;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeed / 2F, moveSpeed * 2F);

        this.enemyLevel = 1;
        this.renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (this.renderer.material.color.g <= 0)
            this.Upgrade();

        Debug.Log(enemyLevel);

        switch (enemyLevel)
        {
            case 1:
                this.MoveTowardsTarget();
                break;
            case 2:
                this.MoveTowardsTarget();
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
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(this.transform.position);
        }
    }

    private void MoveTowardsTarget()
    {
        // Move our position a step closer to the target.
        this.moveDistance = moveSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, moveDistance);
    }

    public void TakeHit()
    {
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g - 0.1F, renderer.material.color.b);
    }

    public void Upgrade()
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
}
