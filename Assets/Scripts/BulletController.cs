using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.cameraTransform = GameObject.FindGameObjectWithTag(Tags.MainCamera).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rigidbody.MovePosition(this.transform.position + this.cameraTransform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case Tags.Enemy:
                //Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            case Tags.Scenery:
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
