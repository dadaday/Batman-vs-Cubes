using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum EnemyState {
	Guarding,
	Following
}

public class EnemyController : MonoBehaviour {

	public int enemyMaxHealth = 100;
	public int playerDamage = 10;
	private int currHealth;

	public GameObject explosion;

	public float AIUpdateRate = 2.0f;
	public float DistanceToEnemyToFollow = 10.0f;

	public float Speed = 2.0f;

	private EnemyState state;
	private Vector3 Velocity =
		new Vector3 ();

	void Start () {
		currHealth = enemyMaxHealth;
		StartCoroutine (MakeDecision ());
	}

	void Update() {
		FirstPersonController hero =
			FindObjectOfType <FirstPersonController> ();

		if (hero) {
			Vector3 enemyPosition = transform.position;
			Vector3 heroPosition = hero.transform.position;

			if (state == EnemyState.Following) {	
				Velocity =
					(heroPosition - enemyPosition).normalized * Speed;
			}

			float distance =
				Vector3.Distance (
					heroPosition,
					enemyPosition
				);

			Vector3 enemyScale = transform.localScale;
			Vector3 heroScale = hero.transform.localScale;

			float distanceWithoutCollision =
				(heroScale.magnitude + enemyScale.magnitude) * 0.4f;

			if (distance < distanceWithoutCollision) {
//				hero.Die ();
			}
		}

		transform.position +=
			Velocity * Time.deltaTime;

		Velocity *= 0.95f;
	}

	IEnumerator MakeDecision() {
		while (true) {
			FirstPersonController hero =
				FindObjectOfType <FirstPersonController> ();

			if (hero) {
				if (Vector3.Distance(
						transform.position,
						hero.transform.position
					) < DistanceToEnemyToFollow)
				{
					state =
						EnemyState.Following;
				} else {
					state =
						EnemyState.Guarding;
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
				Debug.Log ("enemy hit");
				batTrig.PlayBatHitEffect ();

				currHealth -= playerDamage;
				Debug.Log ("Enemy health: " + currHealth);

				if (currHealth <= 0) {
					Debug.Log ("Enemy is killed by " + other.name);
//					Instantiate (explosion, transform.position, transform.rotation);

					Destroy (this.gameObject, 2.0f);
				}

			}
		}
		if (other.CompareTag("Death")) {
			Destroy (this.gameObject);
		}
	}

}
