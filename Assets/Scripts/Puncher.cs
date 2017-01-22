using System.Collections;
using UnityEngine;

class ArmChain
{
	public ConfigurableJoint upperArm;
	public ConfigurableJoint lowerArm;
	public ConfigurableJoint hand;

	public ArmChain(ConfigurableJoint newUpperArm, ConfigurableJoint newLowerArm, ConfigurableJoint newHand)
	{
		upperArm = newUpperArm;
		lowerArm = newLowerArm;
		hand = newHand;
	}

	public void SetRotation(ConfigurableJoint joint, Quaternion newRot)
	{
		joint.targetRotation = newRot;
	}
}

public class Puncher : ManagedPuppet
{
	ArmChain rightChain, leftChain;
	[SerializeField] CollisionChecker handCheck;
	bool punching;
	ConfigurableJoint grabJoint;

	protected override void OnAwake()
	{
		base.OnAwake();

		handCheck.OnCollide -= HitSomething;
		handCheck.OnCollide += HitSomething;
	}

	void Start()
	{
		rightChain = new ArmChain(hierarchy.rUpperArm.GetComponent<ConfigurableJoint>(),
								hierarchy.rLowerArm.GetComponent<ConfigurableJoint>(),
								hierarchy.rHand.GetComponent<ConfigurableJoint>());

		leftChain = new ArmChain(hierarchy.lUpperArm.GetComponent<ConfigurableJoint>(),
								hierarchy.lLowerArm.GetComponent<ConfigurableJoint>(),
								hierarchy.lHand.GetComponent<ConfigurableJoint>());
	}

	IEnumerator PunchRoutine(ArmChain chain)
	{
		chain.SetRotation(chain.lowerArm, new Quaternion(2, 0, 0, 1));

		yield return new WaitForSeconds(0.25f);
		punching = true;

		chain.SetRotation(chain.upperArm, new Quaternion(2, 0, 0, 1));

		yield return new WaitForSeconds(0.25f);

		chain.SetRotation(chain.lowerArm, new Quaternion(0, 0, 0, 1));

		yield return new WaitForSeconds(0.5f);
		punching = false;

		while (input.hitHold)
		{
			yield return null;
		}

		chain.SetRotation(chain.lowerArm, new Quaternion(0, 0, 0, 1));
		chain.SetRotation(chain.upperArm, new Quaternion(0, 0, 0, 1));
	}

	void HitSomething(Collision col)
	{
		if (punching)
		{
			print("Ran hit event");
			Rigidbody otherBody = col.transform.GetComponent<Rigidbody>();
			if (otherBody)
			{
				punching = false;
				if (!input.hitHold)
				{
					float force = 100f;
					otherBody.AddForce(-hierarchy.rLowerArm.transform.up * force, ForceMode.Impulse);
				}
				else
				{
					grabJoint = col.gameObject.AddComponent<ConfigurableJoint>();

					grabJoint.connectedBody = hierarchy.rHand;
					grabJoint.xMotion = ConfigurableJointMotion.Locked;
					grabJoint.yMotion = ConfigurableJointMotion.Locked;
					grabJoint.zMotion = ConfigurableJointMotion.Locked;

					JointDrive drive = grabJoint.angularXDrive;
					JointDrive driveYZ = grabJoint.angularYZDrive;
					drive.positionSpring = 80;
					driveYZ.positionSpring = 80;
					grabJoint.angularXDrive = drive;
					grabJoint.angularYZDrive = driveYZ;
				}
			}
		}
	}

	void Update()
	{
		if (input.hitDown)
		{
			StartCoroutine(PunchRoutine(rightChain));
		}

		if (!input.hitHold)
		{
			if (grabJoint)
			{
				Destroy(grabJoint);
			}
		}
	}
}