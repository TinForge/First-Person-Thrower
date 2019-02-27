using UnityEngine;
using System.Collections;

public class FleeState : IEnemyState
{

	private readonly EnemyState main;

	public FleeState (EnemyState enemyState)
	{
		main = enemyState;
	}

	public void UpdateState ()
	{


	}

	public void OnTriggerEnter (Collider other)
	{


	}

	#region ToState

	public void ToIdleState ()
	{
		main.currentState = main.idleState;
	}

	public void ToSearchState ()
	{
		main.currentState = main.searchState;
	}

	public void ToAttackState ()
	{
		main.currentState = main.attackState;
	}

	public void ToFleeState ()
	{
		return;
	}

	#endregion
}
