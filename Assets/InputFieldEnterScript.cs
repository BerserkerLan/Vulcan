using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldEnterScript : MonoBehaviour {

    TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            
        }

    }
}
