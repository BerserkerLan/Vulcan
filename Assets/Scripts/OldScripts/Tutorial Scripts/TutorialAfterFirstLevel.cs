using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialAfterFirstLevel : MonoBehaviour {
    public Button nextButton, backButton;
    int currentTutorialIndex;
    public Text tutorialTextBox;


    [TextArea(20, 50)]
    public string[] tutorialInstructions;

    //-21 , 23

    // Use this for initialization
    void Start()
    {
        currentTutorialIndex = 0;
        nextButton.onClick.AddListener(loadNextInstruction);
        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));

        backButton.gameObject.SetActive(false);


    }

    void Update()
    {

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
        StopAllCoroutines();

        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));

        if (currentTutorialIndex == 3)
        {
            nextButton.GetComponentInChildren<Text>().text = "To Second level";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(loadSecondLevel);
        }
    }
       

    void loadSecondLevel()
    {
        //SceneManager.LoadScene(1);
    }
}
