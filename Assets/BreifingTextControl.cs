using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreifingTextControl : MonoBehaviour {

   public enum TutorialState
    {
        tutorial_1, UItutorial
    }

    int currentTutorialIndex;
    public Button nextButton, backButton;
    public GameObject UIPanel;
    public TutorialState state;
    
    public int levelStartIndex;

    [TextArea(20, 50)]
    public string[] tutorialInstructions;
    // Use this for initialization
    void Start () {
        currentTutorialIndex = 0;
        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        nextButton.onClick.AddListener(loadNextInstruction);
        backButton.onClick.AddListener(loadPreviousInstruction);

    }
	
	// Update is called once per frame
	void Update () {
		
    }

    void loadNextInstruction()
    {
      

        backButton.gameObject.SetActive(true);
        currentTutorialIndex++;
        if (state == TutorialState.UItutorial)
        {
            if (currentTutorialIndex == 1)
            {
                UIPanel.transform.position = new Vector3(UIPanel.transform.position.x, 2.6f, UIPanel.transform.position.z);
            }
        }
        if (currentTutorialIndex == levelStartIndex)
        {
            Debug.Log("Start Level"); 
            if (state == TutorialState.UItutorial)
            {
                UIPanel.SetActive(false);
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        }

        

    }

    void loadPreviousInstruction()
    {
        currentTutorialIndex--;
        if (currentTutorialIndex == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        }
  
       
    }

    IEnumerator AnimateText(string strComplete)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
        foreach (char letter in strComplete.ToCharArray())
        {
            gameObject.GetComponent<TextMeshProUGUI>().text += letter;
            yield return null;
        }
    }
}
