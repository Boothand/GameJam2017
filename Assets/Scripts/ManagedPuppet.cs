//using System.Collections;
using UnityEngine;

public class ManagedPuppet : MonoBehaviour
{
	[HideInInspector] public Hierarchy hierarchy;
	protected Mover mover;
	protected Balancer balancer;
<<<<<<< HEAD

=======
	protected Rotator rotator;
>>>>>>> b699199ffc68866a1f0223b3dedd5075ce6e6064

	protected virtual void Awake()
	{
		hierarchy = GetComponent<Hierarchy>();
		mover = GetComponent<Mover>();
		balancer = GetComponent<Balancer>();
<<<<<<< HEAD
=======
		rotator = GetComponent<Rotator>();
>>>>>>> b699199ffc68866a1f0223b3dedd5075ce6e6064
		OnAwake();
	}

	protected virtual void OnAwake() { }

	void Update()
	{
		
	}
}