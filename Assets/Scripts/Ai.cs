using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
	public int offenceTolerance = 0;

	public GameObject speechBubblePrefab;

	// Use this for initialization
	void Start()
	{
		offenceTolerance = Random.Range(1, 5);
	}

	public void Listen(int offence)
	{
		offenceTolerance += offence;
		GenerateBubble(offence);
		
		if (offenceTolerance <= 0) //ARG! punch in the face
		{
			//DO PUNCH IN THE FACE STUFF!
		}
	}

	void GenerateBubble(int score)
	{
		SpeakBubble sb = Instantiate(speechBubblePrefab, transform.position, Quaternion.identity).GetComponent<SpeakBubble>();

		if (score > 0)
			sb.SetColor(Color.green);
		else if (score < 0)
			sb.SetColor(Color.red);

		if (score == 0)
			sb.AddText("?");
		else
			sb.AddText(score.ToString());

		sb.Release();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Listen(Random.Range(-2, 3));
		}
	}

}

