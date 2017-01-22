using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject credits;

	public void StartGame()
	{
		SceneManager.LoadScene("Club");
	}

	public void Credits()
	{
		credits.SetActive(!credits.active);
	}
}
