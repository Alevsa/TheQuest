using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    public Texture2D buttonHighlighter;

    private Texture2D spellTextureOne;
    private Texture2D spellTextureTwo = null;
    private Texture2D spellTextureThree = null;
    private Texture2D spellTextureFour = null;
    private Texture2D spellTextureFive = null;

    private int ButtonSize = 70;
    private int HighlightSize = 72;
    GUIStyle style = new GUIStyle();
    GUIStyle buttonHighlight = new GUIStyle();

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        playerScript = player.GetComponent<Player>(); 

        spellTextureOne = playerScript.spellOne.SpellIcon.texture;

        style.normal.textColor = Color.white;
        buttonHighlight.normal.background = buttonHighlighter;
    }

    void OnGUI()
    {
        if (playerScript.PrecastingOne)
            GUI.DrawTexture(new Rect(300, 650, HighlightSize, HighlightSize), buttonHighlighter);

        if(GUI.Button(new Rect(300, 650, ButtonSize, ButtonSize), spellTextureOne))
        {
            if (!playerScript.PrecastingOne)
                playerScript.PrecastingOne = true;
            else
                playerScript.PrecastingOne = false;
        }

        if (GUI.Button(new Rect(400, 650, ButtonSize, ButtonSize), spellTextureTwo))
            {
                
            }

        if (GUI.Button(new Rect(500, 650, ButtonSize, ButtonSize), spellTextureThree))
            {
                
            }

        if (GUI.Button(new Rect(600, 650, ButtonSize, ButtonSize), spellTextureFour))
            {
                
            }

        if (GUI.Button(new Rect(700, 650, ButtonSize, ButtonSize), spellTextureFive))
            {
                
            }

        GUI.Label(new Rect(347, 693, 50, 50), "Q", style);
        GUI.Label(new Rect(447, 693, 50, 50), "W", style);
        GUI.Label(new Rect(547, 693, 50, 50), "E", style);
        GUI.Label(new Rect(647, 693, 50, 50), "R", style);
        GUI.Label(new Rect(747, 693, 50, 50), "T", style);
    }

	void Update () 
    {

	}
}
