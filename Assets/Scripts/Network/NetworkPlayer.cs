using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    
    
    public override void Spawned()
    {
        Utils.DebugLog($"Player spawned, has input auth {Object.HasInputAuthority}");
        // we want to add the spawned runner
        if (Object.HasInputAuthority)
        {
            Local = this;
        }
        
        // Set the player as a playerObject
        Runner.SetPlayerObject(Object.InputAuthority, Object);
        
        // Make it easier to tell which player is which
        transform.name = $"P_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}
