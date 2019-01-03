using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondLevelTutorial : MonoBehaviour {

    public int currentIndexForTutorial;
    public Button nextButton;
    public Text tutorialTextBox;
    public Canvas tutorialCanvas;
    public Button tutorialButtonInfoExample;
    public Button questionIcon;
    public static int showTutorial;
    Vector3 oldtutorialButtonInfoPosition;
    Vector3 oldQuestionIconPosition;

    [TextArea]
    public string[] tutorialInstructions;

    // Use this for initialization
    void Start () {
        currentIndexForTutorial = 0;
        nextButton.onClick.AddListener(loadNextInstruction);
        tutorialCanvas.sortingOrder = 2;
        tutorialCanvas.enabled = true;
        showTutorial = 1;
        oldtutorialButtonInfoPosition = tutorialButtonInfoExample.GetComponent<RectTransform>().localPosition;
        oldQuestionIconPosition = questionIcon.GetComponent<RectTransform>().localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (showTutorial == 1)
        {
            currentIndexForTutorial = 0;
            tutorialTextBox.text = tutorialInstructions[currentIndexForTutorial];
            nextButton.GetComponentInChildren<Text>().text = "Next";
            tutorialButtonInfoExample.GetComponent<RectTransform>().localPosition = oldtutorialButtonInfoPosition;
            questionIcon.GetComponent<RectTransform>().localPosition = oldQuestionIconPosition;
            tutorialCanvas.enabled = true;
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadNextInstruction);
        }
    }
    void loadNextInstruction()
    {
        currentIndexForTutorial = 1;
        showTutorial = 0;

        tutorialTextBox.text = tutorialInstructions[currentIndexForTutorial];

        if (currentIndexForTutorial == 1)
        {
            tutorialButtonInfoExample.GetComponent<RectTransform>().localPosition = new Vector3(0f, 84f, 0f);
            questionIcon.GetComponent<RectTransform>().localPosition = new Vector3(0f, 4f, 0f);
            nextButton.GetComponentInChildren<Text>().text = "Let's Play!";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(closeTutorial);


        }
    }
    void showQuestionIcon()
    {

        tutorialButtonInfoExample.GetComponent<RectTransform>().localPosition = new Vector3(0f, 24f, 0f);


    }

    void closeTutorial()
    {
        tutorialCanvas.enabled = false;
        showTutorial = 0;
    }

}
