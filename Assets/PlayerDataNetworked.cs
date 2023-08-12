using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using Behaviour = UnityEngine.Behaviour;

public class PlayerDataNetworked : NetworkBehaviour
{
    [Networked(OnChanged = nameof(UsernameChanged))]

    public string UserName { get; set; }

    public Text _playernameEntryText;
    public static PlayerDataNetworked NetworkedDataInstance;

    private void Awake()
    {
        if (NetworkedDataInstance == null)
        {
            NetworkedDataInstance = this;
        }
    }

    public override void Spawned()
    {
        _playernameEntryText.text = UserName;
        if (Object.HasStateAuthority)
        {
            var userName = FindObjectOfType<PlayerData>().GetUserName();
            SendNameRpc(userName);
            // küçük u ile de denesene
            _playernameEntryText.text = UserName;
        }
    }

    private static void UsernameChanged(Changed<PlayerDataNetworked> changed)
    {
        changed.Behaviour.UserName = changed.Behaviour._playernameEntryText.text;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SendNameRpc(string name)
    {
        UserName = name;
    }
}
