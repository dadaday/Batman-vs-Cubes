using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	public void Exit()
	{
		Application.Quit ();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
