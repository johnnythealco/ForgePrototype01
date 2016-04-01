using UnityEngine;
using System.Collections;
using BeardedManStudios.Network;
using UnityEngine.UI;

public class JKNetWoking : SimpleNetworkedMonoBehavior
{
	public Text conectionText;

	private bool connected;

	public void Awake ()
	{
		conectionText.text = "Disconnected!";

	}

	public void Update ()
	{
		if (Networking.IsConnected (15937))
		{
			conectionText.text = "Connected!";
		} else
		{
			conectionText.text = "Disconnected!";
		}
	}


	public void HostServer ()
	{
		// Arguments:
		//   ushort <PORT_NUMBER>
		//   Networking.TransportationProtocolType <PROTOCOL_TYPE>
		//   int <MAX_PLAYERS>
		//   optional bool <IS_WIN_RT> [default false]
		//   optional bool <ALLOW_WEBPLAYER_CONNECTION> [default true]

		Networking.Host (15937, Networking.TransportationProtocolType.UDP, 31);

		Networking.Sockets [15937].connected += connectionCallBack;
		Networking.Sockets [15937].disconnected += disconnectionCallBack;



	}

	public void ConnectToLocal ()
	{
		Networking.Connect ("127.0.0.1", 15937, Networking.TransportationProtocolType.UDP);

		Networking.Sockets [15937].connected += connectionCallBack;
		Networking.Sockets [15937].disconnected += disconnectionCallBack;

	
	}

	public void JKDisconnect ()
	{
		Networking.Disconnect (15937);
	}

	void connectionCallBack ()
	{
		var PlayerID = Networking.Sockets [15937].Uniqueidentifier;
		RPC ("RPCDebugLog", NetworkReceivers.AllBuffered, "Player with PlayerID " + PlayerID + " connected");
	}

	void disconnectionCallBack ()
	{
		var PlayerID = Networking.Sockets [15937].Uniqueidentifier;

		RPC ("RPCDebugLog", "Player with PlayerID " + PlayerID + " disconnected");
	}

	[BRPC]
	void RPCDebugLog (string message)
	{
		Debug.Log (message);
	}

}
