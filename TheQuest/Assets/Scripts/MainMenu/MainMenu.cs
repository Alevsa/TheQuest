using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    private string playerName = "";
    public string PlayerName { get { return playerName; } }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnGUI()
    {
        playerName = GUI.TextField(new Rect(90, 10, 200, 20), playerName, 25);

        GUI.Label(new Rect(10, 10, 160, 20), "Player Name ");
    }

    void Test()
    {
    }
}
