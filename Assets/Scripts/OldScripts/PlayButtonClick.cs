using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonClick : MonoBehaviour {

    Button playButton;
	// Use this for initialization
	void Start () {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(changeToPlayScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void changeToPlayScene()
    {
        Debug.Log("Mouse Clicked!");
        SceneManager.LoadScene(2);
    }
   
}
