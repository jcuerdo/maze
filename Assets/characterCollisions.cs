using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class characterCollisions : MonoBehaviour {

	private bool finished = false;
	private bool instructions = false;

	[SerializeField] int timeLow;
	[SerializeField] int time;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.updateTime();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.name == "end")
		{
			this.finishLevel(true);
		}
	}

	void OnGUI()
	{
		if(finished)
		{
			GUIStyle button_style = new GUIStyle(GUI.skin.button);
			button_style.fontSize = Screen.width/30;

			GUI.Box(new Rect (Screen.width/4,Screen.height/4 - 5, Screen.width/2 , Screen.height/1.5f ), "Current Level: " + (this.getLevelFinished()).ToString() );

			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8) ,Screen.width/2 - Screen.width/6,Screen.height/8), "Start level",button_style )) 
			{
				Time.timeScale = 1f;
				Application.LoadLevel(this.getLevel());
			}
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8*2) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Quit",button_style )) 
			{
				Application.Quit();
			}
		}
	}

	private void finishLevel(bool success = false)
	{
		if(!this.finished){
			this.finished = true;
			if(success){
				this.finishSuccess();
			}
			else{
				this.finishFail();
			}

		}

	}

	private void finishSuccess(){
		Time.timeScale = 0.3f;
		GameObject.Find("character").GetComponent<ThirdPersonCharacter>().Move(Vector3.up,false,true);
		GameObject.Find("character").GetComponent<ThirdPersonCharacter>().enabled = false;
		GameObject.Find("character").GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.setLevelFinished();
	}

	private void finishFail(){
		Time.timeScale = 0.3f;
		GameObject.Find("character").GetComponent<ThirdPersonCharacter>().enabled = false;
		GameObject.Find("character").GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void updateTime()
	{
		int time = (int)this.time - (int)Time.timeSinceLevelLoad;
		if(time < timeLow)
		{
			GameObject.Find("time").GetComponent<TextMesh>().color = Color.red;
		}

		if(time < 0)
		{
			this.finishLevel(false);
		}
		else
		{
			GameObject.Find("time").GetComponent<TextMesh>().text = time.ToString();
		}
	}

	private void setLevelFinished(){
		PlayerPrefs.SetInt("lastLevel", Application.loadedLevel + 1);
	}

	private int getLevelFinished(){
		return PlayerPrefs.GetInt("lastLevel", 0);
	}

	private int getLevel(){
		return PlayerPrefs.GetInt("lastLevel");
	}
}
