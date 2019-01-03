using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstLevelTutorial : MonoBehaviour {

    public int currentIndexForTutorial;
    public Button nextButton;
    public Button backButton;
    public TextMeshProUGUI tutorialTextBox;
    public GameObject tutorialCanvas;
    public GameObject tutorialPanel;
    public Button tutorialButtonInfoExample;
    public Button questionIcon;
    public GameObject fogOfWarPanel;
    public static int showTutorial;
    Vector3 oldtutorialButtonInfoPosition;
    Vector3 oldQuestionIconPosition;
    public GameObject UITutorialPanel;

    [TextArea]
    public string[] tutorialInstructions;

	// Use this for initialization
	void Start () {
        showTutorial = 1;

    }

    void Update()
    {
        if (showTutorial == 1)
        {
            currentIndexForTutorial = 0;
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadNextInstruction);
            tutorialPanel.gameObject.SetActive(true);
            UITutorialPanel.gameObject.SetActive(false);
            //fogOfWarPanel.SetActive(false);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(loadPreviousInstruction);
            backButton.gameObject.SetActive(false);
            oldtutorialButtonInfoPosition = tutorialButtonInfoExample.GetComponent<RectTransform>().localPosition;
            nextButton.GetComponentInChildren<Text>().text = "Next";
            oldQuestionIconPosition = questionIcon.GetComponent<RectTransform>().localPosition;
            StopAllCoroutines();
            StartCoroutine(AnimateText(tutorialInstructions[currentIndexForTutorial]));
            showTutorial = 0;

        }

    }



    void loadNextInstruction()
    {
        currentIndexForTutorial++;
        showTutorial = 0;
        Debug.Log(currentIndexForTutorial);
        if (currentIndexForTutorial >= 1)
        {
            backButton.gameObject.SetActive(true);
        } 
        else
        {
            backButton.gameObject.SetActive(false);
        }

        StopAllCoroutines();

        StartCoroutine(AnimateText(tutorialInstructions[currentIndexForTutorial]));

        if (currentIndexForTutorial == 6)
        {
            nextButton.GetComponentInChildren<Text>().text = "Let's Play!";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(closeTutorialCanvas);
        }
        else
        {
            nextButton.GetComponentInChildren<Text>().text = "Next";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadNextInstruction);
        }
        if (currentIndexForTutorial == 2)
        {
            //CreateMask();
        }
    }
    void loadPreviousInstruction()
    {
        currentIndexForTutorial--;
        showTutorial = 0;
        Debug.Log(currentIndexForTutorial);

        if (currentIndexForTutorial >= 1)
        {
            backButton.gameObject.SetActive(true);
        }
        else
        {
            backButton.gameObject.SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(AnimateText(tutorialInstructions[currentIndexForTutorial]));
        if (currentIndexForTutorial == 6)
        {
            nextButton.GetComponentInChildren<Text>().text = "Let's Play!";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(closeTutorialCanvas);
        }
        else
        {
            nextButton.GetComponentInChildren<Text>().text = "Next";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadNextInstruction);
        }
       
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






    void closeTutorialCanvas()
    {
        tutorialPanel.gameObject.SetActive(false);
        showTutorial = 0;
        LevelController.showUITutorial = 1;
        UITutorialPanel.gameObject.SetActive(true);
    }
}
