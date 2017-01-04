using UnityEngine;
using System.Collections;

public class PickUpWeapon : MonoBehaviour {

	public AudioClip PickUpSound;
	private GameManager gm;

	void Start()
	{
		gm = FindObjectOfType<GameManager> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			AudioSource fpsAs = other.GetComponent<AudioSource> ();
			other.SendMessage ("EquipWeapon");
			fpsAs.clip = PickUpSound;
			fpsAs.Play ();
			gm.EndLevel ();

			Destroy (this.gameObject);
		}
	}
}
