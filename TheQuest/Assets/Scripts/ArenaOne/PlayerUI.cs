using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    void OnGUI()
    {
        if(GUI.Button(new Rect(300, 650, 50, 50), "Q"))
            {

            }

        if (GUI.Button(new Rect(400, 650, 50, 50), "W"))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(500, 650, 50, 50), "E"))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(600, 650, 50, 50), "R"))
            {
                //InputCastSpellOne();
            }

        if (GUI.Button(new Rect(700, 650, 50, 50), "T"))
            {
                //InputCastSpellOne();
            }
    }

	// Use this for initialization
	void Start () 
    {
	

	}
	
	// Update is called once per frame
	void Update () 
    {

	}
}
