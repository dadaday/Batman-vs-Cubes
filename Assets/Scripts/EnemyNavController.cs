﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyNavController : MonoBehaviour {

	public int enemyMaxHealth = 100;
	public int playerDamage = 10;
	private int currHealth;

	public GameObject explosion;

	public float AIUpdateRate = 2.0f;
	public float DistanceToEnemyToFollow = 20.0f;

	public float Speed = 10.0f;

	private EnemyState state;

	private NavMeshAgent enemyNavAgent;

	void Start () {
		enemyNavAgent = GetComponent<NavMeshAgent> ();
		currHealth = enemyMaxHealth;
		StartCoroutine (MakeDecision ());
	}

	void Update() {
		FirstPersonController hero =
			FindObjectOfType <FirstPersonController> ();

		if (hero) {
			Vector3 heroPosition = hero.transform.position;

			if (state == EnemyState.Following) {	
				enemyNavAgent.SetDestination (heroPosition);
			}
		}
	}

	IEnumerator MakeDecision() {
		while (true) {
			FirstPersonController hero =
				FindObjectOfType <FirstPersonController> ();

			if (hero) {
				if (Vector3.Distance(transform.position,hero.transform.position) < DistanceToEnemyToFollow)
				{
					state = EnemyState.Following;
				} else {
					state = EnemyState.Guarding;
				}
			}

			yield return new WaitForSeconds(AIUpdateRate);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Triggered by " + other.name);

		if (other.CompareTag ("SwingWeapon")) {
			Animator anim = other.GetComponentInParent<Animator> ();

			if (anim.GetBool ("weaponUsed")) {
				batTrigger batTrig = other.GetComponent<batTrigger> ();
				batTrig.PlayBatHitEffect ();

				currHealth -= playerDamage;
				Debug.Log ("Enemy health: " + currHealth);

				if (currHealth <= 0) {
					Debug.Log ("Enemy is killed by " + other.name);
					//Instantiate (explosion, transform.position, transform.rotation);

					Destroy (this.gameObject, 1.0f);
				}

			}
		}
		if (other.CompareTag("Death")) {
			Destroy (this.gameObject);
		}
	}

}
