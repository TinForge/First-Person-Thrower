using UnityEngine;
using System.Collections;

public class SearchState : IEnemyState
{
	private readonly EnemyState main;

	private const int detectRange = 10;
	private const int sightRange = 25;

	public SearchState (EnemyState enemyState)
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
		if (other.tag == "Player")
			ToAttackState ();
	}

	private void Look ()//heightened line of sight
	{
		if (main.vision.LineOfSight (main.target) && (Vector3.Distance (main.transform.position, main.target.position) < sightRange))
			ToAttackState ();
	}

	private void Patrol ()//vicinity based patrolling
	{
		if (main.move.Stopped (10)) {
			main.move.MoveTo (main.target.position, 10);
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
		return;
	}

	public void ToAttackState ()
	{
		main.detectionCollider.radius = 1;
		main.move.MoveTo (main.target.position, 1);//because the ai is retarded
		main.currentState = main.attackState;
		Debug.Log ("SearchState to AttackState");
	}

	public void ToFleeState ()
	{
		return;
	}


	#endregion


}
