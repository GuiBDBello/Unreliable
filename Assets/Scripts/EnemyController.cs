using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;

    // Adjust the speed for the application.
    public float moveSpeed = 0F;

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float moveDistance = moveSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.target.transform.position, moveDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Player)
        {
            Debug.Log("Collided with the Player!");
        }
    }
}
