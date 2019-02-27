using UnityEngine;

[CreateAssetMenu]
public class WeaponObject : ScriptableObject
{
	new public string name;

	[Header ("Physics")]
	public int forceModifier;
	public Vector3 spin;
	[Space]
	public float weight;
	public float drag;
	public float angularDrag;

	[Header ("Effects")]
	public float pitchOffset;
	public float pitchRange;
	public AudioClip startSound;
	public AudioClip impactSound;
	public AudioClip endSound;
	[Space]
	public Transform startParticle;
	public Transform impactParticle;
	public Transform endParticle;

	[Header ("Damage")]	
	//if heal then *-1
	public int damage;
	[Range (0, 0.5f)]public float range;

	[Header ("Components")]
	public bool stickable;
	public bool breakable;
	public bool explodable;
	public int explodeForce;
	public float explodeSize;

}
