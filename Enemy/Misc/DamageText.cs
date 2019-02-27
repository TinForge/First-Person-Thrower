using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour
{
	[SerializeField]private TextMesh mesh;
	private Transform player;

	public void Initialize (int value, GameObject target)
	{
		mesh.text = value + "";
		player = target.transform;
		StartCoroutine (Hover ());
	}

	private IEnumerator Hover ()
	{
		//MeshRenderer mr = GetComponent<MeshRenderer> ();
		float t = 0;
		while (t < 5) {
			t += Time.deltaTime;
			transform.position += new Vector3 (0, t / 10, 0);
			transform.LookAt (player);
			//mr.material.color = Color.Lerp (mr.material.color, Color.clear, t / 5);
			yield return null;
		}
		Destroy (gameObject);
	}

}
