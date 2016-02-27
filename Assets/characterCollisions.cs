using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class characterCollisions : MonoBehaviour {

	private bool finished = false;
	private bool onMenu = true;

	[SerializeField] int time = 15;
	[SerializeField] int timeLow = 5;

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
		if(onMenu)
		{
			//Show menu
		}
		if(finished)
		{
			//Show button
		}
	}

	void finishLevel(bool success = false)
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
		GameObject.Find("character").GetComponent<ThirdPersonCharacter>().Move(Vector3.right * 10,false,true);
	}

	private void finishFail(){
		while(true)
		GameObject.Find("character").GetComponent<ThirdPersonCharacter>().Move(Vector3.right * 10,true,false);
	}

	void updateTime()
	{
		int time = (int)this.time - (int)Time.realtimeSinceStartup;

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
}
