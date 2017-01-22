using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class SoundTransition : MonoBehaviour {

	public AudioSource[] musicList;

	musicPlayer msP;

	float t;
	// Use this for initialization
	void Start () {

		msP = GameObject.FindWithTag("musicPlayer").GetComponent<musicPlayer>();

		musicList[1].playOnAwake = false;
		musicList[0].playOnAwake = false;

		musicList[1].spatialBlend = 1;
		musicList[0].spatialBlend = 1;

		musicList[0].volume = 0;
		musicList[1].volume = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if(musicList[0].isPlaying == false && musicList[1].isPlaying == false)
		{
			musicList[0].clip = msP.twoSong;
			musicList[0].Play();
			musicList[0].DOKill();
			musicList[0].DOFade(1, 5);

		}
		songSwitch();
	}


	void songSwitch()
	{
		//print(musicList[0].time);
		//print(oneSong.length);
		if (musicList[0].isPlaying == true && musicList[0].time >= msP.oneSong.length - 6 && musicList[1].isPlaying == false)
		{
			print("changing second song");
			musicList[1].clip = msP.twoSong;
			musicList[1].Play();
		}

		if (musicList[1].isPlaying == true && musicList[1].time >= msP.twoSong.length - 6 && musicList[0].isPlaying == false)
			{
			print("changing first song");
			musicList[0].clip = msP.oneSong;
			musicList[0].Play();
		}

		if (musicList[0].isPlaying == true && musicList[0].time >= msP.oneSong.length - 5)
		{
			print("lerp one to two");
			musicList[0].DOKill();
			musicList[1].DOKill();
			musicList[0].DOFade(0, 5);
			musicList[1].DOFade(1, 3);
			if (musicList[0].volume <= 0.00001f)
			{

				musicList[0].Stop();
			}
		}

		if (musicList[1].isPlaying == true && musicList[1].time >= msP.twoSong.length - 5)
		{
			print("lerp two to one");

			musicList[0].DOKill();
			musicList[1].DOKill();
			musicList[0].DOFade(1, 5);
			musicList[1].DOFade(0, 3);

			if (musicList[1].volume <= 0.00001f)
			{
				musicList[1].Stop();
			}
		}
	}
}
