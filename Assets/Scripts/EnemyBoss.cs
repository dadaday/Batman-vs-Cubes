using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyBoss : MonoBehaviour {
	
	public int enemyMaxHealth = 200;
	public int playerDamage = 10;
	private int currHealth;

	public GameObject explosion;
	public GameObject bulletPrefab;
	public AudioClip bulletSound;

	public float AIUpdateRate = 0.5f;
	public float DistanceToEnemyToFollow = 10.0f;

	public float ReloadTime = 3.0f;
	public float ProjectileSpeed = 20.0f;

	private float LastShotTime = 2.0f;

	private EnemyState state;
	private NavMeshAgent enemyNavAgent;
	private AudioSource bossAs;

	void Start () {
		enemyNavAgent = GetComponent<NavMeshAgent> ();
		bossAs = GetComponent<AudioSource> ();
		currHealth = enemyMaxHealth;
		StartCoroutine (MakeDecision ());
	}

	void Update() {
		if (currHealth < enemyMaxHealth / 4)
			ReloadTime = 1.5f; // if boss has 25% hp he shoots twice as frequent

		FirstPersonController hero =
			FindObjectOfType <FirstPersonController> ();

		if (hero) {
			Vector3 heroPosition = hero.transform.position;

			if (state == EnemyState.Following) {	
				enemyNavAgent.SetDestination (heroPosition);
			} else if (state == EnemyState.Firing && (Time.time - LastShotTime > ReloadTime)) {
				Vector3 bulletPosition = transform.position + (heroPosition - transform.position).normalized * 2.0f;
				bulletPosition.y += 0.5f;

				GameObject bullet = Instantiate (bulletPrefab, bulletPosition, transform.rotation) as GameObject;
				bullet.GetComponent<Rigidbody>().velocity = (heroPosition - transform.position).normalized * ProjectileSpeed;

				bossAs.clip = bulletSound;
				bossAs.Play ();

				LastShotTime = Time.time;
				Destroy (bullet, 2.0f);
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
					state = EnemyState.Firing;
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
