using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	void OnGUI()
	{
		GUIStyle button_style = new GUIStyle(GUI.skin.button);
		button_style.fontSize = Screen.width/30;

		GUI.Box(new Rect (Screen.width/4,Screen.height/4 - 5, Screen.width/2 , Screen.height/1.5f ), this.getLevel().ToString() );

		if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8) ,Screen.width/2 - Screen.width/6,Screen.height/8), "Start Level",button_style )) 
		{
			Application.LoadLevel(this.getLevel());
		}
		if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8*2) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Quit",button_style )) 
		{
			Application.Quit();
		}
	}

	private int getLevel(){
		return PlayerPrefs.GetInt("lastLevel");
	}
}
