using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float explosionForce = 10F;
	public float explosionRadius = 2F;


	// Use this for initialization
	void Start () {
		RaycastHit[] hits = Physics.SphereCastAll(transform.position, this.light.range, transform.position); 

		foreach (RaycastHit collider in hits)
		{
			if (collider.collider.networkView.isMine)
				collider.collider.rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
		}

		Destroy (this.gameObject, 1F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
