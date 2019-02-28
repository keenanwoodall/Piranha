using UnityEngine;
public class DragRigidbody : MonoBehaviour
{
	public float forceAmount;

	[SerializeField] private float targetDistance;
	[SerializeField] private Vector3 targetPosition;
	[SerializeField] private Vector3 localHitPosition;
	[SerializeField] private Rigidbody target;
	private new Camera camera;

	private void Start ()
	{
		camera = Camera.main; 
	}

	private void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			var ray = camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit))
			{
				target = hit.transform.GetComponent<Rigidbody> ();
				if (target != null)
				{
					targetDistance = (hit.point - ray.origin).magnitude;
					localHitPosition = target.transform.worldToLocalMatrix.MultiplyPoint3x4 (hit.point);
				}
			}
		}
		else if (Input.GetMouseButton (0))
		{
			if (target == null)
				return;

			var ray = camera.ScreenPointToRay (Input.mousePosition);
			targetPosition = ray.origin + ray.direction * targetDistance;
		}
		else if (Input.GetMouseButtonUp (0))
		{
			target = null;
		}
	}

	private void FixedUpdate ()
	{
		if (target == null)
			return;

		var forcePosition = target.transform.localToWorldMatrix.MultiplyPoint3x4 (localHitPosition);
		target.AddForceAtPosition ((targetPosition - forcePosition) * forceAmount * Time.fixedDeltaTime, forcePosition, ForceMode.Impulse);
	}
}