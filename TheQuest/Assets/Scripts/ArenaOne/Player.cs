using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    private string playerName;
    private Transform playerNameLocation;


	public float speed = 10f;

	public GameObject movementMarker;
	public GameObject spell;

	private Vector3 movingTowards;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	// Use this for initialization
	void Start () 
    {
        playerNameLocation = transform.Find("playerName");
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (networkView.isMine)
		{
			InputMovement();
			InputColorChange();
			InputCastSpell();
		}
		else
			SyncedMovement();

		if (transform.position.y < -20)
		{
			transform.position = new Vector3 (Random.Range(-3F, 3F), 1F, Random.Range(-3F, 3F)); 
		}
	}

	void InputMovement () {
		if (rigidbody.velocity == Vector3.zero)
		{
			if (Input.GetMouseButtonDown(1))
			{
				Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				Physics.Raycast(mouseRay, out hit, 1000, 1 << 8);

				Instantiate(movementMarker, hit.point + new Vector3(0F, 1F, 0F), Quaternion.LookRotation(new Vector3(0, -1, 0), Vector3.zero));
				movingTowards = hit.point + new Vector3 (0F, transform.position.y, 0F);
			}

			if ((transform.position != movingTowards) && (movingTowards.y != 1F))
				transform.position = Vector3.Lerp(transform.position, movingTowards, Time.deltaTime * speed / Vector3.Distance(transform.position, movingTowards));
//		if (Input.GetKey(KeyCode.W))
//			rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);
//		
//		if (Input.GetKey(KeyCode.S))
//			rigidbody.MovePosition(rigidbody.position - Vector3.forward * speed * Time.deltaTime);
//		
//		if (Input.GetKey(KeyCode.D))
//			rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);
//		
//		if (Input.GetKey(KeyCode.A))
//			rigidbody.MovePosition(rigidbody.position - Vector3.right * speed * Time.deltaTime);
		}
	}

	void FixedUpdate () {
	}

	private void InputColorChange () {
		if (Input.GetKeyDown(KeyCode.R))
		    ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	}

	private void InputCastSpell () {
		if (Input.GetKeyDown (KeyCode.Q))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(mouseRay, out hit, 1000, 1 << 8);

			Vector3 spawnPoint = transform.position + (hit.point - transform.position).normalized * 2;
			spawnPoint.y = transform.position.y;

			Network.Instantiate(spell, spawnPoint, Quaternion.identity, 0);
		}
	}

	[RPC] void CastSpell (Vector3 spawn) {
	}

	[RPC] void ChangeColorTo (Vector3 color)
 	{
			renderer.material.color = new Color(color.x, color.y, color.z, 1f);

			if (networkView.isMine)
				networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
	}

	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnCollisionEnter (Collision col) {
		movingTowards = new Vector3 (0F, 1F, 0F);

		if (col.gameObject.GetComponent<Spell>() != null)
		{
			Spell spellCollider = col.gameObject.GetComponent<Spell>();
		}
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
