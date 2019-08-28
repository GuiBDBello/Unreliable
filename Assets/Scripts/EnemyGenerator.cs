using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    private bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        this.isWaiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.StartSpawnEnemyCoroutine();
    }

    private IEnumerator SpawnEnemyCoroutine(float waitTime)
    {
        this.isWaiting = false;
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemy, this.transform.position, Quaternion.identity);
        this.isWaiting = true;
    }

    private void StartSpawnEnemyCoroutine()
    {
        if (this.isWaiting)
            this.StartCoroutine(this.SpawnEnemyCoroutine(5F));
    }
}
