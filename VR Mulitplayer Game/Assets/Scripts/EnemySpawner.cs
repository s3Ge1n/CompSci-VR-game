using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyAI enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemiesNumber;
    [SerializeField] private Player player;

    private List<EnemyAI> spawnedEnemies = new List<EnemyAI> ();
    private float timeSinceLastSpawn;

    private void Start()
    {
        timeSinceLastSpawn = spawnInterval;
    }
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnInterval)
        {
            timeSinceLastSpawn = 0f;
            if (spawnedEnemies.Count < maxEnemiesNumber)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        //GameObject enemy = PhotonNetwork.Instantiate(enemyPrefab, transform.position, transform.rotation);
        if (photonView.IsMine)
        {
            photonView.RPC("Enemy", RpcTarget.AllBuffered, transform.position, transform.rotation);
        }
    }

    [PunRPC]
    void Enemy(Vector3 position, Quaternion rotation)
    {
        EnemyAI enemy = Instantiate(enemyPrefab, position, rotation);
        int spawnPointIndex = spawnedEnemies.Count % spawnPoints.Length;
        enemy.Init(player, spawnPoints[spawnPointIndex]);
        spawnedEnemies.Add(enemy);
    }

}
