using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	public AudioClip CheckpointSound;
	public Material finalCheckpointMat;
	private GameManager gameMan;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<SphereCollider> ().enabled = false;
		gameMan = FindObjectOfType<GameManager> ();
	}

	void Update() {
		Debug.Log ("Enemies found: " + gameMan.enemiesFound);

		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0 && gameMan.enemiesFound) {
			GetComponent<MeshRenderer> ().enabled = true;
			GetComponent<SphereCollider> ().enabled = true;
		}

		if (FindObjectsOfType<Checkpoint> ().Length == 1) {
			GetComponent<Renderer> ().material = finalCheckpointMat;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			AudioSource fpsAs = other.GetComponent<AudioSource> ();
			fpsAs.clip = CheckpointSound;
			fpsAs.Play ();

			Destroy (this.gameObject);
		}
	}
		
}
