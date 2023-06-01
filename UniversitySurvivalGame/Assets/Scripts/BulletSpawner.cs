using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    //���� ���� �߰�
    public Vector3 spawnPosition;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    void Start()
    {
        this.timeAfterSpawn = 0f;
        this.spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        this.target = FindObjectOfType<Player>().transform;
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        timeAfterSpawn += Time.deltaTime;
        spawnPosition = new Vector3(
            Random.Range(transform.position.x - 10f, transform.position.x + 10f),
            transform.position.y,
            transform.position.z );

        if (this.timeAfterSpawn >= this.spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject bullet
                = Instantiate(this.bulletPrefab, spawnPosition, this.transform.rotation);

            bullet.transform.LookAt(target);
            Destroy(bullet,4);
            this.spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}