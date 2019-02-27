using UnityEngine;

public class EnemyStats
{
	//10
	public int maxHealth = 10;

	//4
	public float speed = 5;

	//1
	public float attackSpeed = 1;

	//0.75f
	public float dmgModifier = 0.75f;

	//1
	public float scaleModifier = 1f;


	public void Initialize (int multiplier, float range)
	{
		maxHealth += multiplier * multiplier * 2;
		Debug.Log (maxHealth);
		speed += multiplier / 4;
		attackSpeed += range;
		scaleModifier += range;
	}


}
