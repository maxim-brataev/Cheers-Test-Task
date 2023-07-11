using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 2f;
    public GameObject[] enemyPrefabs;

    private Transform player;
    private Vector3 randPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Spawner());
    }
    private void Update()
    {
        int y = Random.Range(0, 2);
        if (y == 0)
        {
            y = -1;
        }
        randPos.x = player.position.x + Random.Range(-15, 16);
        randPos.y = player.position.y + y * Random.Range(8, 13);
        randPos.z = 0;
    }
    private IEnumerator Spawner ()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while(true)
        {
            yield return wait;
            Spawn();
        }
    }
    void Spawn()
    {
        if (player != null)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], randPos, player.rotation);
        }
    }
}
