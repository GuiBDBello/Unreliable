using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform cameraTransform;

    // Start is called before the first frame update
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.cameraTransform = GameObject.FindGameObjectWithTag(Tags.MainCamera).transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.rigidbody.MovePosition(this.transform.position + this.cameraTransform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tags.Enemy:
                other.GetComponent<EnemyController>().TakeHit();
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
