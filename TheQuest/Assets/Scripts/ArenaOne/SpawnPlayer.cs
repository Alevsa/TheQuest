using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () 
    {
        SpawnNewPlayer();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void SpawnNewPlayer()
    {
        Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }
}
