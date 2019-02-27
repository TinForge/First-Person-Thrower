using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState
{
	private readonly EnemyState main;

	private const int meleeRange = 1;
	private const int attackRange = 10;

	public AttackState (EnemyState enemyState)
	{
		main = enemyState;
	}

	public void UpdateState ()
	{
		Patrol ();
		Look ();
	}

	public void OnTriggerEnter (Collider other)
	{
/*		if (other.tag == "Player") {
			main.target = other.transform;
			main.move.MoveTo (main.target.position, 1);
		}*/
	}

	private void Look ()//Looking at target
	{
/*		if (!main.vision.LineOfSight (main.target) && (Vector3.Distance (main.transform.position, main.target.position) > 20)) {
			ToSearchState ();
		} else {
			main.vision.LookAt (main.target);
		}*/




		if (Vector3.Distance (main.transform.position, main.target.position) > 20) {
			if (!main.vision.LineOfSight (main.target))
				ToSearchState ();
		} else {
			main.vision.LookAt (main.target);
			main.attack.Throw ();
		}
	}

	private void Patrol ()
	{
		if (main.move.Stopped (1)) {
			main.move.MoveTo (main.target.position, 1);
		}
	}

	#region ToState

	public void ToIdleState ()
	{
/*		main.detectionCollider.radius = 25;
		main.target = null;
		main.currentState = main.idleState;
		Debug.Log ("SearchState to IdleState");*/
	}

	public void ToSearchState ()
	{
		main.detectionCollider.radius = 10;
		main.currentState = main.searchState;
		Debug.Log ("AttackState to SearchState");
	}

	public void ToAttackState ()
	{
		return;
	}

	public void ToFleeState ()
	{
		main.currentState = main.fleeState;
		Debug.Log ("SearchState to FleeState");
	}

	#endregion
}
