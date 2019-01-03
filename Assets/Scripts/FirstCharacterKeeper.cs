using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCharacterKeeper : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        string tempText = gameObject.GetComponent<TMPro.TMP_InputField>().text;
        if (tempText == "" || tempText[0] != '>')
            {
                gameObject.GetComponent<TMPro.TMP_InputField>().text = ">" + tempText;
                gameObject.GetComponent<TMPro.TMP_InputField>().caretPosition = 2;
            }
        }
	}

