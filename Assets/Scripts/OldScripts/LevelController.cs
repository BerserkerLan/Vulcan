using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class LevelController : MonoBehaviour {

    public Font theFont;
    public static Font fontForHover;

    public PacketMovementIntoGate caravanMovement;

    public Canvas canvasParent;

    public TMP_Dropdown inputOrOutputDD;
    public TMP_Dropdown acceptOrDropDD;
    public TMP_Dropdown portDD;
    public TMP_FontAsset textMeshFont;

    public GameObject caravanPrefab;
    public VerticalLayoutGroup rulesListView;
    public Gate gate;
    public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Button addRuleButton;
    public Caravan caravan;
    public Text levelText;
    public UnityEngine.UI.Button questionMarkIcon;
    public Sprite caravanMovingLeftSprite;
    public Sprite caravanMovingDownSprite;
    public GameObject winningPanel;
    public GameObject losingPanel;
    public Text losingText;
    public UnityEngine.UI.Button losingButton;
    public UnityEngine.UI.Button winningButton;
    public GameObject[] infoObjects;
    Vector3 pos;
    public UnityEngine.UI.Button[] infoButtons;
    public static bool[] clicked;
    public static List<DataTypes.FirewallRule> rulesList;
    public static bool levelComplete;
    Vector3 packetPosition;
    int currentGateIndex;
    public static int rulesIndex;
    public GameObject UITutorialPanel;
    public static int showUITutorial;

    public static GameObject[] stat_infoObjects;
    public static UnityEngine.UI.Button[] stat_infoButtons;
    


	void Start () {
        showUITutorial = 0;
        rulesList = new List<DataTypes.FirewallRule>();
        UITutorialPanel.gameObject.SetActive(false);
        fontForHover = theFont;
        levelText.text += "01";
        clicked = new bool[infoButtons.Length];
        winningPanel.SetActive(false);
        losingPanel.SetActive(false);
        rulesIndex = 0;
        stat_infoButtons = infoButtons;
        stat_infoObjects = infoObjects;

        
        for (int i = 0; i < clicked.Length; i++)
        {
            clicked[i] = false;
        }
        playButton.onClick.AddListener(playPacketMovement);
        foreach (GameObject infoObject in infoObjects)
        {
            infoObject.SetActive(false); //Hide the info objects at start
        }
       

        for (int i = 0; i < 10; i++)
        {
            infoButtons[i].gameObject.AddComponent<InfoMouseEnter>();
        }
        
        addRuleButton.onClick.AddListener(addRule);
        levelComplete = false;
        packetPosition = caravanPrefab.transform.position;
        losingButton.onClick.AddListener(dismissLosingPanel);
        questionMarkIcon.onClick.AddListener(showTutorial);
	}


    void Update() {
        //Handdles if caravan should go through, or go to next gate
        if (caravanMovement.getCollision())
        {
            Debug.Log(caravanMovement.recentCollision.gameObject.name);
            if (caravanMovement.recentCollision.gameObject.name == "dragon")
            {
                losingText.text = "The dragon swallowed your entire caravan whole! Wrong Rule! \n \n Please try again.";
                caravanPrefab.transform.position = packetPosition;
                caravanMovement.collided = false;
                caravanMovement.currentGateIndex = 0;
                losingPanel.SetActive(true);
                caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
                caravanMovement.ready = false;
                AudioSource dragon_roar = caravanMovement.recentCollision.gameObject.GetComponent<AudioSource>();
                dragon_roar.Play();
            
                foreach (UnityEngine.UI.Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }
            }
            else if (caravanMovement.recentCollision.gameObject.name == "cat")
            {
                losingText.text = "THE CARAVAN CRUSHED A KITTEN. \n YOU KILLED A KITTEN. \n Please try again.";
                caravanPrefab.transform.position = packetPosition;
                caravanMovement.collided = false;
                caravanMovement.currentGateIndex = 0;
                losingPanel.SetActive(true);
                caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
                caravanMovement.ready = false;
                AudioSource cat_meow = caravanMovement.recentCollision.gameObject.GetComponent<AudioSource>();
                cat_meow.Play();
                foreach (UnityEngine.UI.Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }
            }
            else if (caravanMovement.recentCollision.gameObject.name == "Lava")
            {
                losingText.text = "The caravan melted into the lava. \n Its dead. \n Please try again.";
                caravanPrefab.transform.position = packetPosition;
                caravanMovement.collided = false;
                caravanMovement.currentGateIndex = 0;
                losingPanel.SetActive(true);
                caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
                caravanMovement.ready = false;
                AudioSource lava_melting_sound = caravanMovement.recentCollision.gameObject.GetComponent<AudioSource>();
                lava_melting_sound.Play();
                foreach (UnityEngine.UI.Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }
            }
            else if (caravanMovement.recentCollision.gameObject.name == "80_gate")
            {
                losingText.text = "The gates are closed, so the caravan is not allowed to pass. \n Please try again.";
                caravanPrefab.transform.position = packetPosition;
                caravanMovement.collided = false;
                losingPanel.SetActive(true);
                caravanMovement.currentGateIndex = 0;
                caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
                caravanMovement.ready = false;
                foreach (UnityEngine.UI.Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }
            }
            else if (caravanMovement.recentCollision.gameObject.name == "market")
            {
               // winningPanel.text = "The gates are closed, so the caravan is not allowed to pass. \n Please try again.";
               // caravanPrefab.transform.position = packetPosition;
               // caravanMovement.collided = false;
                winningPanel.SetActive(true);
                //caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                rulesList = new List<DataTypes.FirewallRule>();
               // caravanMovement.ready = false;
                /*foreach (UnityEngine.UI.Button infoButton in infoButtons)
                {
                    infoButton.gameObject.SetActive(true);
                }*/
            }
            else
            {
                Collision2D theCollision = caravanMovement.recentCollision;
                Gate collisionGate = theCollision.gameObject.GetComponent<Gate>();
                DataTypes.ProtoPort gatePort = collisionGate.protoPort;
                int inputOrOutput, acceptOrDrop;
                string IPAddress;
                DataTypes.ProtoPort protoPort;
                foreach (DataTypes.FirewallRule firewallRule in rulesList)
                {
                    inputOrOutput = firewallRule.getInputOrOutput();
                    acceptOrDrop = firewallRule.getAcceptOrDrop();
                    IPAddress = firewallRule.getFactionIP();
                    protoPort = firewallRule.getProtoPort();
                    if ((inputOrOutput == DataTypes.INPUT) && (acceptOrDrop == DataTypes.ACCEPT) && ((caravan.factionName + " (" + caravan.IPAddress + ")") == IPAddress) && (protoPort == gatePort))
                    {
                        //Let it through
                        levelComplete = true;
                        caravanMovement.collided = false;
                        rulesList = new List<DataTypes.FirewallRule>();
                        //winningPanel.SetActive(true);
                        break;

                    }

                }
                if (!levelComplete)
                {

                    if (caravanMovement.currentGateIndex == 4) //If this was the last gate
                    {
                        Debug.Log("You lost due to either no rules, or just no rule to match this caravan, and it dropped the packet by default");
                        caravanPrefab.transform.position = packetPosition;
                        caravanMovement.collided = false;
                        losingPanel.SetActive(true);
                        caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                        rulesList = new List<DataTypes.FirewallRule>();
                        caravanMovement.ready = false;
                        foreach (UnityEngine.UI.Button infoButton in infoButtons)
                        {
                            infoButton.gameObject.SetActive(true);
                        }
                    }
                    else //If this wasn't the last gate, we keep moving towards the left, but first we move down till the road
                    {
                        caravanMovement.ready = false; //Stop packetmovement update method
                        caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingDownSprite;

                        if (caravan.transform.position.y != packetPosition.y)
                        {
                            //caravan.transform.position -= velocity * Time.fixedDeltaTime;
                            caravan.transform.position = Vector3.MoveTowards(caravan.transform.position, new Vector3(caravan.transform.position.x, packetPosition.y, caravan.transform.position.z), 3 * Time.deltaTime);

                        }
                        else
                        {
                            //Now we're back on the road, so we change sprite back to left one
                            caravanPrefab.GetComponent<SpriteRenderer>().sprite = caravanMovingLeftSprite;
                            caravanMovement.currentGateIndex++; //Update the current index to show its going to the next gate
                            caravanMovement.ready = true; //Start packetmovement updatee method so it moves left
                            caravanMovement.collided = false;
                        }

                    }

                }
            }

        }
    }
            
        
	
    void dismissLosingPanel()
    {
        losingPanel.SetActive(false);
    }
    void showTutorial()
    {
        FirstLevelTutorial.showTutorial = 1;
       
    }

    void UITutorial()
    {
        UITutorialPanel.gameObject.SetActive(true);
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

    void playPacketMovement()
    {
        caravanMovement.ready = true;
        foreach (GameObject infoObject in infoObjects)
        {
            infoObject.SetActive(false); //Hide the info objects at start
        }
        foreach(UnityEngine.UI.Button infoButton in infoButtons) {
            infoButton.gameObject.SetActive(false);
        }

    }

    void addRule()
    {
        if (rulesIndex == 4)
        {
            Debug.Log("This is the max amount of rules needed for this level. HINT: You only need to add one rule.");
            return;
        }
        rulesIndex++;
        
        int inputOrOutput = getInputOrOutput();
        int acceptOrDrop = getAcceptOrDrop();
        DataTypes.ProtoPort protoPort = getPort();
        DataTypes.FirewallRule firewallRule = new DataTypes.FirewallRule(DataTypes.INPUT,acceptOrDrop,"Amazon (72.21.215.90)",protoPort);
        rulesList.Add(firewallRule);
        RectTransform parent = rulesListView.GetComponent<RectTransform>();
       
        GameObject g = new GameObject(firewallRule.getFactionIP() + " | " + (int)firewallRule.getProtoPort() + " | " + firewallRule.getADAsString());
        TextMeshProUGUI t = g.AddComponent<TextMeshProUGUI>();
        t.transform.SetParent(parent);
        t.font = textMeshFont;

       RectTransform rect =  t.GetComponent<RectTransform>();

        Debug.Log(rect.sizeDelta);
        t.fontSize = 35;
        rect.sizeDelta = new Vector2(30, 25);
        t.alignment = TMPro.TextAlignmentOptions.Midline;
        t.color = new Color(163.0f / 255.0f, 15.0f / 255.0f, 45.0f / 255.0f);

        g.AddComponent<RulesMouseEnter>();



        Debug.Log(rulesIndex);
        t.text = rulesIndex + "";
        


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

  

    public DataTypes.ProtoPort getPort()
    {
        string port = portDD.options[portDD.value].text;
        return convertToProtoPort(port);
    }

    public DataTypes.ProtoPort convertToProtoPort(string port)
    {
        if (port == "80")
        {
            return DataTypes.ProtoPort.HTTP;
        }
        else if (port == "25")
        {
            return DataTypes.ProtoPort.SMTP;
        }
        else if (port == "53")
        {
            return DataTypes.ProtoPort.DNS;
        }
        else if (port == "21")
        {
            return DataTypes.ProtoPort.FTP;
        }
        else
        {
            return DataTypes.ProtoPort.SSH;
        }
    }
}
