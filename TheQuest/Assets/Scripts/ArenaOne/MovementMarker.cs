using UnityEngine;
using System.Collections;

public class MovementMarker : MonoBehaviour {

	public float delay = 2F;

	// Use this for initialization
	void Start () {
		GameObject previousMarker = GameObject.Find ("MovementMarker(Clone)");
		if ((previousMarker != null) && (previousMarker != this.gameObject))
			Destroy(previousMarker);

		Destroy(this.gameObject, 2F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
