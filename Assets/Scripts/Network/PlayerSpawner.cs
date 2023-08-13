using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField]  GameObject _playerPrefab;
    
    
    public void Spawned()
    {
        SpawnPlayer(Runner.LocalPlayer);
    }

    
    public void SpawnPlayer(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, new Vector3(0,1,0), Quaternion.identity, player);
        }
    }
}
