using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour {

	Light discoLight;
	public Transform target;

	Vector3 startPosition;
	public Vector3 minPosition;
	public Vector3 maxPosition;
	public Color[] discoColor;
	public float timeWait;

	Transform currentTarget;

	Vector3 newPosition;

	public float smooth = 2f;
	public float tiltAngle = 30.0F;
	float timer;
	Vector3 fromForward;
	Vector3 targetForward;

	// Use this for initialization
	void Start () {

		StartCoroutine(discoWait());
		//currentTarget = target[Random.Range(0, target.Length)];
		targetForward = target.position - transform.position;
		fromForward = transform.forward;
		targetForward.Normalize();
	}

	// Update is called once per frame
	void Update()

	{
		timer += Time.deltaTime * 0.3f;

        transform.forward = Vector3.Lerp(fromForward, targetForward, timer);

		if (timer > 1f)
		{
			PositionRandomizer();
			timer = 0f;
			targetForward = target.position - transform.position;
			targetForward.Normalize();
			fromForward = transform.forward;
		}

		//transform.rotation = Quaternion.Slerp(transform.rotation, newAngle.rotation, Time.deltaTime * smooth);
	}

	IEnumerator discoWait()
	{

		timeWait = Random.Range(0, 3);
		discoLight = this.GetComponent<Light>();

		discoLight.color = discoColor[Random.Range(0, discoColor.Length)];

		yield return new WaitForSeconds(timeWait);
		StartCoroutine(discoWait());

	}

	void PositionRandomizer()
	{
		
		target.position = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
	


	}

}
