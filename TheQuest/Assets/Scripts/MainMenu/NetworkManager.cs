using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "Sorcerers";
	private string gameName = "Default";
	public GameObject playerPrefab;
	private HostData[] hostList;

	private void StartServer() {
		Network.InitializeServer(4, 420420, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized() 
    {
        Application.LoadLevel("ArenaOne");
		SpawnPlayer();
	}

	void OnGUI() {
		if (!Network.isClient && !Network.isServer)
		{
            GUI.Label(new Rect(100, 70, 160, 20), "Server Name ");
            gameName = GUI.TextField(new Rect(180, 70, 170, 20), gameName, 25);

            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();

			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}

	private void RefreshHostList() {
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer (HostData hostData)
	{
		Network.Connect(hostData);
	}

	void OnConnectedToServer()
	{
        Application.LoadLevel("ArenaOne");
		SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
