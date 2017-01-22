//using System.Collections;
using UnityEngine;

public class BottleSounds : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip[] clips;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void OnCollisionEnter()
	{
		AudioClip randomClip = clips[Random.Range(0, clips.Length)];
		audioSource.pitch = 1 + Random.Range(-0.1f, 0.1f);
		audioSource.PlayOneShot(randomClip);
	}

	void Update()
	{

	}
}