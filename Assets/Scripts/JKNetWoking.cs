using UnityEngine;
using System.Collections;
using BeardedManStudios.Network;
using UnityEngine.UI;

public class JKNetWoking : SimpleNetworkedMonoBehavior
{
	public Text conectionText;
	public Button HostGameBtn;
	public Button ConnectBtn;
	public Button DisconnectBtn;
	public GameObject playerListItemPrefab;
	public Transform ListPanelTransform;

	private bool connected;

	public void Awake ()
	{
		conectionText.text = "Disconnected!";
		HostGameBtn.gameObject.SetActive (true);
		ConnectBtn.gameObject.SetActive (true);
		DisconnectBtn.gameObject.SetActive (false);

	}

	public void Update ()
	{
		CheckConnectionStatus ();
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
		
		NetworkingManager.Instance.Players.Add (Networking.Sockets [15937].Me);
		var n = NetworkingManager.Instance;
		var m = Networking.Sockets [15937].Me;

		n.Players.Add (m);

		var PlayerID = Networking.Sockets [15937].Uniqueidentifier;
		RPC ("RPCDebugLog", NetworkReceivers.AllBuffered, "Player with PlayerID " + PlayerID + " connected");



		Networking.Instantiate ("NetPlayer", NetworkReceivers.AllBuffered, PlayerSpawned);

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

	void CheckConnectionStatus ()
	{
		if (Networking.IsConnected (15937))
		{
			if (connected)
				return;
			
			conectionText.text = "Connected!";
			HostGameBtn.gameObject.SetActive (false);
			ConnectBtn.gameObject.SetActive (false);
			DisconnectBtn.gameObject.SetActive (true);



			connected = true;
		} else
		{
			if (!connected)
				return;
			
			conectionText.text = "Disconnected!";
			HostGameBtn.gameObject.SetActive (true);
			ConnectBtn.gameObject.SetActive (true);
			DisconnectBtn.gameObject.SetActive (false);
		
			connected = false;
		}
	}

	private void PlayerSpawned (SimpleNetworkedMonoBehavior playerObject)
	{
//		playerObject.gameObject.name = "Player " + playerObject.OwningNetWorker.Uniqueidentifier.ToString ();
		Debug.Log ("The player object " + playerObject.name + " has spawned at " +
		"X: " + playerObject.transform.position.x +
		"Y: " + playerObject.transform.position.y +
		"Z: " + playerObject.transform.position.z);
	}
}
