//using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] Transform actualTarget;
	[SerializeField] Transform actualPivotX;
	[SerializeField] Transform actualPivotY;
	[SerializeField] Transform camPivotPoint;
	[SerializeField] float sensitivity = 5f;
	
	void Awake()
	{
		
	}
	
	void Update()
	{
		actualPivotX.position = camPivotPoint.position;

		transform.position = Vector3.Lerp(transform.position, actualTarget.position, Time.deltaTime * 1f);
		transform.rotation = Quaternion.Lerp(transform.rotation, actualTarget.rotation, Time.deltaTime * 1f);
		actualPivotX.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, Space.World);
		//actualPivotY.Rotate(camPivotPoint.right, -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, Space.World);
	}
}