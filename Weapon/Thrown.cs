using UnityEngine;

public class Thrown : MonoBehaviour
{
	[Header ("Components")]
	private Weapon main;
	private WeaponObject wo;

	void Awake ()
	{
		main = GetComponent<Weapon> ();
		wo = main.wo;
	}

	void Start ()
	{
		transform.SetParent (null);
		CalculateTrajectory (transform.position, main.target, wo.forceModifier);
		SelfDestruct ();
	}

	#region INNERWORKS





	private void CalculateTrajectory (Vector3 origin, Vector3 target, float force)
	{
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = (Vector3.Distance (origin, target)) / force;

		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)

		//result = result + Random.insideUnitSphere * 5; //ACCURACY MODIFIER

		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddForce (result, ForceMode.VelocityChange);
		rb.AddRelativeTorque (wo.spin, ForceMode.VelocityChange); //may not spin on server idk
	}

	private void SelfDestruct ()
	{
		Destroy (this);
	}

	#endregion
}
