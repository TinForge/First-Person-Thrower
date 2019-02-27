using UnityEngine;
using System.Collections;

public class EnemyStun : MonoBehaviour,IStunnable
{
	[HideInInspector]private EnemyState main;

	void Awake ()
	{
		main = GetComponent<EnemyState> ();
	}

	public void Stunned ()
	{
		if (!main.health.alive)
			return;
		main.dumb = true;
		main.nav.enabled = false;
		main.rb.constraints = RigidbodyConstraints.None;
		StartCoroutine (Recover ());
	}

	private IEnumerator Recover ()
	{
		float t = 0;
		float rate = Random.Range (0.75f, 1);
		Quaternion temp = transform.rotation;

		while (main.rb.velocity.magnitude > 0.1f && !main.nav.isOnNavMesh)
			yield return null;
		
		while (t <= 1) {
			t += Time.deltaTime * rate;
			transform.rotation = Quaternion.Lerp (temp, Quaternion.identity, t);
			yield return null;
		}

		if (main.health.alive) {
			transform.rotation = Quaternion.identity;
			main.dumb = false;
			main.nav.enabled = true;
			main.move.Resume ();
			main.rb.constraints = RigidbodyConstraints.FreezeRotationX;
			main.rb.constraints = RigidbodyConstraints.FreezeRotationZ;
			main.currentState = main.searchState;
		}
	}
}
