using UnityEngine;
using System.Collections;

public class PickUpWeapon : MonoBehaviour {

	public AudioClip PickUpSound;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpscontroller = 
				other.GetComponent <UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
			fpscontroller.weapon.SetActive (true);

			AudioSource fpsAs = other.GetComponent<AudioSource> ();
			fpsAs.clip = PickUpSound;
			fpsAs.Play ();

			Destroy (this.gameObject);
		}
	}
}
