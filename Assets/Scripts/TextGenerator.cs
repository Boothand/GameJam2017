using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
	[SerializeField]
	bool debug = false;

	string[] subjects; //Who is doing it "I", "They" recipe[0]
					   //string[] noun; //
	string[] adjective; //Describes the noun "Red", "Strong" recipe[1]
	string[] verb; //Action "Speaking"


	public int[] recipe; //How are the sentence built.

	void Start()
	{
		//importing words
		subjects = ReadFile("subjects.txt");


	}

	string[] ReadFile(string path)
	{
		string fullPath = Application.streamingAssetsPath + "/" + path;
		string[] lines = System.IO.File.ReadAllLines(fullPath);

		if (debug)
			foreach (string line in lines)
			{
				print(line);
			}

		return lines;
	}

	void Update()
	{

	}
}
