//using System.Collections;
using UnityEngine;

public class Balancer : ManagedPuppet
{
	[SerializeField] float balanceForce = 400f;
	public Vector3 offCenter { get; private set; }
	public Vector3 footCenter { get; private set; }

	protected override void OnAwake()
	{

	}


	void Update()
	{
		footCenter = (hierarchy.rFoot.position + hierarchy.lFoot.position) / 2;

		Vector3 balanceCenter = footCenter;

		balanceCenter.y = hierarchy.hips.position.y;
		offCenter = balanceCenter - hierarchy.hips.position;

		//if (offCenter.magnitude > 0.2f)
		//{
		//	return;
		//}

		hierarchy.hips.AddForce(Vector3.up * 4 /*+ (dir * 10) */ * balanceForce * Time.deltaTime);

		if (true)//!mover.moving)
		{
			hierarchy.hips.AddForce((offCenter * 10) * balanceForce * Time.deltaTime);
		}
	}
}