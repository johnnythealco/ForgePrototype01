using UnityEngine;
using System.Collections;

using BeardedManStudios.Network;

public class netGame : NetworkedMonoBehavior {

	public static netGame Manager; 

	[NetSync]
	public string netText;


	void Awake()
	{
		Manager = this;

		netText = " Loading...";
	}

	[BRPC]
	void UpdateNetText(string _text)
	{
		netText = _text;
	}

	public void RPCUpdateNetText(string _text)
	{
		RPC ("UpdateNetText", NetworkReceivers.Server, _text);
	}

	[BRPC]
	void netAddPlayer(string json)
	{
		Game.Manager.state.Players.Add (new Player (json));
	}

	public void RPCnetAddPlayer(string json)
	{
		RPC("netAddPlayer", NetworkReceivers.AllBuffered, json );
	}


}
