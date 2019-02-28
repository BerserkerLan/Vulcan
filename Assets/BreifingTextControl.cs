using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreifingTextControl : MonoBehaviour {

   public enum TutorialState
    {
        tutorial_1 = 1,tutorial_2 = 2, tutorial_3 = 3, tutorial_4 = 4 ,tutorial_5 = 5, tutorial_6 = 6, tutorial_7 = 7, tutorial_8 = 8, UItutorial = 0
    }

    int currentTutorialIndex;
    
    public Button nextButton, backButton;
    public GameObject UIPanel;
    public TutorialState state;
    public static TutorialState staticTutorialState;
    public static bool changedTutorialState = false;
    public AudioSource keyboardSound;
    public TextMeshProUGUI tutNo;
    public GameObject diagramPanel;
    public GameObject bobPacket;
    public GameObject yourPacket;
    public GameObject colonelImage;
    public GameObject inputArrow;
    public GameObject outputArrow;
    int startIndex;
    bool coroutinePlaying;
    
    public int levelStartIndex;

    [TextArea(20, 50)]
    public string[] tutorialInstructions;
    // Use this for initialization
    void Start () {
       
        currentTutorialIndex = 0;
        levelStartIndex = 9;
        coroutinePlaying = false;
        
        if (changedTutorialState)
        {
            state = staticTutorialState;
           
        }
        Debug.Log(state);

        //Now set the indexes
        if (state == TutorialState.UItutorial)
        {
            currentTutorialIndex = 0;
            levelStartIndex = 4;
        }
        if (state == TutorialState.tutorial_2)
        {
            levelStartIndex = 15;
            currentTutorialIndex = 9;
        }
        if (state == TutorialState.tutorial_3)
        {
            levelStartIndex = 20;
            currentTutorialIndex = 15;
        }
        if (state == TutorialState.tutorial_4)
        {
            levelStartIndex = 25;
            currentTutorialIndex = 20;
        }
        if (state == TutorialState.tutorial_5)
        {
            levelStartIndex = 27;
            currentTutorialIndex = 25;
        }
        if (state == TutorialState.tutorial_6)
        {
            levelStartIndex = 32;
            currentTutorialIndex = 27;
        }
        if (state == TutorialState.tutorial_7)
        {
            levelStartIndex = 39;
            currentTutorialIndex = 32;
        }
        if (state == TutorialState.tutorial_8)
        {
            levelStartIndex = 43;
            currentTutorialIndex = 39;
        }
        if (state != TutorialState.UItutorial)
        {
            //UIPanel.SetActive(false);
            tutNo.text = "1/" + (levelStartIndex - currentTutorialIndex);
        }
        staticTutorialState = state;
        startIndex = currentTutorialIndex;
        StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        nextButton.onClick.AddListener(loadNextInstruction);
        backButton.onClick.AddListener(loadPreviousInstruction);
        backButton.gameObject.SetActive(false);
        Debug.Log(levelStartIndex);
        Debug.Log(currentTutorialIndex);
       

    }
	

    void loadNextInstruction()
    {
        
        if (coroutinePlaying)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = tutorialInstructions[currentTutorialIndex];
            StopAllCoroutines();
            coroutinePlaying = false;
        }
        else
        {

            backButton.gameObject.SetActive(true);
            currentTutorialIndex++;
            if (state != TutorialState.UItutorial)
            {
                if (currentTutorialIndex == 5)
                {
                    showInputPacket();
                }
                else if (currentTutorialIndex == 6)
                {
                    showOutputPacket();
                }
                else
                {
                    hideDiagram();
                }
            }
            if (state != TutorialState.UItutorial)
            {
                tutNo.text = ((currentTutorialIndex + 1) - startIndex) + tutNo.text.Substring(1, tutNo.text.Length - 1);
            }
            Debug.Log(currentTutorialIndex);
            Debug.Log(levelStartIndex);

            if (state == TutorialState.UItutorial)
            {
                if (currentTutorialIndex == 1)
                {
                    UIPanel.transform.position = new Vector3(UIPanel.transform.position.x, 2.6f, UIPanel.transform.position.z);
                }
            }
            if (currentTutorialIndex == levelStartIndex)
            {
                Debug.Log("Equal Indexes");
                if (state == TutorialState.UItutorial)
                {
                    UIPanel.SetActive(false);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {

                StopAllCoroutines();
                StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
            }
        }

        

    }

    void showInputPacket()
    {
        diagramPanel.SetActive(true);
        colonelImage.SetActive(false);
        yourPacket.SetActive(false);
        bobPacket.SetActive(true);
        inputArrow.SetActive(true);
        outputArrow.SetActive(false);
        
    }

    void showOutputPacket()
    {
        diagramPanel.SetActive(true);
        colonelImage.SetActive(false);
        yourPacket.SetActive(true);
        bobPacket.SetActive(false);
        inputArrow.SetActive(false);
        outputArrow.SetActive(true);

        yourPacket.GetComponent<Animation>().Play();
    }

    void hideDiagram()
    {
        diagramPanel.SetActive(false);
        colonelImage.SetActive(true);
    }

    void loadPreviousInstruction()
    {
        currentTutorialIndex--;

        if (state != TutorialState.UItutorial)
        {
            if (currentTutorialIndex == 5)
            {
                showInputPacket();
            }
            else if (currentTutorialIndex == 6)
            {
                showOutputPacket();
            }
            else
            {
                hideDiagram();
            }
        }
        Debug.Log(currentTutorialIndex);
        if (state != TutorialState.UItutorial)
        {
            tutNo.text = ((currentTutorialIndex + 1) - startIndex) + tutNo.text.Substring(1, tutNo.text.Length - 1);
        }
        if (currentTutorialIndex == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
        
        
            StopAllCoroutines();
            StartCoroutine(AnimateText(tutorialInstructions[currentTutorialIndex]));
        
  
       
    }

    IEnumerator AnimateText(string strComplete)
    {
        coroutinePlaying = true;
        gameObject.GetComponent<TextMeshProUGUI>().text = "";

        foreach (char letter in strComplete.ToCharArray())
        {
            gameObject.GetComponent<TextMeshProUGUI>().text += letter;
            yield return null;
         
        }
        coroutinePlaying = false;
    }
}
