using UnityEngine;
using System.Collections;
using BeardedManStudios.Network;

public class NetPlayer : NetworkedMonoBehavior
{
	[NetSync]
	public string playerName;

	[NetSync]
	public int playerID;

			
	protected override void NetworkInitialized ()
	{

		base.NetworkInitialized ();

		playerID = (int)this.OwningNetWorker.Uniqueidentifier;

		if (!IsServerOwner)
		{
			this.OwningNetWorker.Me.SetName ("Client " + playerID);
		}

		this.playerName = this.OwningNetWorker.Me.Name;

		if (IsOwner)
		{
			netGame.Manager.netPlayers.Add (this.OwningNetWorker.Me);
		}


	}

	public void updateRemotePlayerNames (int playerID)
	{
		RPC ("setGameObjectName", NetworkReceivers.All, playerID);
	}


	[BRPC]
	public void addPlayer (int playerID)
	{
		if (!IsOwner)
			this.gameObject.name = this.playerName;
	}


}
