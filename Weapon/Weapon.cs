using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour//Set weapon object, Calculate trajectory
{
	[Header ("Components")]
	[HideInInspector]public Effects effects;

	[Header ("Asset")]
	public WeaponObject wo;

	[Header ("Read Only")]
	public Transform owner;
	public Vector3 target;

	[Header ("Custom")]
	[HideInInspector]public bool active = true;

	void Awake ()
	{
		effects = gameObject.AddComponent <Effects> ();
	}

	void Start ()//In hand
	{
		owner = transform.parent.root;
		SetCollision ();
	}

	public void Throw (Vector3 coords)//In air
	{
		target = coords;
		SetRigidbody ();
		effects.PlayStart ();

		gameObject.AddComponent <Thrown> ();
		gameObject.AddComponent<Impact> ();
		if (wo.damage != 0)
			gameObject.AddComponent<Damage> ();
		if (wo.explodable)
			gameObject.AddComponent<Explode> ();
		if (wo.stickable)
			gameObject.AddComponent<Sticky> ();

		Invoke ("Disable", Random.Range (10, 15));
	}

	private void SetCollision ()
	{
		foreach (Collider col in transform.GetComponentsInChildren<Collider> ())
			Physics.IgnoreCollision (col, owner.GetComponentInChildren<Collider> ());
	}

	private void SetRigidbody ()
	{
		Rigidbody rb = gameObject.AddComponent<Rigidbody> ();
		rb.mass = wo.weight;
		rb.interpolation = RigidbodyInterpolation.Interpolate;
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}


	public void Disable ()//Depleted
	{
		foreach (Collider col in GetComponentsInChildren<Collider>())
			col.enabled = false;
		active = false;
		StartCoroutine (Destroy ());
	}

	private IEnumerator Destroy ()
	{
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer> ())
			mr.enabled = false;
		effects.PlayEnd ();
		yield return new WaitForSeconds (effects.EndDuration ());
		Destroy (gameObject);
	}

	void OnDisable ()
	{
		Destroy (gameObject);//when player dies and despawns
	}

	/*	private IEnumerator FadeOut () //Obsolete - cartoon shader doesn't fade
	{
		float i = 255;
		MeshRenderer[] copies = GetComponentsInChildren<MeshRenderer> ();
		while (i > 0) {
			foreach (MeshRenderer mr in copies) {
				Color copy = mr.material.color;
				copy.a = i / 100;
				mr.material.color = copy;
			}
			//Debug.Log (i);
			i -= Time.deltaTime * 500;
			yield return null;
		}
		if (NetworkServer.active)
			NetworkServer.Destroy (gameObject);
	}*/

}
