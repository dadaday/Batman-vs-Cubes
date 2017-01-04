using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public bool IsPaused = true;

	public GameObject menu;
	public GameObject quitMenu;
	public GameObject helpMenu;

	public GameObject Player;

//	private bool levelCompleted = false;
	public float delayBetweenLevels = 5.0f;
	private int levelNum = 1;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Update() {

		if (Input.GetKeyUp (KeyCode.Escape))
		{
			helpMenu.SetActive (false); // escape button always disables help menu

			if (!Player) {
				Player = GameObject.FindGameObjectWithTag ("Player");
				Debug.Log ("init");
			}
		
			if (IsPaused)
			{			
				Play ();
			}
			else
			{
				Pause ();
	
				if (quitMenu.activeSelf)
				{
					quitMenu.SetActive (false);
					menu.SetActive (true);
				}
			}
		}
	}

	public void EndLevel() {
		LoadSceneAfterDelay (++levelNum);
	}

	public void Exit() {
		Application.Quit ();
	}

	public void Play() {
		IsPaused = false;
		Time.timeScale = 1.0f;
		Player.GetComponent<FirstPersonController> ().enabled = true;

		menu.SetActive(false);
	}

	public void Pause() {
		IsPaused = true;
		Time.timeScale = 0.0f;
		Player.GetComponent<FirstPersonController> ().enabled = false;

		menu.SetActive(true);
	}

	public void HelpPress() {
		helpMenu.SetActive (!helpMenu.activeSelf);
	}

	public void ExitPress() {
		helpMenu.SetActive (false);
		menu.SetActive (false);
		quitMenu.SetActive (true);
	}

	public void ChooseNo() {
		quitMenu.SetActive (false);
		menu.SetActive (true);
	}

	public void ChooseYes() {
		Application.Quit ();
	}

	IEnumerator waitForSec(int level)
	{
		yield return new WaitForSeconds (delayBetweenLevels);
		SceneManager.LoadScene (level);
	}

	private void LoadSceneAfterDelay(int level) {
		Debug.Log ("next");
		StartCoroutine(waitForSec (level));
	}

	public void LoadScene(int level) {
		SceneManager.LoadScene (++levelNum);
	}

}
