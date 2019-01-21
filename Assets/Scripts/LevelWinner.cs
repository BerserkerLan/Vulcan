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
    [TextArea(20,30)]
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


    // Use this for initialization
    void Start () {
        selectedBlueColor = new Color();
        UnselectedBlueColor = new Color();
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
        }
        networkDiagrams[levelNumber - 1].SetActive(true);
        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = hintText[levelNumber - 1];
        hintBox.SetActive(false);

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

    IEnumerator loadLevel5Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_5;
        SceneManager.LoadScene(0);
    }

    IEnumerator loadLevel3Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_3;
        SceneManager.LoadScene(0);
    }

    IEnumerator loadLevel4Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_4;
        SceneManager.LoadScene(0);
    }

    IEnumerator loadLevel2Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_2;
        SceneManager.LoadScene(0);
    }

    IEnumerator loadLevel6Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_6;
        SceneManager.LoadScene(0);
    }
    IEnumerator loadLevel7Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_7;
        SceneManager.LoadScene(0);
    }
    IEnumerator loadLevel8Tutorial()
    {
        yield return new WaitForSeconds(6);
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_8;
        SceneManager.LoadScene(0);
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


            if (inputTableRules.Contains(winningRule1)  && outputTableRules.Contains(winningRule3) &&inputTableRules.Contains(winningRule2) && inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP")
            {
                Debug.Log("You won!");
                alicePacketLevel3.SetActive(true);
                bobPacketLevel3.SetActive(true);
                charliePacketLevel3.SetActive(true);
                alicePacketLevel3.GetComponent<Animation>().Play();
                charliePacketLevel3.GetComponent<Animation>().Play();
                bobPacketLevel3.GetComponent<Animation>().Play();
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
            //Can be done in two ways, but I'm just going to allow one way
            // Way 1 : They simple do this rule on both the INPUT and OUTPUT table "DROP ANY ANY ANY http tcp" i.e iptables -A <tableName> --dport http -p tcp -j DROP

            string winningRule1 = "DROP ANY ANY ANY http tcp";

            if (inputTableRules.Contains(winningRule1) && outputTableRules.Contains(winningRule1))
            {
                Debug.Log("You won!");
                alicePacketLevel4.SetActive(true);
                bobPacketLevel4.SetActive(true);
                charliePacketLevel4.SetActive(true);
                alicePacketLevel4.GetComponent<Animation>().Play();
                bobPacketLevel4.GetComponent<Animation>().Play();
                charliePacketLevel4.GetComponent<Animation>().Play();
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

            bool cond1 = (outputTableRules.Contains("ACCEPT 122.15.43.22 ssh ANY ssh tcp") || outputTableRules.Contains("ACCEPT 192.11.76.5 22 ANY 22 tcp"));
            bool cond2 = (inputTableRules.Contains("ACCEPT 192.11.76.5 ssh ANY ssh tcp") || inputTableRules.Contains("ACCEPT 122.15.43.22 22 ANY 22 tcp"));
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
                StartCoroutine(loadLevel6Tutorial());

            }
            else if (cond1 && cond2 && inputTableRules.Contains("DROP ANY ANY ANY ANY ANY") && outputTableRules.Contains("DROP ANY ANY ANY ANY ANY"))
            {
                Debug.Log("You won!");
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

            bool cond1 = inputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY ANY udp") && inputTableRules.Contains("DROP 192.168.1.33 80 ANY 80 tcp");
            bool cond2 = outputTableRules.Contains("DROP 122.15.43.22 80 ANY 80 tcp");

            if (cond1 && cond2)
            {
                Debug.Log("You won!");
                alicePacketLevel6.SetActive(true);
                bobPacketLevel6.SetActive(true);
                charliePacketLevel6.SetActive(true);
                alicePacketLevel6.GetComponent<Animation>().Play();
                bobPacketLevel6.GetComponent<Animation>().Play();
                charliePacketLevel6.GetComponent<Animation>().Play();
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
    
            bool cond1 = outputTableRules.Contains("ACCEPT 122.15.43.22 ANY ANY ANY tcp") && (outputTableRules.Contains("DROP 122.15.43.22 ssh ANY ssh tcp") || outputTableRules.Contains("DROP 122.15.43.22 22 ANY 22 tcp"));
            bool cond2 = (inputTableRules.Contains("ACCEPT 192.168.1.33 ssh ANY ssh tcp") || inputTableRules.Contains("ACCEPT 192.168.1.33 22 ANY 22 tcp")) && inputTableRules.Contains("ACCEPT 192.11.76.5 ANY ANY ANY tcp");
            
            if (cond1 && cond2 && inputDefault.text == "Default : DROP" && outputDefault.text == "Default : DROP")
            {
                Debug.Log("You won!");
                alicePacketLevel7.SetActive(true);
                bobPacketLevel7.SetActive(true);
                charliePacketLevel7.SetActive(true);
                alicePacketLevel7.GetComponent<Animation>().Play();
                bobPacketLevel7.GetComponent<Animation>().Play();
                charliePacketLevel7.GetComponent<Animation>().Play();
                StartCoroutine(loadLevel8Tutorial());
            }
            else
            {
                ShowLosingPanel();
            }




        }
        if (levelNumber == 8)
        {
            //NEED TO DO ; _ ;

        }
    }

    void StartTutorial2()
    {
        BreifingTextControl.changedTutorialState = true;
        BreifingTextControl.staticTutorialState = BreifingTextControl.TutorialState.tutorial_2;
        SceneManager.LoadScene(0);
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

                string rule = acceptOrDrop + " " + sourceIP + " " + sourcePort + " " + destinationIP + " " + dport  + " "+ protocol;
                addOutputRule(rule);

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

    void addInputRule(string rule_s) //The current rule will have format : "RULE(ACCEPT or DROP) SourceIP SourcePort DestinationIP DestinationPort Protocol"
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
            textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
        }
    
   }

    void addOutputRule(string rule_s) //The current rule will have format : "RULE(ACCEPT or DROP) SourceIP SourcePort DestinationIP DestinationPort Protocol"
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
            textMesh.alignment = TextAlignmentOptions.MidlineGeoAligned;
            Debug.Log(textMesh.alignment);
            textMesh.font = textMeshFont;
            textMesh.fontSize = 20.52f;
            textMesh.text = rule[i];
            
        }

    }


}