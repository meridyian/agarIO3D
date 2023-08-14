using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField]  GameObject _playerPrefab;

    [SerializeField] private GameObject foodPrefab;
    private bool isFoodSpawned = false;
    private bool isBotsSpawned = false;

    private const int desiredNumberOfPlayers = 30;

    private List<NetworkObject> botsList = new List<NetworkObject>();


     void SpawnFood()
     {
         for (int i = 0; i < 300; i++)
         {
             NetworkObject spawnedFood = Runner.Spawn(foodPrefab, Utils.GetRandomSpawnPosition(), Quaternion.identity);
             spawnedFood.transform.position = Utils.GetRandomSpawnPosition();
         }

         isFoodSpawned = true;
     }

     void SpawnBots()
     {
         // check how many players there are, if there isn't enough players and bots then spawn a new one
         if (Runner.SessionInfo.PlayerCount < desiredNumberOfPlayers + botsList.Count)
         {
             int numberOfBotsToSpawn = desiredNumberOfPlayers - Runner.SessionInfo.PlayerCount - botsList.Count;
             Utils.DebugLog($"Number of bots to spawm {numberOfBotsToSpawn}. Bot spawned count {botsList.Count}. Player count {Runner.SessionInfo.PlayerCount}");
             for (int i = 0; i < numberOfBotsToSpawn; i++)
             {
                 NetworkObject spawnedAIPlayer = Runner.Spawn(_playerPrefab, Utils.GetRandomSpawnPosition(),
                     Quaternion.identity, null, InitializeBotBeforeSpawn);
                 
                 botsList.Add(spawnedAIPlayer);
             }
         }
     }

     private void InitializeBotBeforeSpawn(NetworkRunner runner, NetworkObject networkObject)
     {
         networkObject.GetComponent<PlayerStateController>().isBot = true;
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
