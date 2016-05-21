using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class characterCollisions : MonoBehaviour {

	private bool finished = false;
	private bool levels = false;

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
		GUIStyle button_style = new GUIStyle(GUI.skin.button);
		button_style.fontSize = Screen.width/30;

		if(finished && !levels)
		{
			GUI.Box(new Rect (Screen.width/4,Screen.height/4 - 5, Screen.width/2 , Screen.height/1.5f ), "Current Level: " + (this.getLevelFinished()).ToString() );

			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8) ,Screen.width/2 - Screen.width/6,Screen.height/8), this.getNextLevelText() ,button_style )) 
			{
				Time.timeScale = 1f;
				Application.LoadLevel(this.getLevel());
			}
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Levels",button_style )) 
			{
				this.levels = true;
			}
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8*2) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Quit",button_style )) 
			{
				Application.Quit();
			}
		}

		if(levels)
		{

			this.getLevel(0,button_style,1,1);
			this.getLevel(1,button_style,1,2);
			this.getLevel(2,button_style,1,3);
			this.getLevel(3,button_style,1,4);
			this.getLevel(4,button_style,1,5);
			this.getLevel(5,button_style,1,6);
			this.getLevel(6,button_style,1,7);
			if( GUI.Button(new Rect( Screen.width/2 - Screen.width/6,Screen.height/4 + (Screen.height/8*2) + 20,Screen.width/2 - Screen.width/6,Screen.height/8), "Back to menu",button_style )) 
			{
				this.levels = false;
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

	private void getLevel(int level, GUIStyle style, int row, int column){
		if(level > this.getLevel())
		{
			return;
		}
		if( GUI.Button(new Rect( Screen.width/24 + (column*Screen.width/12),(row*Screen.height/9) + 20,Screen.width/4 - Screen.width/6,Screen.height/8), level.ToString(),style )) 
		{
			Time.timeScale = 1f;
			Application.LoadLevel(level);
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

	private string getNextLevelText(){
		string buttonText = "";
		if(Application.loadedLevel == this.getLevelFinished()){
			buttonText = "Restart level " + this.getLevelFinished();
		}
		else{
			buttonText = "Start next level " + this.getLevelFinished();
		}

		return buttonText;
	}
}
