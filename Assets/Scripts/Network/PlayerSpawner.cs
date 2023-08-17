using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    
    // Class to be used for spawning objects in scene
    
    // prefabs to be spawned
    [SerializeField] private GameObject _playerPrefab;
    //[SerializeField] private GameObject _playerBody;
    [SerializeField] private GameObject foodPrefab;
    
    // bot attributes
    private const int desiredNumberOfPlayers = 5;
    
    // control attributes, make them networked
    private bool isFoodSpawned = false;
    private bool isBotsSpawned = false;
    
    // lsit to hold spawned bots
    private List<NetworkObject> botsList = new List<NetworkObject>();



    // spawn 100 foods, you can make them static
    
     void SpawnFood()
     {
         for (int i = 0; i < 100; i++)
         {
             // belki bi ana gameObject altına atarsın?
             NetworkObject spawnedFood = Runner.Spawn(foodPrefab, Utils.GetRandomSpawnPosition(), Quaternion.identity);
             spawnedFood.transform.position = Utils.GetRandomSpawnPosition();
         }

         isFoodSpawned = true;
     }
     

     // check how many players exist, if there isn't enough players and bots then spawn a bot
     // burda anlamadığım şey _playerPrefab kısmındaki prefab aslınd sadece bodysi olmiycak mı? çünkü movement input almiycak 
     /*
     
     void SpawnBots()
     {
         
         if (Runner.SessionInfo.PlayerCount < desiredNumberOfPlayers + botsList.Count)
         {
             int numberOfBotsToSpawn = desiredNumberOfPlayers - Runner.SessionInfo.PlayerCount - botsList.Count;
             Utils.DebugLog($"Number of bots to spawm {numberOfBotsToSpawn}. Bot spawned count {botsList.Count}. Player count {Runner.SessionInfo.PlayerCount}");
             for (int i = 0; i < numberOfBotsToSpawn; i++)
             {
                 NetworkObject spawnedAIPlayer = Runner.Spawn(_playerPrefab, Utils.GetRandomSpawnPosition(),
                     Quaternion.identity, null, InitializeBotBeforeSpawn);

                 //spawnedAIPlayer.BotJoinGame;
                 
                 botsList.Add(spawnedAIPlayer);
             }
         }

         isBotsSpawned = true;
     }

     private void InitializeBotBeforeSpawn(NetworkRunner runner, NetworkObject networkObject)
     {
         networkObject.GetComponent<PlayerStateController>().isBot = true;
     }
     */
     

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
            /*
            if (!isBotsSpawned)
            {
                SpawnBots();
            }
            */

        }
    }

  
   
}
