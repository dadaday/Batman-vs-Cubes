using UnityEngine;
using System.Collections;

public class batScript : MonoBehaviour {

	void onCollisionEnter(Collision collision)
	{
		Debug.Log (collision.gameObject.tag);
		Debug.Log ("heree");
	}
}
