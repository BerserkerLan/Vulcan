using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinner : MonoBehaviour {

    List<string> inputTableRules = new List<string>();
    List<string> outputTableRules = new List<string>();
    string inputDefaultRule = "Default : ACCEPT";
    string outputDefaultRule = "Default : ACCEPT";
    bool hintShowing = false;
    bool breifShowing = true;

    public int levelNumber;
    public TMP_FontAsset textMeshFont;
    public Button playButton;
    public Button runTerminalCommandButton;
    public Button inputTableButton, outputTableButton;
    public Button resetButton;
    public TMP_InputField terminalInput;
    public TMP_Text inputDefault, outputDefault;
    public GridLayoutGroup inputTableLayout, outputTableLayout;
    public VerticalLayoutGroup inputNumberLayout, outputNumberLayout;
    public GameObject outputScrollview, inputScrollview;
    public GameObject outputNumberview, inputNumberview;
    Color selectedBlueColor, UnselectedBlueColor;
    public Packet bobPacket, alicePacket;
    public Button hintButton;
    public GameObject hintBox;
    public GameObject hintTextBox;
    public Button breifingButton;
    public GameObject breifingBox;
    public GameObject breifingTextBox;
    public GameObject UITutorial;
    public GameObject[] networkDiagrams;
    public GameObject losingPanel;
    public GameObject winningPanel;
    [TextArea(20,30)]
    public string[] breifingText;
    [TextArea(20, 30)]
    public string[] hintText;

    public GameObject bobPacketLevel2;
    public GameObject charliePacketLevel2;
    public GameObject bobPacketLevel3;
    public GameObject charliePacketLevel3;
    public GameObject alicePacketLevel3;
    public GameObject bobPacketLevel4;
    public GameObject charliePacketLevel4;
    public GameObject alicePacketLevel4;
    public GameObject bobPacketLevel5;
    public GameObject charliePacketLevel5;
    public GameObject alicePacketLevel5;
    public GameObject bobPacketLevel6;
    public GameObject charliePacketLevel6;
    public GameObject alicePacketLevel6;
    public GameObject bobPacketLevel7;
    public GameObject charliePacketLevel7;
    public GameObject alicePacketLevel7;
    public GameObject bobPacketLevel8;
    public GameObject alicePacketLevel8;

    int inputIndex, outputIndex;


    // Use this for initialization
    void Start () {
        selectedBlueColor = new Color();
        UnselectedBlueColor = new Color();
        ColorUtility.TryParseHtmlString("#2F49AD", out selectedBlueColor);
        ColorUtility.TryParseHtmlString("#3F63EC", out UnselectedBlueColor);
        playButton.onClick.AddListener(handleLevel);
        runTerminalCommandButton.onClick.AddListener(runCommand);
        inputIndex = 0;
        outputIndex = 0;
        inputTableButton.onClick.AddListener(showInputTable);
        outputTableButton.onClick.AddListener(showOutputTable);
        hintButton.onClick.AddListener(showOrHideHint);
        resetButton.onClick.AddListener(resetTableAndRules);
        if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_2)
        {
            levelNumber = 2;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_3)
        {
            levelNumber = 3;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_4)
        {
            levelNumber = 4;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_5)
        {
            levelNumber = 5;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_6)
        {
            levelNumber = 6;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_7)
        {
            levelNumber = 7;
        }
        else if (BreifingTextControl.staticTutorialState == BreifingTextControl.TutorialState.tutorial_8)
        {
            levelNumber = 8;
        }


        if (levelNumber == 1)
        {
            showUITutorial();
        }
        else
        {
            hideUITutorial();
        }

        Debug.Log("Level Number" + levelNumber);


      

        if (levelNumber == 8)
        {
            addOutputRule("DROP 122.15.43.22 ANY ANY ANY tcp");
            addOutputRule("DROP 122.15.43.22 ANY ANY ssh tcp");
            addInputRule("DROP 192.168.1.33 ANY ANY http tcp");
        }
        networkDiagrams[levelNumber - 1].SetActive(true);
        hintTextBox.GetComponentInChildren<TextMeshProUGUI>().text = hintText[levelNumber - 1];
        breifingTextBox.GetComponentInChildren<TextMeshProUGUI>().text = breifingText[levelNumber - 1];

        if (levelNumber == 1)
        {
            breifingBox.SetActive(false);
        }
        hintBox.SetActive(false);

    }

    void deleteIndexByLineNumberInput(int line)
    {
            if (inputIndex == 0)
            {
                return;
            }
            if (inputIndex == 1)
            {
                Debug.Log("Deleting Input");
                foreach (Transform childr in inputNumberLayout.transform)
                {
                if (childr.gameObject.name != "#")
                {
                    Destroy(childr.gameObject);
                }
                }
                inputIndex--;
            }
            else
            {
                inputIndex--;
                Destroy(inputNumberLayout.GetComponent<Transform>().GetChild((line)).gameObject);
                Debug.Log("Deleting Input again");
                foreach (Transform childr in inputNumberLayout.transform)
                {
                  if (childr.gameObject.name != "#")
                    {

                        if (int.Parse(childr.gameObject.name) > line)
                        {
                            childr.gameObject.name = (int.Parse(childr.gameObject.name) - 1) + "";
                        }
                    }
                }
            }

      

    }

    void deleteIndexByLineNumberOutput(int line)
    {

        Destroy(outputNumberLayout.GetComponent<Transform>().GetChild(line).gameObject);
        if (outputIndex == 0)
            {
                return;
            }
        if (outputIndex == 1)
            {
                foreach (Transform childr in outputNumberLayout.transform)
                {
                    if (childr.gameObject.name != "#")
                    {
                    Debug.Log("Destroying object : " + childr.gameObject.name);
                        Destroy(childr.gameObject);
                    }
                }
                outputIndex--;
             }
        else
            {
                outputIndex--;
                Destroy(outputNumberLayout.GetComponent<Transform>().GetChild((line)).gameObject);
                foreach (Transform childr in outputNumberLayout.transform)
                {
                if (childr.gameObject.name != "#")
                {
                    Debug.Log("current Child is :" + childr.gameObject.GetComponent<TextMeshProUGUI>().text);
                    try {
                        if (Convert.ToInt32(childr.gameObject.name) > line)
                        {
                            childr.gameObject.name = (Convert.ToInt32(childr.gameObject.name) - 1) + "";
                            childr.gameObject.GetComponent<TextMeshProUGUI>().text = (Convert.ToInt32(childr.gameObject.GetComponent<TextMeshProUGUI>().text) - 1) + "";
                        }
                    } catch
                    {
                        Debug.Log("Caught in parsing the int in the number layout"); //So this got caught
                    }
                    }
                
                }
            }


    }

    void resetTableAndRules()
    {
        if (levelNumber == 8)
        {
            return;
        }
        inputTableRules.Clear();
        outputTableRules.Clear();
        Transform inputTransform = inputTableLayout.GetComponent<Transform>();
        Transform outputTransform = outputTableLayout.GetComponent<Transform>();
        foreach (Transform childr in inputTransform)
        {
            Destroy(childr.gameObject);
        }
        foreach (Transform childr in outputTransform)
        {
            Destroy(childr.gameObject);
        }
        foreach (Transform childr in inputNumberLayout.GetComponent<Transform>())
        {
            Destroy(childr.gameObject);
        }
        foreach (Transform childr in outputNumberLayout.GetComponent<Transform>())
        {
            Destroy(childr.gameObject);
        }

        
        //Reset the labels on top of table for first, OUTPUT
        showOutputTable();

        GameObject child = null;
        child = new GameObject("#");
        RectTransform childTransformation = child.AddComponent<RectTransform>();
        childTransformation.SetParent(outputNumberLayout.transform);
        childTransformation.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        TextMeshProUGUI textMesh = null;
        textMesh = child.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
        Color textCol2 = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol2);
        textMesh.color = textCol2;
        textMesh.text = "#";

      

        GameObject childRule = null;
        childRule = new GameObject("Access Rule");
        RectTransform childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
        Color textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00",out textCol);
        textMesh.color = textCol;
        textMesh.text = "Access Rule";

         childRule = null;
        childRule = new GameObject("Source IP");
         childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.text = "Source IP";

         childRule = null;
        childRule = new GameObject("Source Port");
         childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
         textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.text = "Source Port";

         childRule = null;
        childRule = new GameObject("Destination IP");
         childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
         textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Destination IP";

        childRule = null;
        childRule = new GameObject("Destination Port");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Destination Port";

         childRule = null;
        childRule = new GameObject("Protocol");
         childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(outputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
         textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Protocol";

        //Now INPUT
        showInputTable();

        child = null;
        child = new GameObject("#");
        childTransformation = child.AddComponent<RectTransform>();
        childTransformation.SetParent(inputNumberLayout.transform);
        childTransformation.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = child.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
        textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.text = "#";

        childRule = null;
        childRule = new GameObject("Access Rule");
         childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Access Rule";

        childRule = null;
        childRule = new GameObject("Source IP");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Source IP";

        childRule = null;
        childRule = new GameObject("Source Port");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Source Port";

        childRule = null;
        childRule = new GameObject("Destination IP");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.text = "Destination IP";

        childRule = null;
        childRule = new GameObject("Destination Port");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
        textMesh.fontSize = 20.52f;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.text = "Destination Port";

        childRule = null;
        childRule = new GameObject("Protocol");
        childTrans = childRule.AddComponent<RectTransform>();
        childTrans.SetParent(inputTableLayout.transform);
        childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
        textMesh = null;
        textMesh = childRule.AddComponent<TextMeshProUGUI>();
        textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        Debug.Log(textMesh.alignment);
        textMesh.font = textMeshFont;
         textCol = new Color();
        ColorUtility.TryParseHtmlString("#D9FF00", out textCol);
        textMesh.color = textCol;
        textMesh.fontSize = 20.52f;
        textMesh.text = "Protocol";

        inputIndex = 0;
        outputIndex = 0;
       

    }

    void showUITutorial()
    {
        UITutorial.SetActive(true);
    }
    void hideUITutorial()
    {
        UITutorial.SetActive(false);
    }
    void ShowLosingPanel()
    {
        
        StartCoroutine(panelTemporaryEnum());

    }
    void ShowWinningPanel()
    {
        StartCoroutine(winningPanelTemporaryEnum());
    }
        
    IEnumerator panelTemporaryEnum()
    {
        losingPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        losingPanel.SetActive(false);

    }

    IEnumerator winningPanelTemporaryEnum()
    {
        winningPanel.SetActive(true);
        yield return new WaitForSeconds(8);
        winningPanel.SetActive(false);
    }

    IEnumerator loadLevel5Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_5;
        levelNumber = 5;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadLevel3Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        levelNumber = 3;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_3;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadLevel4Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        levelNumber = 4;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_4;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadLevel2Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_2;
        levelNumber = 2;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadLevel6Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        levelNumber = 6;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_6;
        SceneManager.LoadScene(1);
    }
    IEnumerator loadLevel7Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        levelNumber = 7;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_7;
        SceneManager.LoadScene(1);
    }
    IEnumerator loadLevel8Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        levelNumber = 8;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_8;
        SceneManager.LoadScene(1);
    }
    IEnumerator loadWinningScene()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        SceneManager.LoadScene(3);
    }

    void showOrHideHint()
    {
        if (hintShowing)
        {
            hintBox.SetActive(false);
            hintShowing = false;
        }
        else
        {
            hintBox.SetActive(true);
            hintShowing = true;
        }
    }

    void showOrHideBreifingPanel()
    {
        if (breifShowing)
        {
            breifingBox.SetActive(false);
            breifShowing = false;
        }
        else
        {
            breifingBox.SetActive(true);
            breifShowing = true;
        }
    }

    //Bob: 192.168.1.33
    //Alice: 122.15.43.22
    //CHarlie: 192.11.76.5
    void handleLevel()
    {
        if (levelNumber == 1)
        {
            //Need default drop on all traffic : iptables --policy INPUT ACCEPT and iptables --policy OUTPUT DROP
            string winningDefault = "Default : DROP";
            if (inputDefault.text == winningDefault && outputDefault.text == winningDefault)
            {
                Debug.Log("You won!");
                bobPacket.gameObject.SetActive(true);
                alicePacket.gameObject.SetActive(true);
                bobPacket.playMovement();
                alicePacket.playMovement();
                ShowWinningPanel();
                StartCoroutine(loadLevel2Tutorial());

            }
            else
            {
                Debug.Log("You lost :(, Try Again");
                ShowLosingPanel();
            }
          
        }
        if (levelNumber == 2)
        {
            //Need Alice to allow communication from BOB only, and not charlie, so iptables -A INPUT -s 192.11.76.5 -j DROP is all thats needed
            //In the array, it'll be listed as "DROP 192.11.76.5 ANY ANY ANY" in the INPUT table 
            string winningRule = "DROP 192.11.76.5 ANY ANY ANY ANY";
            if (inputTableRules.Contains(winningRule))
            {
                Debug.Log("You win boi!");
                bobPacketLevel2.SetActive(true);
                charliePacketLevel2.SetActive(true);
                charliePacketLevel2.GetComponent<Animation>().Play();
                bobPacketLevel2.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel3Tutorial());
                

            }
            else
            {
                Debug.Log("You lose :(");
                ShowLosingPanel();
            }



        }
        if (levelNumber == 3)
        {
            //Need to ONLY ALLOW TCP traffic to and from Alice's computer
            //Can be done in multiple ways
            //Set default to DROP in both, then iptables -A INPUT -s <IP> -p tcp -j ACCEPT for both IP addresses, and also for the OUTPUT table
            //Or you could have a acceptance of TCP for both IP on both tables, and the last rule can be a drop on all

            //Way 1:
            //ACCEPT 122.15.43.22 ANY ANY tcp in OUTPUT,
            String winningRule3 = "ACCEPT 122.15.43.22 ANY ANY ANY tcp"; //Should be in OUTPUT
            String winningRule1 = "ACCEPT 192.11.76.5 ANY ANY ANY tcp";
            String winningRule2 = "ACCEPT 192.168.1.33 ANY ANY ANY tcp"; //Both rules just needed in INPUT table
            String winningRule12 = "ACCEPT ANY ANY ANY ANY tcp"; //This can also be in the INPUT table instead of the above rule
            String dropOnAllRule = "DROP ANY ANY ANY ANY ANY";
            //This one also needs default policy to be DROP
            foreach (string rule in inputTableRules)
            {
                Debug.Log(rule);
            }
            foreach (string rule in outputTableRules)
            {
                Debug.Log("- " + rule);
            }
            Debug.Log(inputTableRules.Contains(winningRule1));
            Debug.Log(outputTableRules.Contains(winningRule2));
            Debug.Log(outputTableRules.Contains(winningRule1));
            Debug.Log(inputTableRules.Contains(winningRule2));


            if ((inputTableRules.Contains(winningRule1) && inputTableRules.Contains(winningRule2) || inputTableRules.Contains(winningRule12)) && outputTableRules.Contains(winningRule3) && inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP")
            {
                Debug.Log("You won!");
                alicePacketLevel3.SetActive(true);
                bobPacketLevel3.SetActive(true);
                charliePacketLevel3.SetActive(true);
                alicePacketLevel3.GetComponent<Animation>().Play();
                charliePacketLevel3.GetComponent<Animation>().Play();
                bobPacketLevel3.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel4Tutorial());
            }
            //Way 2:
            else if (inputTableRules.Contains(winningRule1) && outputTableRules.Contains(winningRule2) && outputTableRules.Contains(winningRule1) && inputTableRules.Contains(winningRule2) && inputTableRules.Contains(dropOnAllRule) && outputTableRules.Contains(dropOnAllRule))
            {
                Debug.Log("You won!");
                alicePacketLevel3.SetActive(true);
                bobPacketLevel3.SetActive(true);
                charliePacketLevel3.SetActive(true);
                alicePacketLevel3.GetComponent<Animation>().Play();
                charliePacketLevel3.GetComponent<Animation>().Play();
                bobPacketLevel3.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel4Tutorial());
            }
            else
            {
                Debug.Log("You Lose");
                ShowLosingPanel();
            }


        }
        if (levelNumber == 4)
        {
            //To win, have to block ALL HTTP TRAFFIC INTO AND OUT OF THE FIREWALL
            // Way 1 : They simple do this rule on both the INPUT and OUTPUT table "DROP ANY ANY ANY http tcp" i.e iptables -A <tableName> --dport http -p tcp -j DROP
            // Way 2 : As this level doesn't specify allowing any type of traffic, it can simply DROP by default with nothing different, or have the DROP ALL rule in both tables
            //Done some other ways too since players kept doing other ways

            string winningRule1 = "DROP ANY ANY ANY http tcp";
            string outputRule1 = "DROP 122.15.43.22 ANY ANY http tcp";


            //When IP Addresses are specified
            string winningRule2 = "DROP 192.11.76.5 ANY ANY http tcp";
            string winningRule3 = "DROP 192.168.1.33 ANY ANY http tcp";
            String dropOnAllRule = "DROP ANY ANY ANY ANY ANY";
            
            if (inputTableRules.Contains(winningRule1) && outputTableRules.Contains(winningRule1))
            {
                Debug.Log("You won!");
                alicePacketLevel4.SetActive(true);
                bobPacketLevel4.SetActive(true);
                charliePacketLevel4.SetActive(true);
                alicePacketLevel4.GetComponent<Animation>().Play();
                bobPacketLevel4.GetComponent<Animation>().Play();
                charliePacketLevel4.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel5Tutorial());
            }
            else if (inputTableRules.Contains(winningRule1) && outputTableRules.Contains(outputRule1))
            {
                Debug.Log("You won!");
                alicePacketLevel4.SetActive(true);
                bobPacketLevel4.SetActive(true);
                charliePacketLevel4.SetActive(true);
                alicePacketLevel4.GetComponent<Animation>().Play();
                bobPacketLevel4.GetComponent<Animation>().Play();
                charliePacketLevel4.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel5Tutorial());
            }
            else if (inputTableRules.Contains(winningRule2) && inputTableRules.Contains(winningRule3) && (outputTableRules.Contains(winningRule1) || outputTableRules.Contains(outputRule1))) 
            {
                Debug.Log("You won!");
                alicePacketLevel4.SetActive(true);
                bobPacketLevel4.SetActive(true);
                charliePacketLevel4.SetActive(true);
                alicePacketLevel4.GetComponent<Animation>().Play();
                bobPacketLevel4.GetComponent<Animation>().Play();
                charliePacketLevel4.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel5Tutorial());
            }
            else if ((inputTableRules.Contains(dropOnAllRule) && outputTableRules.Contains(dropOnAllRule)) || (inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP"))
            {
                Debug.Log("You won!");
                alicePacketLevel4.SetActive(true);
                bobPacketLevel4.SetActive(true);
                charliePacketLevel4.SetActive(true);
                alicePacketLevel4.GetComponent<Animation>().Play();
                bobPacketLevel4.GetComponent<Animation>().Play();
                charliePacketLevel4.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel5Tutorial());
            }
            else
            {
                ShowLosingPanel();
            }

         

        }
        if (levelNumber == 5)
        {
            //To win, have to Allow charlie to SSH into Alice, and DROP everything else
            //Two ways: set default policy to DROP, and Allow SSH from Charlie to Alice, or Allow SSH and then add a rule to drop it
            //Way 1 : Allow SSH in both tables from Charlie to Alice and vice versa, and then default drop "ACCEPT 192.11.76.5 ssh ANY ssh tcp" || "ACCEPT 192.11.76.5 ssh ANY ssh tcp"

            bool cond1 = (outputTableRules.Contains("ACCEPT 122.15.43.22 ANY ANY ssh tcp") || outputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY 22 tcp"));
            bool cond2 = (inputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY ssh tcp") || inputTableRules.Contains("ACCEPT 122.15.43.22 ANY ANY 22 tcp"));
            Debug.Log(cond1);
            Debug.Log(cond2);
            Debug.Log(inputTableRules);
            Debug.Log(outputTableRules);

            if (cond1 && cond2 && inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP")
            {
                Debug.Log("You won!");
                alicePacketLevel5.SetActive(true);
                bobPacketLevel5.SetActive(true);
                charliePacketLevel5.SetActive(true);
                alicePacketLevel5.GetComponent<Animation>().Play();
                bobPacketLevel5.GetComponent<Animation>().Play();
                charliePacketLevel5.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel6Tutorial());

            }
            else if (cond1 && cond2 && inputTableRules.Contains("DROP ANY ANY ANY ANY ANY") && outputTableRules.Contains("DROP ANY ANY ANY ANY ANY"))
            {
                Debug.Log("You won!");
                alicePacketLevel5.SetActive(true);
                bobPacketLevel5.SetActive(true);
                charliePacketLevel5.SetActive(true);
                alicePacketLevel5.GetComponent<Animation>().Play();
                bobPacketLevel5.GetComponent<Animation>().Play();
                charliePacketLevel5.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel6Tutorial());
            }
            else
            {
                ShowLosingPanel();
            }

        }
        if (levelNumber == 6)
        {
            //To win, Block HTTP traffic from Alice to Bob and vice versa, and Allow Charlie to send UDP packets to Alice only, no need to change anything else
            //Rules needed: "DROP 192.168.1.33 80 ANY 80 tcp" in INPUT, "DROP 122.15.43.22 80 ANY 80 tcp" in OUTPUT, and "ACCEPT 192.11.76.5 ANY ANY ANY udp" in INPUT

            bool cond1 = inputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY ANY udp") && inputTableRules.Contains("DROP 192.168.1.33 ANY ANY http tcp");
            bool cond2 = outputTableRules.Contains("DROP 122.15.43.22 ANY ANY http tcp");

            if (cond1 && cond2)
            {
                Debug.Log("You won!");
                alicePacketLevel6.SetActive(true);
                bobPacketLevel6.SetActive(true);
                charliePacketLevel6.SetActive(true);
                alicePacketLevel6.GetComponent<Animation>().Play();
                bobPacketLevel6.GetComponent<Animation>().Play();
                charliePacketLevel6.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel7Tutorial());
            }
            else
            {
                ShowLosingPanel();
            }

        }
        if (levelNumber == 7)
        {
            //NEED TO ADD ORDERING MATTER IN THIS
            //For this level, you need to do three things:

            //1) Don't allow Bob to SSH into Alice's computer (Remember the two way rule)
            //Need rule "ACCEPT 192.168.1.33 ssh ANY ssh tcp" in INPUT, "ACCEPT 122.15.43.22 ssh ANY ssh tcp" in OUTPUT

            //2) Allow TCP traffic coming from Charlies computer through the firewall from the outside, and TCP traffic going from Alice computer through the firewall from the inside.
            //Need rule "ACCEPT 122.15.43.22 ANY ANY ANY tcp" in OUTPUT, "ACCEPT 192.11.76.5 ANY ANY ANY tcp" in INPUT

            bool firstRuleFound = false;
            bool orderingCorrect = false;

            foreach (String rule in outputTableRules)
            {
                if (rule == "DROP 122.15.43.22 ANY ANY ssh tcp" || rule == "DROP 122.15.43.22 ANY ANY 22 tcp")
                {
                    firstRuleFound = true;
                }
                if ( rule == "ACCEPT 122.15.43.22 ANY ANY ANY tcp" && firstRuleFound)
                {
                    orderingCorrect = true;
                }
            }

            Debug.Log("orderingCorrent : " + orderingCorrect);

            bool cond1 = outputTableRules.Contains("ACCEPT 122.15.43.22 ANY ANY ANY tcp") && (outputTableRules.Contains("DROP 122.15.43.22 ANY ANY ssh tcp") || outputTableRules.Contains("DROP 122.15.43.22 ANY ANY 22 tcp"));
            bool cond2 = (inputTableRules.Contains("DROP 192.168.1.33 ANY ANY ssh tcp") || inputTableRules.Contains("DROP 192.168.1.33 ANY ANY 22 tcp")) && inputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY ANY tcp");

            Debug.Log("cond1 : " + cond1);
            Debug.Log("cond2 :" + cond2);
            if (orderingCorrect && cond1 && cond2 && inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP")
            {
                Debug.Log("You won!");
                alicePacketLevel7.SetActive(true);
                bobPacketLevel7.SetActive(true);
                charliePacketLevel7.SetActive(true);
                alicePacketLevel7.GetComponent<Animation>().Play();
                bobPacketLevel7.GetComponent<Animation>().Play();
                charliePacketLevel7.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadLevel8Tutorial());
            }
            else
            {
                ShowLosingPanel();
            }




        }
        if (levelNumber == 8)
        {
            //Just need to delete the rule "DROP 122.15.43.22 ANY ANY ANY tcp" from output table

        if (!outputTableRules.Contains("DROP 122.15.43.22 ANY ANY ANY tcp"))
            {
                Debug.Log("You won!");
                alicePacketLevel7.SetActive(true);
                bobPacketLevel7.SetActive(true);
                charliePacketLevel7.SetActive(true);
                alicePacketLevel7.GetComponent<Animation>().Play();
                bobPacketLevel7.GetComponent<Animation>().Play();
                charliePacketLevel7.GetComponent<Animation>().Play();
                ShowWinningPanel();
                StartCoroutine(loadWinningScene());
            }
        else
            {
                ShowLosingPanel();
            }

        }
    }

    void StartTutorial2()
    {
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_2;
        SceneManager.LoadScene(1);
    }

    void runCommand()
    {
        string[] commandsParse = terminalInput.text.Split(' ');
        if (commandsParse[1] == "--policy")
        {
            string table = commandsParse[2];
            if (table != "INPUT" && table != "OUTPUT")
            {
                Debug.Log("Invalid Table");
            }
            else
            {
                string policy = commandsParse[3];
                if (policy != "ACCEPT" && policy != "DROP")
                {
                    Debug.Log("Invalid Policy");
                }
                else
                {

                    if (table == "INPUT")
                    {
                        inputDefaultRule = policy;
                        inputDefault.text = "Default : " + policy;
                    }
                    else if (table == "OUTPUT")
                    {
                        outputDefaultRule = policy;
                        outputDefault.text = "Default : " + policy;
                    }
                }
            }
        }
        else if (commandsParse[1] == "-A")
        {
            if (commandsParse[2] == "INPUT")
            {
                string protocol = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "-p")
                    {
                        protocol = commandsParse[i + 1];
                        break;
                    }
                }
                string dport = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "--dport")
                    {
                        dport = commandsParse[i + 1];
                        break;
                    }
                }
                string sourceIP = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "-s")
                    {
                        sourceIP = commandsParse[i + 1];
                    }
                }
                string sourcePort = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "--sport")
                    {
                        sourcePort = commandsParse[i + 1];
                    }
                }
                string acceptOrDrop = commandsParse[commandsParse.Length - 1];
                string destinationIP = "ANY";

                string rule = acceptOrDrop + " " + sourceIP + " " + sourcePort + " " + destinationIP + " " + dport + " " + protocol;

                addInputRule(rule);



            }
            else if (commandsParse[2] == "OUTPUT")
            {
                string protocol = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "-p")
                    {
                        protocol = commandsParse[i + 1];
                        break;
                    }
                }
                string dport = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "--dport")
                    {
                        dport = commandsParse[i + 1];
                        break;
                    }
                }
                string sourceIP = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "-s")
                    {
                        sourceIP = commandsParse[i + 1];
                    }
                }
                string sourcePort = "ANY";
                for (int i = 0; i < commandsParse.Length; i++)
                {
                    if (commandsParse[i] == "--sport")
                    {
                        sourcePort = commandsParse[i + 1];
                    }
                }
                string acceptOrDrop = commandsParse[commandsParse.Length - 1];
                string destinationIP = "ANY";

                string rule = acceptOrDrop + " " + sourceIP + " " + sourcePort + " " + destinationIP + " " + dport + " " + protocol;
                addOutputRule(rule);

            }
        }
        else if (commandsParse[1] == "-D" || commandsParse[1] == "-d")
            Debug.Log("Deleting rule detected");
        {
            string table = "";
            if (commandsParse[2] == "INPUT" || commandsParse[2] == "OUTPUT")
            {
                table = commandsParse[2];
            }
            string lineNumber = "";
        
            if (commandsParse.Length > 2)
            {
                lineNumber = commandsParse[3];
            }
            Debug.Log("Table is " + table);
            Debug.Log("LINE NUMBER:" + lineNumber);
            if (table != "" && lineNumber != "" && lineNumber != "0")
            {
                if (table == "INPUT")
                {
                    int line = Convert.ToInt32(lineNumber);
                    Debug.Log("line after conversion:" + line);
                    try
                    {
                        inputTableRules.RemoveAt(line - 1);
                        Debug.Log("Child Count: " + inputTableLayout.GetComponent<Transform>().GetChild(line * 6).gameObject.name);
                        deleteIndexByLineNumberInput(line);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6))).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 1)).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 2)).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 3)).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 4)).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 5)).gameObject);
                        Destroy(inputTableLayout.GetComponent<Transform>().GetChild(((line * 6) + 6)).gameObject);
                       
                        
                    }
                    catch
                    {
                        Debug.Log("Got caught in INPUT for deletion");
                    }

                }
                else if (table == "OUTPUT")
                {
                    int line = Convert.ToInt32(lineNumber);
                    Debug.Log("line after conversion:" + line);
                    
                        outputTableRules.RemoveAt(line - 1);
                        deleteIndexByLineNumberOutput(line);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild(line * 6).gameObject);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild((line * 6) + 1).gameObject);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild((line * 6) + 2).gameObject);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild((line * 6) + 3).gameObject);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild((line * 6) + 4).gameObject);
                        Destroy(outputTableLayout.GetComponent<Transform>().GetChild((line * 6) + 5).gameObject);
                        
                    
                    
                }
            }
        }
    }

     void showInputTable()
    {
        Debug.Log("Show input");
        inputTableLayout.gameObject.SetActive(true);
        outputTableLayout.gameObject.SetActive(false);
        inputTableButton.GetComponent<Image>().color = selectedBlueColor;
        outputTableButton.GetComponent<Image>().color = UnselectedBlueColor;
        inputDefault.gameObject.SetActive(true);
        outputDefault.gameObject.SetActive(false);
        inputNumberLayout.gameObject.SetActive(true);
        outputNumberLayout.gameObject.SetActive(false);
        inputScrollview.SetActive(true);
        inputNumberview.SetActive(true);
        outputScrollview.SetActive(false);
        outputNumberview.SetActive(false);
    }
     void showOutputTable()
    {
        Debug.Log("Show output");
        inputTableLayout.gameObject.SetActive(false);
        outputTableLayout.gameObject.SetActive(true);
        inputTableButton.gameObject.GetComponent<Image>().color = UnselectedBlueColor;
        outputTableButton.gameObject.GetComponent<Image>().color = selectedBlueColor;
        inputDefault.gameObject.SetActive(false);
        outputDefault.gameObject.SetActive(true);
        inputNumberLayout.gameObject.SetActive(false);
        outputNumberLayout.gameObject.SetActive(true);
        inputScrollview.SetActive(false);
        inputNumberview.SetActive(false);
        outputScrollview.SetActive(true);
        outputNumberview.SetActive(true);

    }

    void addInputRule(string rule_s) //The current rule will have format : "RULE(ACCEPT or DROP) SourceIP SourcePort DestinationIP DestinationPort Protocol"
    {
        inputTableRules.Add(rule_s);
        string[] rule = rule_s.Split(' ');

        showInputTable();
        GameObject childRule;
        GameObject childIndex;
        TextMeshProUGUI textMesh;
        for (int i = 0; i < rule.Length; i++)
        {
            childRule = null;
            childRule = new GameObject(rule[i]);
            RectTransform childTrans = childRule.AddComponent<RectTransform>();
            childTrans.SetParent(inputTableLayout.transform);
            childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
            textMesh = null;
            textMesh = childRule.AddComponent<TextMeshProUGUI>();
            textMesh.font = textMeshFont;
            textMesh.fontSize = 20.52f;
            textMesh.text = rule[i];
            textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;

            if (i == 0)
            {
                inputIndex++;
                childIndex = new GameObject(inputIndex + "");
                RectTransform childIndexTrans = childIndex.AddComponent<RectTransform>();
                childIndexTrans.SetParent(inputNumberLayout.transform);
                childIndexTrans.sizeDelta = new Vector2(48.36f, 50);
                childIndexTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
                textMesh = null;
                textMesh = childIndex.AddComponent<TextMeshProUGUI>();
                textMesh.font = textMeshFont;
                textMesh.fontSize = 20.52f;
                textMesh.text = inputIndex + "";
                textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
            }




        }
    
   }

    void addOutputRule(string rule_s) //The current rule will have format : "RULE(ACCEPT or DROP) SourceIP SourcePort DestinationIP DestinationPort Protocol"
    {
        outputTableRules.Add(rule_s);
        string[] rule = rule_s.Split(' ');

        showOutputTable();

        GameObject childRule;
        GameObject childIndex;
        TextMeshProUGUI textMesh;
        for (int i = 0; i < rule.Length; i++)
        {
            childRule = null;
            childRule = new GameObject(rule[i]);
            RectTransform childTrans = childRule.AddComponent<RectTransform>();
            childTrans.SetParent(outputTableLayout.transform);
            childTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
            textMesh = null;
            textMesh = childRule.AddComponent<TextMeshProUGUI>();
            textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
            Debug.Log(textMesh.alignment);
            textMesh.font = textMeshFont;
            textMesh.fontSize = 20.52f;
            textMesh.text = rule[i];

            if (i == 0)
            {
                outputIndex++;
                childIndex = new GameObject(outputIndex + "");
                RectTransform childIndexTrans = childIndex.AddComponent<RectTransform>();
                childIndexTrans.SetParent(outputNumberLayout.transform);
                childIndexTrans.localScale = new Vector3(1.0002f, 1.0002f, 1.0002f);
                textMesh = null;
                textMesh = childIndex.AddComponent<TextMeshProUGUI>();
                textMesh.font = textMeshFont;
                textMesh.fontSize = 20.52f;
                textMesh.text = outputIndex + "";
                textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
            }

        }

    }


}