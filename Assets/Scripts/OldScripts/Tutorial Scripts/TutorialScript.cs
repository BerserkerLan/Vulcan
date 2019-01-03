using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Camera cam;
    public Button nextButton, backButton;
    int currentTutorialIndex;
    public Text tutorialTextBox;

    [TextArea(20,50)]
    public string[] tutorialInstructions;

    //-21 , 23

	// Use this for initialization
	void Start () {
        currentTutorialIndex = 0;
        nextButton.onClick.AddListener(loadNextInstruction);
        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        backButton.gameObject.SetActive(false);
        backButton.onClick.AddListener(loadPreviousInstruction);
        Debug.Log(cam.aspect);

        

        //Current resolution : 1920 x 1080

    }
	
	// Update is called once per frame
	void Update () {
       
	}
    IEnumerator AnimateText(string strComplete)
    {
        tutorialTextBox.text = "";
        foreach (char letter in strComplete.ToCharArray())
        {
            tutorialTextBox.text += letter;
            yield return null;
        }
    }


        void loadNextInstruction()
    {
        currentTutorialIndex++;
        if (currentTutorialIndex >= 1)
        {
            backButton.gameObject.SetActive(true);
        }
        StopAllCoroutines();

        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));

  
        if (currentTutorialIndex == 6)
        {
            nextButton.GetComponentInChildren<Text>().text = "To first level";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadFirstLevel);
        }

        

    }
    void loadPreviousInstruction()
    {
        currentTutorialIndex--;
        if (currentTutorialIndex == 0)
        {
            backButton.gameObject.SetActive(false);
        }

        StopAllCoroutines();

        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
    }

    void loadFirstLevel()
    {
        SceneManager.LoadScene(1);
        
    }
}
