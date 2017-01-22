using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakBubble : MonoBehaviour
{
	bool flying = false;
	[SerializeField] float flySpeed = 1f;

	public float timeLeft = 3f;

	public Text tx;

	// Update is called once per frame
	void Update()
	{
		if (flying)
		{
			transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)//Burst
			{
				Destroy(gameObject);
			}
		}
	}

	public void AddText(string word)
	{
		tx.text += word;
	}

	public void Release()
	{
		flying = true;
	}
}