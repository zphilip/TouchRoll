using UnityEngine;
using System.Collections;
[AddComponentMenu("Touch/Touchy")]
[RequireComponent (typeof(Rigidbody))]
public class Touchy : MonoBehaviour
{
	public bool useMomentum = true;
	public float addedTouchArea = 1.0f;
	[HideInInspector]
	public bool beingTouched = false;
	
	private float momentumMaximum = 150.0f; // Change private to public if you want to be able to change this value in the editor per object.
	private float slowFilter = 0.05f;
	private float directionRange = 0.6f;
	private bool forceAdded = false;
	private bool draggingBall = false;
	private Vector3 lastPosition;
	private float rollStartTime;
	private Vector3 rollStartPosition;
	private Vector3 rollEndPosition;
	private bool possibleRoll = false;
	private Vector3 rollForce;
	private Vector3 actualPosition;
	private int objectLayer = 12;
	private int touchAreaLayer = 22;
	
	void Start ()
	{
		TouchyManager touchyManager = FindObjectOfType(typeof(TouchyManager)) as TouchyManager;
		if (touchyManager)
		{
			objectLayer = touchyManager.touchyCollisionLayer;
			touchAreaLayer = touchyManager.touchAreaLayer;
		}
		else
		{
			Debug.LogError("No Touchy Manager found in scene. Make sure the Touchy Manager component has been added to one Game Object in your scene.");
		}
		gameObject.layer = objectLayer;
		rigidbody.maxAngularVelocity = 100f;
		GameObject touchArea = new GameObject();
		touchArea.layer = touchAreaLayer;
		touchArea.transform.position = transform.position;
		SphereCollider touchCollider = touchArea.AddComponent<SphereCollider>();
		touchCollider.isTrigger = true;
		touchCollider.radius = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y, collider.bounds.extents.z) + addedTouchArea;
		touchArea.transform.parent = transform;
		touchArea.name = "Touch Area";
	}
	
	public void TouchBegan (RaycastHit hit)
	{
		beingTouched = true;
		forceAdded = false;
		rigidbody.isKinematic = true;
		Vector3 adjustedPosition = hit.point;
		adjustedPosition.y += collider.bounds.extents.y;
		actualPosition = adjustedPosition;
		draggingBall = true;
		lastPosition = hit.point;
	}
	
	public void TouchMoved (RaycastHit hit)
	{
		if (draggingBall)
		{
			Vector3 adjustedPosition = hit.point;
			adjustedPosition.y += collider.bounds.extents.y;
			actualPosition = adjustedPosition;
			Vector3 currentPosition = adjustedPosition;
			Vector3 positionDelta = currentPosition - lastPosition;
			lastPosition = currentPosition;
			if (!possibleRoll && positionDelta.magnitude > slowFilter)
			{
				possibleRoll = true;
				rollStartTime = Time.time;
				rollStartPosition = rigidbody.position;
			}
			if (possibleRoll)
			{
				Vector3 currentRollDelta = currentPosition - rollStartPosition;
				float currentRollDot = Vector3.Dot(currentRollDelta.normalized, positionDelta.normalized);
				if (positionDelta.magnitude < slowFilter || currentRollDot < directionRange)
				{
					possibleRoll = false;
				}
			}
		}
	}
	
	public void TouchEnded (RaycastHit hit)
	{
		beingTouched = false;
		rigidbody.isKinematic = false;
		Vector3 adjustedPosition = hit.point;
		adjustedPosition.y += collider.bounds.extents.y;
		actualPosition = adjustedPosition;
		if (draggingBall && possibleRoll)
		{
			float rollEndTime = Time.time;
			rollEndPosition = hit.point;
			Vector3 rollDelta = rollEndPosition - rollStartPosition;
			rollForce = (rollDelta / (rollEndTime - rollStartTime));
			if (rollForce.magnitude > momentumMaximum)
			{
				rollForce.Normalize();
				rollForce *= momentumMaximum;
			}
			draggingBall = false;
			possibleRoll = false;
			
			if (!forceAdded)
			{
				if (useMomentum)
				{
					rigidbody.AddForce(rollForce, ForceMode.VelocityChange);
				}
				forceAdded = true;
			}
		}
	}
	
	void FixedUpdate ()
	{
		if (beingTouched)
		{
			rigidbody.position = actualPosition;
		}
	}
}
