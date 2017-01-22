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
			tx.transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)//Burst
			{
				Destroy(gameObject);
			}
		}
		//transform.LookAt(-Camera.main.transform.position, Vector3.up);
		transform.forward = (transform.position - Camera.main.transform.position).normalized;
	}

	public void AddText(string word)
	{
		tx.text += word;
	}

	public void Release()
	{
		flying = true;
	}

	public void SetColor(Color c)
	{
		tx.color = c;
	}
}