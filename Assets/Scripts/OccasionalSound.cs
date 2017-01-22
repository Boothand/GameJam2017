using System.Collections;
using UnityEngine;

public class OccasionalSound : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip clip;
	[SerializeField] float minTime = 10f;
	[SerializeField] float maxTime = 30f;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(PlaySound());
	}

	IEnumerator PlaySound()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
			audioSource.PlayOneShot(clip);
		}
	}
	
	void Update()
	{
		
	}
}