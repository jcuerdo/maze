using UnityEngine;
using System.Collections;

public class characterCollisions : MonoBehaviour {

	private bool finished = false;
	private bool onMenu = true;
	[SerializeField] float time = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.name == "end")
		{
			finished = true;
			this.finishLevel();
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

	void finishLevel()
	{
		
	}
}
