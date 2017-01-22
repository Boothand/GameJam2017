//using System.Collections;
using UnityEngine;

class LegChain
{
	public ConfigurableJoint thigh;
	public ConfigurableJoint leg;
	public ConfigurableJoint foot;


	public LegChain(ConfigurableJoint newThigh, ConfigurableJoint newLeg, ConfigurableJoint newFoot)
	{
		thigh = newThigh;
		leg = newLeg;
		foot = newFoot;
	}

	public void SetRotation(ConfigurableJoint joint, Quaternion newRot)
	{
		joint.targetRotation = newRot;
	}
}

public class Mover : ManagedPuppet
{
	[SerializeField] float moveForce = 400f;
	[SerializeField] float accelerationSpeed = 2f;
	[SerializeField] Transform forwardTarget;
	public bool moving { get; private set; }
	public float moveLerper { get; private set; }

	LegChain rightChain, leftChain;
	LegChain frontChain, backChain;

	protected override void OnAwake()
	{
		base.OnAwake();

		rightChain = new LegChain(hierarchy.rThigh.GetComponent<ConfigurableJoint>(),
									hierarchy.rLeg.GetComponent<ConfigurableJoint>(),
									hierarchy.rFoot.GetComponent<ConfigurableJoint>());

		leftChain = new LegChain(hierarchy.lThigh.GetComponent<ConfigurableJoint>(),
									hierarchy.lLeg.GetComponent<ConfigurableJoint>(),
									hierarchy.lFoot.GetComponent<ConfigurableJoint>());

		frontChain = rightChain;
		backChain = leftChain;
	}

	void TakeInput(ref float targetSpeed)
	{
		//Input: Move forward
		if (Input.GetKey(KeyCode.W))
		{
			targetSpeed = 1f;
		}

		//Input: Move backward
		if (Input.GetKey(KeyCode.S))
		{
			targetSpeed = -1f;
		}
	}

	void Update()
	{
		float targetSpeed = 0f;
		TakeInput(ref targetSpeed);
		moving = Mathf.Abs(targetSpeed) > 0.001f;

		//Gradually tween between -1, 0 and 1 for moving back and forth smoothly
		moveLerper = Mathf.MoveTowards(moveLerper, targetSpeed, Time.deltaTime * accelerationSpeed);

		//Fall forward
		Vector3 forward = forwardTarget.forward;
		transform.forward = Vector3.Lerp(transform.forward, forward, Time.deltaTime * 2f);
		//Vector3 dir = hierarchy.hips.transform.forward + Vector3.up * 1f;
		Vector3 dir = forward + Vector3.up * 1f;


		float moveForceToUse = moveForce;
		float asdSpeed = hierarchy.hips.velocity.magnitude;
		print(asdSpeed);
		if (asdSpeed < 1f)
		{
			//moveForceToUse *= 0.5f;
			hierarchy.hips.AddForce(dir * moveForceToUse * moveLerper * Time.deltaTime);

		}

		//hierarchy.rFoot.AddForce(dir * 0.2f * moveForce * moveLerper * Time.deltaTime);
		//hierarchy.lFoot.AddForce(dir * 0.2f * moveForce * moveLerper * Time.deltaTime);
		//hierarchy.head.AddForce(Vector3.up * 100f * Time.deltaTime);

		float hipVel = hierarchy.hips.velocity.magnitude;
		//print(hipVel);

		if (frontChain.foot.transform.localPosition.z > 0.5f)
		{
			frontChain.SetRotation(frontChain.thigh, new Quaternion(hipVel * 2f, 0, 0, 1));
			frontChain.SetRotation(frontChain.leg, new Quaternion(-hipVel * 1f, 0, 0, 1));
		}
		else if (frontChain.foot.transform.localPosition.z < -0.3f)
		{
			//hierarchy.hips.AddForce(transform.forward * 250f);
			hierarchy.hips.AddForce(Vector3.up * 250f);
			frontChain.SetRotation(frontChain.thigh, new Quaternion(hipVel * 2f, 0, 0, 1));
			frontChain.SetRotation(frontChain.leg, new Quaternion(-hipVel * 1f, 0, 0, 1));
			//frontChain.SetRotation(frontChain.thigh, new Quaternion(-hipVel * 2, 0, 0, 1));
			//frontChain.SetRotation(frontChain.leg, new Quaternion(hipVel * 1.3f, 0, 0, 1));
			//frontChain.leg.GetComponent<Rigidbody>().AddForce(transform.forward * 800f);
		}

		if (Input.GetKey(KeyCode.R))
		{
			backChain.SetRotation(backChain.foot, new Quaternion(-1, 0, 0, 1));
			hierarchy.hips.AddForce(Vector3.up * 30f);
			//backChain.foot.GetComponent<Rigidbody>().AddForce(Vector3.down * 1000f);
		}

		//frontChain.foot.GetComponent<Rigidbody>().AddForce(Vector3.down * 1000f);

		//if (backChain.foot.transform.localPosition.z > 0.3f)
		//{
		//	backChain.SetRotation(frontChain.thigh, new Quaternion(-hipVel * 2, 0, 0, 1));
		//	backChain.SetRotation(frontChain.leg, new Quaternion(hipVel * 1.3f, 0, 0, 1));
		//	print("True");
		//}

		if (backChain.foot.transform.localPosition.z > 0f)
		{
			backChain.foot.GetComponent<Rigidbody>().AddForce(Vector3.down * 700f);
			backChain.SetRotation(backChain.thigh, new Quaternion(0, 0, 0, 1));
			backChain.SetRotation(backChain.leg, new Quaternion(0, 0, 0, 1));

		}

		frontChain.SetRotation(frontChain.foot, new Quaternion(0, 0, 0, 1));
		backChain.SetRotation(backChain.foot, new Quaternion(0, 0, 0, 1));


		//frontChain.SetRotation(frontChain.foot, new Quaternion(0f, 0, 0, 1));

		//backChain.SetRotation(backChain.thigh, new Quaternion(-1, 0, 0, 1));
		//backChain.SetRotation(backChain.leg, new Quaternion(offcenter * 0.5f, 0, 0, 1));
		//backChain.SetRotation(backChain.foot, new Quaternion(0f, 0, 0, 1));
		//frontChain.foot.GetComponent<Rigidbody>().AddForce(hierarchy.hips.transform.forward * Time.deltaTime * offcenter * 1000f);

		if (backChain.foot.transform.localPosition.z < -1.15f)
		{
			print("Switched feet");
			LegChain currentChain = frontChain;
			frontChain = backChain;
			backChain = currentChain;
		}
	}
}