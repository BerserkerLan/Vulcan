using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondLevelController : MonoBehaviour {

    public PacketMovementIntoGate madridMovement;
    public PacketMovementIntoGate italyMovement;

    public Caravan italyCaravan;
    public Caravan madridCaravan;

    public GameObject madridCaravanPrefab;
    public GameObject italyCaravanPrefab;
    public Text rulesTextBox;
    public Gate gate;
    public Button playButton;
    public Button addRuleButton;
    public Dropdown inputOrOutputDD;
    public Dropdown acceptOrDropDD;
    public Dropdown factionIPDD;
    public Text levelText;
    public Dropdown portDD;
    public Button questionMarkIcon;
    public Sprite caravanMovingLeftSprite;
    public Sprite caravanMovingRightSprite;
    public GameObject winningPanel;
    public GameObject losingPanel;
    public Button losingButton;
    public Button winningButton;
    public GameObject[] infoObjects;
    public Button[] infoButtons;
    bool[] clicked;
    public static List<DataTypes.FirewallRule> rulesList;
    bool levelComplete;
    Vector3 madridCaravanPosition;
    Vector3 italyCaravanPosition;
    int currentGateIndex;

    // Use this for initialization
    void Start () {
        rulesList = new List<DataTypes.FirewallRule>();
        levelText.text += "02";
        clicked = new bool[infoButtons.Length];
        winningPanel.SetActive(false);
        losingPanel.SetActive(false);


        for (int i = 0; i < clicked.Length; i++)
        {
            clicked[i] = false;
        }
        playButton.onClick.AddListener(playFirstMovement);
        foreach (GameObject infoObject in infoObjects)
        {
            infoObject.SetActive(false); //Hide the info objects at start
        }
        infoButtons[0].onClick.AddListener(() => { showInfo(0); });
        infoButtons[1].onClick.AddListener(() => { showInfo(1); });
        infoButtons[2].onClick.AddListener(() => { showInfo(2); });
        infoButtons[3].onClick.AddListener(() => { showInfo(3); });
        infoButtons[4].onClick.AddListener(() => { showInfo(4); });
        infoButtons[5].onClick.AddListener(() => { showInfo(5); });
        infoButtons[6].onClick.AddListener(() => { showInfo(6); });
        infoButtons[7].onClick.AddListener(() => { showInfo(7); });


        addRuleButton.onClick.AddListener(addRule);
        levelComplete = false;
        madridCaravanPosition = madridCaravanPrefab.transform.position;
        italyCaravanPosition = italyCaravanPrefab.transform.position;
        losingButton.onClick.AddListener(dismissLosingPanel);
        questionMarkIcon.onClick.AddListener(showTutorial);
    }
	
	// TODO: Make update method for both packets
	void Update () {
		
	}
     
    //TODO: Add the movement for both caravans
    void playFirstMovement()
    {
        //PacketMovementIntoGate.ready = true;
        //italyMovement.setReady(false);
        madridMovement.setReady(true);

        foreach (GameObject infoObject in infoObjects)
        {
            infoObject.SetActive(false); //Hide the info objects at start
        }
        foreach (Button infoButton in infoButtons)
        {
            infoButton.gameObject.SetActive(false);
        }
        int elapsed = 0;
        //Spin until collision
        while (!madridMovement.getCollision())
        {

            Debug.Log("Stuck");
            return;
        }

       bool firstCaravanResult =  checkRules(madridCaravan);

        if (!firstCaravanResult)
        {
            italyCaravanPrefab.transform.position = italyCaravanPosition;
            madridCaravanPrefab.transform.position = madridCaravanPosition;
            madridMovement.collided = false;
            losingPanel.SetActive(true);
            italyCaravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
            madridCaravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
            rulesList = new List<DataTypes.FirewallRule>();
            //madridMovement.ready = false;

            foreach (Button infoButton in infoButtons)
            {
                infoButton.gameObject.SetActive(true);
            }
            rulesTextBox.text = "";
        }
        else
        {
            italyMovement.setReady(true);
            while (italyMovement.getCollision()) { }
            bool secondCaravanResult = checkRules(italyCaravan);
            if (!secondCaravanResult)
            {
                italyCaravanPrefab.transform.position = italyCaravanPosition;
                madridCaravanPrefab.transform.position = madridCaravanPosition;
                italyMovement.collided = false;
                losingPanel.SetActive(true);
                italyCaravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                madridCaravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
               // italyMovement.ready = false;

                foreach (Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }
                rulesTextBox.text = "";
            }
            else
            {
                levelComplete = true;
                italyMovement.collided = false;
                rulesList = new List<DataTypes.FirewallRule>();
                winningPanel.SetActive(true);
                rulesTextBox.text = "";
            }
        }
        

    }

    bool checkRules(Caravan caravan)
    {
        int inputOrOutput, acceptOrDrop;
        string IPAddress;
        DataTypes.ProtoPort protoPort;
        foreach (DataTypes.FirewallRule firewallRule in rulesList)
        {
            inputOrOutput = firewallRule.getInputOrOutput();
            acceptOrDrop = firewallRule.getAcceptOrDrop();
            IPAddress = firewallRule.getFactionIP();
            protoPort = firewallRule.getProtoPort();
            if ((inputOrOutput == DataTypes.INPUT) && (acceptOrDrop == DataTypes.ACCEPT) && ((caravan.factionName + " (" + caravan.IPAddress + ")") == IPAddress) && (protoPort == gate.protoPort))
            {
                return true;
            }

        }
        return false; //Player lost
    }

        void playSecondMovement()
    {
        italyMovement.setReady(true);
        
    }
    void showInfo(int index)
    {
        Debug.Log(index);
        if (!clicked[index])
        {
            infoObjects[index].SetActive(true);
            clicked[index] = true;
        }
        else
        {
            clicked[index] = false;
            infoObjects[index].SetActive(false);
        }
    }
    void addRule()
    {
        int inputOrOutput = getInputOrOutput();
        int acceptOrDrop = getAcceptOrDrop();
        string factionIP = getFactionIP();
        DataTypes.ProtoPort protoPort = getPort();
        DataTypes.FirewallRule firewallRule = new DataTypes.FirewallRule(inputOrOutput, acceptOrDrop, factionIP, protoPort);
        rulesList.Add(firewallRule);
        rulesTextBox.text += firewallRule.ToString() + "\n";

    }
    void dismissLosingPanel()
    {
        losingPanel.SetActive(false);
    }

    void showTutorial()
    {
        FirstLevelTutorial.showTutorial = 1;

    }

    public int getInputOrOutput()
    {
        string inputOrOutputString = inputOrOutputDD.options[inputOrOutputDD.value].text;

        if (inputOrOutputString == "INPUT")
        {
            return DataTypes.INPUT;
        }
        else
        {
            return DataTypes.OUTPUT;
        }
    }
    public int getAcceptOrDrop()
    {
        string acceptOrDropString = acceptOrDropDD.options[acceptOrDropDD.value].text;
        if (acceptOrDropString == "ACCEPT")
        {
            return DataTypes.ACCEPT;
        }
        else
        {
            return DataTypes.DROP;
        }
    }
    public string getFactionIP()
    {
        string factionIP = factionIPDD.options[factionIPDD.value].text;
        return factionIP;
    }
    public DataTypes.ProtoPort getPort()
    {
        string port = portDD.options[portDD.value].text;
        return convertToProtoPort(port);
    }
    public DataTypes.ProtoPort convertToProtoPort(string port)
    {
        if (port == "HTTP (80)")
        {
            return DataTypes.ProtoPort.HTTP;
        }
        else if (port == "SMTP (25)")
        {
            return DataTypes.ProtoPort.SMTP;
        }
        else if (port == "DNS (53)")
        {
            return DataTypes.ProtoPort.DNS;
        }
        else if (port == "FTP (21)")
        {
            return DataTypes.ProtoPort.FTP;
        }
        else
        {
            return DataTypes.ProtoPort.SSH;
        }
    }



}
