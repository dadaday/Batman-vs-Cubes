using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour {

	public GameObject menu;
	public GameObject quitMenu;
	public GameObject aboutMenu;

	public void StartGame() {
		SceneManager.LoadScene (1);
	}

	public void Exit() {
		Application.Quit ();
	}

	public void AboutPress() {
		aboutMenu.SetActive (!aboutMenu.activeSelf);
	}

	public void ExitPress() {
		aboutMenu.SetActive (false);
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
}
