using UnityEngine;
using System.Collections;

public class menuEffect : MonoBehaviour
{

	public bool isEnabled;
	public Canvas canvas;
	public PlayerMovement p;

	// Use this for initialization
	void Start ()
	{
		p = p.GetComponent<PlayerMovement> ();
		canvas = canvas.GetComponent<Canvas> ();
		if (Application.loadedLevelName == "Level1") {
			canvas.enabled = false;
			isEnabled = false;
			p.GetComponent<PlayerMovement>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			TogglePauseMenu ();
		}
	}

	public void playGame ()
	{
		Application.LoadLevel ("Level1");
	}

	public void TogglePauseMenu ()
	{
		isEnabled = !isEnabled; //change isEnabled
		p.GetComponent<PlayerMovement>().enabled = !p.GetComponent<PlayerMovement>().enabled;
		if (isEnabled == true) {
			//paused
			canvas.enabled = true;
			Time.timeScale = 0;
		} else if (isEnabled == false) {
			//unpaused
			canvas.enabled = false;
			Time.timeScale = 1;
		}
	}

	public void Quit ()
	{
		Application.LoadLevel ("Menu");
	}

	public void Resume ()
	{
		canvas.enabled = false;
		isEnabled = false;
	}
	

}
