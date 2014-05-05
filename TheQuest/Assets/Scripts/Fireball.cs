using UnityEngine;
using System.Collections;

public class Fireball : Spell {

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition;
	private Vector3 syncEndPosition;

	public Fireball() {
		damage = 5f;
		speed = 100f;
	}
	// Use this for initialization
	void Start () {
		syncStartPosition = transform.position;
		syncEndPosition = transform.position;

		renderer.material.color = Color.red;

		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(mouseRay, out hit, 1000, 1 << 8);

		if (networkView.isMine)
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered);

		rigidbody.AddForce((hit.point - transform.position).normalized * rigidbody.mass * rigidbody.mass * speed);
	}

	void OnCollisionEnter (Collision col) {
		//Destroy(this.gameObject);
	}

	void Update () {
		if (networkView.isMine)
		{
		}
		else
			SyncedMovement();
	}

	[RPC] void ChangeColorTo ()
	{
		renderer.material.color = Color.red;
		
		if (networkView.isMine)
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, renderer.material.color);
	}

	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncStartPosition = rigidbody.position;
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
		}
	}
}
