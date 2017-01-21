//using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AFKLoop : MonoBehaviour
{
	[SerializeField] float loopTime = 5f;
	float timer;
	
	void Awake()
	{

	}
	
	void Update()
	{
		timer += Time.deltaTime;

		if (timer > loopTime)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}