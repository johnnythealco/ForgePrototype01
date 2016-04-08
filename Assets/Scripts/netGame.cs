using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Network;

public class netGame : NetworkedMonoBehavior
{

	public static netGame Manager;

	[NetSync]
	public string netText;

	[NetSync]
	public List<NetworkingPlayer> netPlayers = new List<NetworkingPlayer> ();


	void Awake ()
	{
		Manager = this;

		netText = " Loading...";
	}

	[BRPC]
	void UpdateNetText (string _text)
	{
		netText = _text;
	}

	public void RPCUpdateNetText (string _text)
	{
		RPC ("UpdateNetText", NetworkReceivers.Server, _text);
	}

	[BRPC]
	void netAddPlayer (string json)
	{
		Game.Manager.state.Players.Add (new Player (json));
	}

	public void RPCnetAddPlayer (string json)
	{
		RPC ("netAddPlayer", NetworkReceivers.AllBuffered, json);
	}



	public void RPCaddPlayer (NetworkingPlayer player)
	{
		RPC ("AddPlayer", NetworkReceivers.Server, player);
	}

	[BRPC]
	void AddPlayer (NetworkingPlayer player)
	{
		netPlayers.Add (player);
	}


}
