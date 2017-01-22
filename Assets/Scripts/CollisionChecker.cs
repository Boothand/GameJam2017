//using System.Collections;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
	[SerializeField] LayerMask layerMask;
	public delegate void CollisionEvent(Collision col);
	public event CollisionEvent OnCollide;

	void Awake()
	{
		
	}

	private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
	{
		// Convert the object's layer to a bitfield for comparison
		int objLayerMask = (1 << obj.layer);
		if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
			return true;
		else
			return false;
	}

	void OnCollisionEnter(Collision col)
	{

		if (!IsInLayerMask(col.gameObject, layerMask))
		{
			return;
		}

		if (OnCollide != null)
		{
			OnCollide(col);
		}
	}

	void Update()
	{
		
	}
}