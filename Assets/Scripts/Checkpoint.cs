using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	public AudioClip CheckpointSound;
	public Material finalCheckpointMat;
	private GameManager gameMan;

	private MeshRenderer meshRend;
	private SphereCollider sphCol;

	// Use this for initialization
	void Start () {
		meshRend = GetComponent<MeshRenderer> ();
		sphCol = GetComponent<SphereCollider> ();
		gameMan = FindObjectOfType<GameManager> ();
	}

	void Update() {
		
		meshRend.enabled = gameMan.enableCheckpoints;
		sphCol.enabled = gameMan.enableCheckpoints;

		if (FindObjectsOfType<Checkpoint> ().Length == 1) {
			GetComponent<Renderer> ().material = finalCheckpointMat;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			AudioSource fpsAs = gameMan.GetComponent<AudioSource> ();
			fpsAs.clip = CheckpointSound;
			fpsAs.Play ();

			Destroy (this.gameObject);
		}
	}
		
}
