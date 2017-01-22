//using System.Collections;
using UnityEngine;

public class Balancer : ManagedPuppet
{
	[SerializeField] float balanceForce = 400f;
	public Vector3 offCenter { get; private set; }
	public Vector3 footCenter { get; private set; }
	[SerializeField] bool canFall = true;
	[SerializeField] bool canGetUp = true;
	bool fallenOver;
	float fallTime = 2f;
	float fallTimer;

	protected override void OnAwake()
	{

	}


	void Update()
	{
		footCenter = (hierarchy.rFoot.position + hierarchy.lFoot.position) / 2;

		Vector3 balanceCenter = footCenter;

		balanceCenter.y = hierarchy.hips.position.y;
		offCenter = balanceCenter - hierarchy.hips.position;

		float magnitude = offCenter.magnitude;
		float fallThreshold = 0.6f;
		//Falling over
		if (!fallenOver && magnitude > fallThreshold &&
			canFall && fallTimer < 0.001f)
		{
			print(transform.root.name + " fell over");
			fallenOver = true;
		}

		//Getting up after falling down
		if (canGetUp)
		{
			if (fallenOver)
			{
				fallTimer += Time.deltaTime;

				if (fallTimer > fallTime)
				{
					fallenOver = false;
				}
			}
			else
			{
				fallTimer -= Time.deltaTime;
			}
		}

		fallTimer = Mathf.Clamp(fallTimer, 0, fallTime);

		//Keeping balance
		if (!fallenOver)
		{
			hierarchy.hips.AddForce(Vector3.up * 4 /*+ (dir * 10) */ * balanceForce * Time.deltaTime);

			
			hierarchy.hips.AddForce((offCenter * 10) * balanceForce * Time.deltaTime);
		}
	}
}