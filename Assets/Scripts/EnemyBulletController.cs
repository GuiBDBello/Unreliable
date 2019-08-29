using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform playerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.playerTransform = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        this.transform.LookAt(playerTransform.position);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.rigidbody.MovePosition(this.transform.position + this.transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tags.Player:
                other.GetComponent<PlayerController>().TakeHit(this.transform.position);
                Destroy(this.gameObject);
                break;
            case Tags.Enemy:
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            case Tags.Scenario:
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
