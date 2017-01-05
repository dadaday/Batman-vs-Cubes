﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class HpStEnemyController : EnemyController {

	public int HP = 3;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Triggered HP by " + other.name);
			
		if (other.CompareTag ("SwingWeapon")) {
			Animator anim = other.GetComponentInParent<Animator> ();

			if (anim.GetBool ("weaponUsed"))
				Debug.Log ("enemy was hit");
		}
	}

//	public float AIUpdateRate = 2.0f;
//	public float DistanceToEnemyToFollow = 10.0f;
//
//	public float Speed = 2.0f;
//
//	private EnemyState state;
//	private Vector3 Velocity =
//		new Vector3 ();
//
//	void Start () {
//		StartCoroutine (MakeDecision ());
//	}
//
//	void Update() {
//		FirstPersonController hero =
//			FindObjectOfType <FirstPersonController> ();
//
//		if (hero) {
//			Vector3 enemyPosition = transform.position;
//			Vector3 heroPosition = hero.transform.position;
//
//			if (state == EnemyState.Following) {	
//				Velocity =
//					(heroPosition - enemyPosition).normalized * Speed;
//			}
//
//			float distance =
//				Vector3.Distance (
//					heroPosition,
//					enemyPosition
//				);
//
//			Vector3 enemyScale = transform.localScale;
//			Vector3 heroScale = hero.transform.localScale;
//
//			float distanceWithoutCollision =
//				(heroScale.magnitude + enemyScale.magnitude) * 0.4f;
//
//			if (distance < distanceWithoutCollision) {
//				//				hero.Die ();
//			}
//		}
//
//		transform.position +=
//			Velocity * Time.deltaTime;
//
//		Velocity *= 0.95f;
//	}
//
//	IEnumerator MakeDecision() {
//		while (true) {
//			FirstPersonController hero =
//				FindObjectOfType <FirstPersonController> ();
//
//			if (hero) {
//				if (Vector3.Distance(
//					transform.position,
//					hero.transform.position
//				) < DistanceToEnemyToFollow)
//				{
//					state =
//						EnemyState.Following;
//				} else {
//					state =
//						EnemyState.Guarding;
//				}
//			}
//
//			yield return new WaitForSeconds(AIUpdateRate);
//		}
//	}

}
