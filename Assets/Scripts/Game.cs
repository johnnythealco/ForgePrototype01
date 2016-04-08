using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Network;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;

public class Game : MonoBehaviour
{

	public static Game Manager;


	public GameState state;
	//
	//	public JsonData JData;
	//	public JsonData iData;
	//
	public GameState newState;


	// Use this for initialization
	void Awake ()
	{

		Manager = this;

//		if (JKNetWoking.connection == null)
//			JKNetWoking.connected += Setup;
//		else
//			Setup ();

	}
	//
	//
	//	void Setup ()
	//	{
	//		JKNetWoking.connection.AddCustomDataReadEvent (addPlayerNetEventID, ReadPlayerListNetwork);
	//		Debug.Log (" Setup Called");
	//	}
	//
	//
	//	void ReadPlayerListNetwork (NetworkingPlayer sender, NetworkingStream stream)
	//	{
	//		BinaryFormatter bf = new BinaryFormatter ();
	//		var data = (byte[])ObjectMapper.MapByteArray (stream);
	//		MemoryStream memStream = new MemoryStream ();
	//		memStream.Write (data, 0, data.Length);
	//		memStream.Seek (0, SeekOrigin.Begin);
	//		var newstate = (GameState)bf.Deserialize (memStream);
	//		Debug.Log ("Received Data : " + data.Length + " Bytes of Data");
	//
	//	}
	//
	//
	//	public void WritePlayerListToNetwork ()
	//	{
	//		BinaryFormatter bf = new BinaryFormatter ();
	//		MemoryStream memStream = new MemoryStream ();
	//		bf.Serialize (memStream, state);
	//		byte[] myByteArray = memStream.ToArray ();
	//
	//		cachedData.Clone (ObjectMapper.MapBytes (cachedData, myByteArray));
	//
	//		Networking.WriteCustom (addPlayerNetEventID, JKNetWoking.connection, cachedData, true);
	//
	//		Debug.Log ("Sending Data : " + myByteArray.Length + " Bytes of Data");
	//	}

	void Update ()
	{
		
		if (Input.GetKeyDown (KeyCode.Space) && Networking.IsConnected (15937))
		{
			var PlayerID = Networking.Sockets [15937].Uniqueidentifier;
			var PlayerName = "Player " + PlayerID.ToString ();
			var Player = new Player (PlayerName, (int)PlayerID);
			var JSONPlayer = JsonMapper.ToJson (Player); 
			netGame.Manager.RPCnetAddPlayer (JSONPlayer);





		}

		if (Input.GetKeyDown (KeyCode.Tab))
		{
			ReadJasonToObject ();
		}

		if (Input.GetKeyDown (KeyCode.R))
		{
			Debug.Log ("R Pressed");
			
			foreach (var player in NetworkingManager.Instance.Players)
			{
				Debug.Log ("Networking Player name :" + player.Name);
				Debug.Log ("Networking Player ID :" + player.NetworkId);

			}
		}



	}

	//	void WriteJsonToLog ()
	//	{
	//
	//		JData = JsonMapper.ToJson (state);
	//		iData = JsonMapper.ToObject (JData.ToString ());
	//
	//
	//		Debug.Log (JsonMapper.ToJson (state));
	//
	//	}

	void ReadJasonToObject ()
	{ 
		newState = new GameState (JsonMapper.ToJson (state));
	}





}