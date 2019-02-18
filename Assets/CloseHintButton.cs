using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseHintButton : MonoBehaviour {

    public GameObject hintBox;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button>().onClick.AddListener(hideHint);
    }
	
	void hideHint()
    {
        hintBox.SetActive(false);
    }
}
