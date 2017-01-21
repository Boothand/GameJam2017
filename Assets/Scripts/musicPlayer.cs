using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour {

	public AudioClip[] songs;

	public AudioClip oneSong;
	public AudioClip twoSong;

	// Use this for initialization
	void Start () {
		oneSong = songs[Random.Range(0, songs.Length - 1)];
		twoSong = songs[Random.Range(0, songs.Length - 1)];

	}
	
	// Update is called once per frame
	void Update () {
		oneSong = songs[Random.Range(0, songs.Length)];
		twoSong = songs[Random.Range(0, songs.Length)];
	}
}
