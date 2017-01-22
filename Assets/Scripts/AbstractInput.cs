//using System.Collections;
using UnityEngine;

public class AbstractInput : MonoBehaviour
{
	[HideInInspector] public bool forward, backward;
	[HideInInspector] public bool hitDown, hitHold;
	[HideInInspector] public float lookX;
}