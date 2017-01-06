using UnityEngine;
using System.Collections;

public class batTrigger : MonoBehaviour {
	private ParticleSystem ps;

	void Start()
	{
		ps = GetComponent<ParticleSystem> ();
	}

	public void PlayBatHitEffect()
	{
		ps.Play ();
	}
}
