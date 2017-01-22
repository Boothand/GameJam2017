using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
	public TextGenerator tg;

	bool inSentence = false;

	string fullSentence;
	//List<string> fullSentenceList = new List<string>();
	List<KeyValuePair<string, int>> currentPairsList = new List<KeyValuePair<string, int>>();
	string currentSentence;
	[SerializeField] int currentOffence = 0;

	float timeLeft = 0f;
	float timer = 2f;

	[Range(0, 1)]
	public float pitchVariance = 0.25f;
	public GameObject speechBubblePrefab;
	SpeakBubble currentSpeechBubble;

	AudioSource audioPlayer;
	[SerializeField] AudioClip[] voices;

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();
	}

	void PlaySound()
	{
		AudioClip sound = voices[Random.Range(0, voices.Length)];
		audioPlayer.clip = sound;
		audioPlayer.pitch = 1f + Random.Range(-pitchVariance, pitchVariance);
		audioPlayer.Play();
	}

	public void Speak()
	{
		if (inSentence)
		{
			if (currentPairsList.Count > 0)
			{
				if (currentSentence != "")
				{
					currentSentence += " ";
					currentSpeechBubble.AddText(" ");
				}

				currentSentence += currentPairsList[0].Key;
				currentSpeechBubble.AddText(currentPairsList[0].Key);

				currentOffence += currentPairsList[0].Value;

				PlaySound();

				currentPairsList.RemoveAt(0);
			}
			else // sentence is ended.
			{
				inSentence = false;
				currentSentence += ".";
				currentSpeechBubble.AddText(".");
				print(currentOffence);
				currentSpeechBubble.Release();
			}
		}
		else // no Sentence exists; Creating one!
		{
			GameObject newBubble = Instantiate(speechBubblePrefab, transform.position + Vector3.up * 2, Quaternion.identity) as GameObject;
			currentSpeechBubble = newBubble.GetComponent<SpeakBubble>();
			currentSentence = "";
			currentOffence = 0;

			GetSentence();
			Speak();
		}
	}

	public void GetSentence()
	{
		//fullSentence = tg.GenerateSentence(out currentOffence);

		List<KeyValuePair<string, int>> temp = new List<KeyValuePair<string, int>>();

		temp = tg.GenerateSentence();

		foreach (KeyValuePair<string, int> p in temp)
		{
			string[] s = p.Key.Split(" "[0]);
			if (s.Length > 1) // WE NEED TO SPLIT
			{
				foreach(string ss in s)
				{
					KeyValuePair<string, int> pair = new KeyValuePair<string, int>(ss, 0);
					currentPairsList.Add(pair);
				}
				int size = currentPairsList.Count-1;
				currentPairsList[size] = new KeyValuePair<string, int>(currentPairsList[size].Key, p.Value);
			}
			else // ALL FINE NEXT PAIR
			{
				currentPairsList.Add(p);
			}

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
				currentPairsList.Clear();
				inSentence = false;
				currentSentence = "";
				currentOffence = 0;
				currentSpeechBubble.AddText(".");
				print(currentOffence);
				currentSpeechBubble.Release();
			}
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			timeLeft = timer;
			Speak();
		}
	}
}
