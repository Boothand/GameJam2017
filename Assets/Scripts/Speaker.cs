using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
	public TextGenerator tg;

	bool inSentence = false;

	public string fullSentence;
	List<string> fullSentenceList = new List<string>();
	public string currentSentence;
	public int currentOffence;

	float timeLeft = 0f;
	float timer = 2f;

	public void Speak()
	{
		if (inSentence)
		{
			if (fullSentenceList.Count > 0)
			{
				if (currentSentence != "")
				{
					currentSentence += " ";
				}

				currentSentence += fullSentenceList[0];
				fullSentenceList.RemoveAt(0);
			}
			else // sentence is ended.
			{
				inSentence = false;
				currentSentence += ".";
			}
		}
		else // no Sentence exists; Create one!
		{
			currentSentence = "";
			currentOffence = 0;

			GetSentence();
			Speak();
		}
	}

	public void GetSentence()
	{
		fullSentence = tg.GenerateSentence(out currentOffence);

		string[] sss = fullSentence.Split(" "[0]);

		foreach (string s in sss)
		{
			fullSentenceList.Add(s);
		}

		inSentence = true;
	}

	void Update()
	{
		if (timeLeft > 0f)
		{
			timeLeft -= Time.deltaTime;
		}
		else 
		{
			timeLeft = 1f;
			if (inSentence) //end sentence if one exists.
			{
				fullSentenceList.Clear();
				inSentence = false;
				currentSentence = "";
				currentOffence = 0;
			}
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			timeLeft = timer;
			Speak();
		}
	}
}
