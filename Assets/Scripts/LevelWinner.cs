using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWinner : MonoBehaviour {

    List<string> inputTableRules = new List<string>();
    List<string> outputTableRules = new List<string>();
    string inputDefaultRule = "Default : ACCEPT";
    string outputDefaultRule = "Default : ACCEPT";
    bool hintShowing = false;

    public int levelNumber;
    public TMP_FontAsset textMeshFont;
    public Button playButton;
    public Button runTerminalCommandButton;
    public Button inputTableButton, outputTableButton;
    public TMP_InputField terminalInput;
    public TMP_Text inputDefault, outputDefault;
    public GridLayoutGroup inputTableLayout, outputTableLayout;
    Color selectedBlueColor, UnselectedBlueColor;
    public Packet bobPacket, alicePacket;
    public Button hintButton;
    public GameObject hintBox;
    public GameObject UITutorial;
    public GameObject[] networkDiagrams;
    public GameObject losingPanel;
    

	// Use this for initialization
	void Start () {
        selectedBlueColor = new Color();
        UnselectedBlueColor = new Color();
        hintBox.SetActive(false);
        ColorUtility.TryParseHtmlString("#2F49AD", out selectedBlueColor);
        ColorUtility.TryParseHtmlString("#3F63EC", out UnselectedBlueColor);
        playButton.onClick.AddListener(handleLevel);
        runTerminalCommandButton.onClick.AddListener(runCommand);
        inputTableButton.onClick.AddListener(showInputTable);
        outputTableButton.onClick.AddListener(showOutputTable);
        hintButton.onClick.AddListener(showOrHideHint);
        if (levelNumber == 1)
        {
            showUITutorial();
            networkDiagrams[levelNumber - 1].SetActive(true);
        }
	}
	
    void showUITutorial()
    {
        UITutorial.SetActive(true);
    }
    void ShowLosingPanel()
    {
        
        StartCoroutine(panelTemporaryEnum());

    }
    IEnumerator panelTemporaryEnum()
    {
        losingPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        losingPanel.SetActive(false);

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

            }
            else
            {
                Debug.Log("You lost :(, Try Again");
                ShowLosingPanel();
            }
          
        }
    }

    void StartTutorial2()

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
                string table = "INPUT";
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
                string acceptOrDrop = commandsParse[commandsParse.Length - 1];

                string rule = table + " " + protocol + " " + dport + " " + sourceIP + " " + acceptOrDrop;
                addInputRule(rule);
                


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

    }

    void addInputRule(string rule_s) //The current rule will have format : "table(INPUT) Protocol(any if none) dport(any if none) sourceIP AcceptOrDrop"
    {
        inputTableRules.Add(rule_s);
        string[] rule = rule_s.Split(' ');

        GameObject childRule;
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
        }
    
   }

    void addOutputRule(string rule_s) //The current rule will have format : "table(INPUT) Protocol(any if none) dport(any if none) sourceIP AcceptOrDrop"
    {
        outputTableRules.Add(rule_s);
        string[] rule = rule_s.Split(' ');

        GameObject childRule;
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
            textMesh.font = textMeshFont;
            textMesh.fontSize = 20.52f;
            textMesh.text = rule[i];
        }

    }


}
