using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToAbout : MonoBehaviour {

	
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(goToAbout);
    }
	
	void goToAbout()
    {
        SceneManager.LoadScene(5);
    }
}
