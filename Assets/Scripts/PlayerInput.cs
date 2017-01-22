//using System.Collections;
using UnityEngine;

public class PlayerInput : AbstractInput
{
	
	
	void Awake()
	{
		
	}
	
	void Update()
	{
		forward = Input.GetAxisRaw("Vertical") > 0.001f;
		backward = Input.GetAxisRaw("Vertical") < -0.001f;
		hitDown = Input.GetButtonDown("Fire1");
		hitHold = Input.GetButton("Fire1");
		lookX = Input.GetAxis("Mouse X");
	}
}