using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInputController : MonoBehaviour {

	public Text outputText;

	public InputField inputField;


	public void onRecievedInput()
	{
		netGame.Manager.RPCUpdateNetText( inputField.text);
	}



	void Update()
	{
		outputText.text = netGame.Manager.netText;
	}




}
