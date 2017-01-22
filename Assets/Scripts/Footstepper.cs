//using System.Collections;
using UnityEngine;

public class Footstepper : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip[] clips;
	
	void Awake()
	{
		
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