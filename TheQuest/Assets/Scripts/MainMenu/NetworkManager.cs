using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "Conjurers";
	private string gameName = "Conjurers";
	public GameObject playerPrefab;
	private HostData[] hostList;

    private GameObject playerClone;
    private Player playerScript;
    public Font Font;
    private string playerName = "Cyka";

    private bool creatingServer, searchingForGame, options = false;
    private Vector2 scrollposition;

	private void StartServer() 
    {
		Network.InitializeServer(4, 420420, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized() 
    {
        Application.LoadLevel("ArenaOne");
		SpawnPlayer();
	}

	void OnGUI() 
    {
        GUI.skin.font = Font;

        GUIStyle fontStyleHeader = new GUIStyle();
        fontStyleHeader.fontSize = 60;
        fontStyleHeader.normal.textColor = Color.white;
        GUI.Label(new Rect(385, 50, 500, 100), "Conjurers", fontStyleHeader);

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 20;
        fontStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(440, 180, 160, 20), "Player Name ", fontStyle);
        playerName = GUI.TextField(new Rect(400, 210, 200, 25), playerName, 14);

		if (!Network.isClient && !Network.isServer && !creatingServer && !options && !searchingForGame)
		{

            if (GUI.Button(new Rect(400, 255, 200, 50), "Join Game"))
            {
                searchingForGame = true;
                RefreshHostList();
            }
            if (GUI.Button(new Rect(400, 330, 200, 50), "Host Game"))
                creatingServer = true;

            if (GUI.Button(new Rect(400, 405, 200, 50), "Options"))
                options = true;

            if (GUI.Button(new Rect(400, 480, 200, 50), "Exit"))
                Exit();
		}

        if (creatingServer)
        {
            GUI.Label(new Rect(440, 245, 160, 20), "Server Name ", fontStyle);
            gameName = GUI.TextField(new Rect(400, 280, 200, 25), gameName, 14);

            if (GUI.Button(new Rect(400, 330, 200, 50), "Create"))
                StartServer();

            if (GUI.Button(new Rect(400, 400, 200, 50), "Back"))
                creatingServer = false;
        }

        if (searchingForGame)
        {
            scrollposition = GUI.BeginScrollView(new Rect(360, 245, 300, 200), scrollposition, new Rect(0, 0, 305, 200));

            if (hostList != null)
            {
                int i;
                for (i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(0, 0 + (25 * i), 305, 25), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }

                GUI.EndScrollView();

                if (GUI.Button(new Rect(400, 470, 200, 50), "Back"))
                    searchingForGame = false;
            }

            else
            {
                GUI.EndScrollView();
                if (GUI.Button(new Rect(400, 470, 200, 50), "Back"))
                    searchingForGame = false;
            }

        }

        if (options)
        {
            if (GUI.Button(new Rect(400, 400, 200, 50), "Back"))
                options = false;
        }
	}

	private void RefreshHostList() 
    {
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) 
    {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

    private void Exit()
    {
        Application.Quit();
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
        playerClone = GameObject.Find("Player(Clone)");
        playerScript = playerClone.GetComponent<Player>();
        playerScript.playerName = this.playerName;
	}

	void Start () 
    {
		Application.runInBackground = true;
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            creatingServer = false;
            searchingForGame = false;
            options = false;
        }
	}
}
