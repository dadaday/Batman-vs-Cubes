using UnityEngine;
using System.Collections;

public class batTrigger : MonoBehaviour {
	private ParticleSystem ps;
	private AudioSource batAs;
	public AudioClip hit_sound;

	void Start()
	{
		ps = GetComponent<ParticleSystem> ();
		batAs = GetComponent<AudioSource> ();
	}

	public void PlayBatHitEffect()
	{
		batAs.clip = hit_sound;
		batAs.Play ();
		ps.Play ();
	}
}
