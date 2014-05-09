using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    private Texture2D spellTextureOne;
    private Texture2D spellTextureTwo;
    private Texture2D spellTextureThree;
    private Texture2D spellTextureFour;
    private Texture2D spellTextureFive;

    private int ButtonSize = 70;
    GUIStyle style = new GUIStyle();

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        playerScript = player.GetComponent<Player>();
        spellTextureOne = playerScript.spellOne.SpellIcon.texture;

        style.normal.textColor = Color.white;
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(300, 650, ButtonSize, ButtonSize), spellTextureOne))
            {
                playerScript.InputCastSpellOne(true);
            }

        if (GUI.Button(new Rect(400, 650, ButtonSize, ButtonSize), spellTextureTwo))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(500, 650, ButtonSize, ButtonSize), spellTextureThree))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(600, 650, ButtonSize, ButtonSize), spellTextureFour))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(700, 650, ButtonSize, ButtonSize), spellTextureFive))
            {
                //InputCastSpellOne();
            }

        GUI.Label(new Rect(347, 693, 50, 50), "Q", style);
        GUI.Label(new Rect(447, 693, 50, 50), "W", style);
        GUI.Label(new Rect(547, 693, 50, 50), "E", style);
        GUI.Label(new Rect(647, 693, 50, 50), "R", style);
        GUI.Label(new Rect(747, 693, 50, 50), "T", style);
    }
	
	// Update is called once per frame
	void Update () 
    {

	}
}
