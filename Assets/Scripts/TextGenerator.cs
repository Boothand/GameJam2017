using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TextGenerator : MonoBehaviour
{
	[SerializeField]
	bool debug = false;

	//Dictionary<string, int> subjects = new Dictionary<string, int>(); //Who is doing it "I", "They" recipe[0]
	//Dictionary<string, int> adjectives = new Dictionary<string, int>(); //Describes the noun "Red", "Strong" recipe[1]
	//Dictionary<string, int> verbs = new Dictionary<string, int>(); //Action "Speaking"

	List<KeyValuePair<string, int>> subjects = new List<KeyValuePair<string, int>>();

	List<KeyValuePair<string, int>> starters = new List<KeyValuePair<string, int>>();

	List<KeyValuePair<string, int>> preactions = new List<KeyValuePair<string, int>>();

	List<KeyValuePair<string, int>> adjectives = new List<KeyValuePair<string, int>>();
	List<KeyValuePair<string, int>> verbs = new List<KeyValuePair<string, int>>();

	List<KeyValuePair<string, int>> contractions = new List<KeyValuePair<string, int>>();

	//public int[] recipe; //How are the sentence built.

	Dictionary<int, List<KeyValuePair<string, int>>> recipe = new Dictionary<int, List<KeyValuePair<string, int>>>();

	List<int[]> recipeList = new List<int[]>();


	void Start()
	{
		//importing words
		starters = ReadFile("starters.txt");

		subjects = ReadFile("subjects.txt");

		preactions = ReadFile("preactions.txt");

		adjectives = ReadFile("adjectives.txt");
		verbs = ReadFile("verbs.txt");

		contractions = ReadFile("contractions.txt");

		ReadRecipe("recipes.txt");
		SetupRecipe();

		if (debug)
			print("Completed loading all textfiles...");
	}

	void SetupRecipe()
	{
		recipe.Add(0, starters);
		recipe.Add(1, subjects);
		recipe.Add(2, preactions);
		recipe.Add(3, adjectives);
		recipe.Add(4, verbs);
		recipe.Add(5, contractions);
	}

	void ReadRecipe(string path)
	{
		string fullPath = Application.streamingAssetsPath + "/" + path;
		string[] lines = System.IO.File.ReadAllLines(fullPath);

		foreach (string line in lines)
		{
			string[] parts = line.Split(" "[0]);
			List<int> ii = new List<int>();

			foreach (string p in parts)
			{
				int iint;
				bool success = Int32.TryParse(p, out iint);

				if (success)
					ii.Add(iint);
			}
			recipeList.Add(ii.ToArray());
		}

		if (debug)
		{
			print("Printing all recipes");
			foreach (int[] i in recipeList)
			{
				string text = "";
				//print (i.ToString() )
				foreach (int j in i)
				{
					text += " " + j;
				}
				print(text);
			}
		}
	}

	List<KeyValuePair<string, int>> ReadFile(string path)
	{
		string fullPath = Application.streamingAssetsPath + "/" + path;
		int offence = 0; // default is 0 - Neutral
		List<KeyValuePair<string, int>> temp = new List<KeyValuePair<string, int>>();

		string[] lines = System.IO.File.ReadAllLines(fullPath);
		foreach (string line in lines)
		{
			string[] parts = line.Split(" "[0]);

			if (parts.Length == 1) // word has no offence attatched.
			{
				temp.Add(new KeyValuePair<string, int>(parts[0], 0));
			}
			else // longer parts.
			{
				int length = parts.Length;
				bool success = Int32.TryParse(parts[length - 1], out offence);

				if (success) // did contain a offence level;
				{
					//recombine words
					string words = "";
					for (int i = 0; i < length - 1; i++)
					{
						if (i > 0)
							words += " ";

						words += parts[i];
					}
					//temp.Add(words, offence);
					temp.Add(new KeyValuePair<string, int>(words, offence));
				}
				else // did not contain an offence level;
				{
					//recombine words
					string words = "";
					for (int i = 0; i < length; i++)
					{
						if (i > 0)
							words += " ";

						words += parts[i];
					}
					temp.Add(new KeyValuePair<string, int>(words, 0));
				}
			}

		}
		if (debug)
		{
			print("Printing all files from" + " " + path);
			foreach (KeyValuePair<string, int> val in temp)
			{
				print(val.Key + " " + val.Value);
			}
		}
		return temp;
	}

	public string GenerateSentence(out int offence) // negative is more offencive
	{
		string sentence = "";
		offence = 0;

		int r = UnityEngine.Random.Range(0, recipeList.Count);
		int[] components = recipeList[r];

		foreach (int i in components)
		{
			List<KeyValuePair<string, int>> dd;
			bool success = recipe.TryGetValue(i, out dd);

			if (success)
			{
				int rand = UnityEngine.Random.Range(0, dd.Count);
				if (sentence != "")
					sentence += " ";
				sentence += dd[rand].Key;
				offence += dd[rand].Value;
			}
		}
		return FirstCharToUpper(sentence);
	}

	public static string FirstCharToUpper(string input)
	{
		if (String.IsNullOrEmpty(input))
			throw new ArgumentException("ARGH!");
		return input.First().ToString().ToUpper() + input.Substring(1);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			int o;
			string s = GenerateSentence(out o);

			print(s + " (" + o + ")");
		}
	}
}