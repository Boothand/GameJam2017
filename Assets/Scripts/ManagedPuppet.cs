//using System.Collections;
using UnityEngine;

public class ManagedPuppet : MonoBehaviour
{
	[HideInInspector] public Hierarchy hierarchy;
	protected Mover mover;
	protected Balancer balancer;
	protected Rotator rotator;

	protected virtual void Awake()
	{
		hierarchy = GetComponent<Hierarchy>();
		mover = GetComponent<Mover>();
		balancer = GetComponent<Balancer>();
		rotator = GetComponent<Rotator>();
		OnAwake();
	}

	protected virtual void OnAwake() { }

	void Update()
	{
		
	}
}