using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private AudioSource audioSource;

	public bool IsPaused = true;

	public GameObject menu;
	public GameObject quitMenu;
	public GameObject helpMenu;
	public GameObject messagePanel;
	public AudioClip victorySound;

	public Slider healthBar;
	public FirstPersonController Player;

	public bool enemiesFound = false;
	public bool enableCheckpoints = false;
	public float delayBetweenLevels = 5.0f;
	private int currentLevel = 1;
	private bool stopUpdate = false;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		audioSource = GetComponent<AudioSource> ();
		DontDestroyOnLoad (gameObject);
	}

	void Update() {
		if (!Player) {
			Player = FindObjectOfType<FirstPersonController> ();
			healthBar.onValueChanged.AddListener (delegate {Player.PlayHurtSound();});
		}

		if (Player.isDead) {
			LoadScene (currentLevel);
			return;
		}

		if (!stopUpdate && currentLevel != 0) {
			if (!enemiesFound && GameObject.FindGameObjectsWithTag ("Enemy").Length > 0) {
				enemiesFound = true;
			} else if (enemiesFound && GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
				enableCheckpoints = true;
				enemiesFound = false;
			}

			if (GameObject.FindGameObjectsWithTag ("Checkpoint").Length == 0) {
				audioSource.clip = victorySound;
				audioSource.Play ();

				stopUpdate = true;
				currentLevel++;

				if (currentLevel >= SceneManager.sceneCountInBuildSettings) {
					Player.GetComponent<FirstPersonController> ().enabled = false; // enable cursor
					SceneManager.LoadScene (SceneManager.sceneCountInBuildSettings - 1); // load the last scene in build - exit
				} else {
					EndLevel ();
				}
			}

			if (Input.GetKeyUp (KeyCode.Escape)) {
				helpMenu.SetActive (false); // escape button always disables help menu
		
				if (IsPaused) {			
					Play ();
				} else {
					Pause ();
					menu.SetActive (true);
					if (quitMenu.activeSelf) {
						quitMenu.SetActive (false);
						menu.SetActive (true);
					}
				}
			}
		}
	}

	public void Exit() {
		Application.Quit ();
	}

	public void Play() {
		IsPaused = false;
		Time.timeScale = 1.0f;
		Player.GetComponent<FirstPersonController> ().enabled = true;

		menu.SetActive(false);
		messagePanel.SetActive (false);
	}

	public void Pause() {
		IsPaused = true;
		Time.timeScale = 0.0f;
		Player.GetComponent<FirstPersonController> ().enabled = false;
	}

	public void HelpPress() {
		helpMenu.SetActive (!helpMenu.activeSelf);
	}

	public void ExitPress() {
		helpMenu.SetActive (false);
		menu.SetActive (false);
		healthBar.gameObject.SetActive (false);
		quitMenu.SetActive (true);
	}

	public void ChooseNo() {
		quitMenu.SetActive (false);
		menu.SetActive (true);
		healthBar.gameObject.SetActive (true);
	}

	public void ChooseYes() {
		Application.Quit ();
	}

	public void EnableMessagePanel() {
		messagePanel.SetActive (true);
		Pause ();
	}

	IEnumerator waitForSec(int level)
	{
		yield return new WaitForSeconds (delayBetweenLevels);
		Debug.Log ("loaded level " + level);
		SceneManager.LoadScene (level);
		stopUpdate = false;
		enemiesFound = false;
		enableCheckpoints = false;
	}

	public void EndLevel() {
		LoadSceneAfterDelay (currentLevel);
	}

	private void LoadSceneAfterDelay(int level) {
		StartCoroutine(waitForSec (level));
	}

	public void LoadScene(int level) {
		SceneManager.LoadScene (level);
		enemiesFound = false;
		enableCheckpoints = false;
		stopUpdate = false;
	}

}
