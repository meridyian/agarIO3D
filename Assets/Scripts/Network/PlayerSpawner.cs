using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField]  GameObject _playerPrefab;

    [SerializeField] private GameObject foodPrefab;
    private bool isFoodSpawned;


     void SpawnFood()
     {
         for (int i = 0; i < 300; i++)
         {
             NetworkObject spawnedFood = Runner.Spawn(foodPrefab, Utils.GetRandomSpawnPosition(), Quaternion.identity);
             spawnedFood.transform.position = Utils.GetRandomSpawnPosition();
         }

         isFoodSpawned = true;
     }

    public void Spawned()
    {
        SpawnPlayer(Runner.LocalPlayer);
    }

    
    public void SpawnPlayer(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, new Vector3(0,0.5f,0), Quaternion.identity, player);
            if (!isFoodSpawned)
            {
                SpawnFood();
            }
        }
    }
}
