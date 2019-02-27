using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState
{
	private readonly EnemyState main;

	public IdleState (EnemyState enemyState)
	{
		main = enemyState;
	}

	public void UpdateState ()
	{
		Look ();
		Patrol ();
	}

	public void OnTriggerEnter (Collider other)//Something hits it
	{
/*		if (other.tag == "Player") {
			main.target = other.transform;
			ToSearchState ();
		}*/
	}

	private void Look ()//shortened line of sight
	{
/*		main.target = main.vision.Target (50, true);
		if (main.target != null) {
			ToSearchState ();
		}*/
	}

	private void Patrol ()//random pathfinding
	{
/*		if (main.move.Stopped (10)) {
			main.move.MoveTo (main.transform.position, 25);
		}*/
	}

	#region ToState

	public void ToIdleState ()
	{
		return;
	}

	public void ToSearchState ()
	{
/*		main.detectionCollider.radius = 75;
		main.destination = main.target.position;
		main.move.MoveTo (main.destination, 10);//because the ai is retarded
		main.currentState = main.searchState;
		Debug.Log ("IdleState to SearchState");*/
	}

	public void ToAttackState ()
	{
		return;
	}

	public void ToFleeState ()
	{
		return;
	}

	#endregion

}
