using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RulesMouseEnter : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject gameObject = GameObject.Find("ruleTxt");

        Text ruleText = gameObject.GetComponent<Text>();

        ruleText.text = eventData.pointerCurrentRaycast.gameObject.name;
        ruleText.fontSize = 12;
    }

  
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("MOUSE IS DOWN BRO");
        if (Input.GetMouseButtonDown(1))
        {  
            string rule = eventData.pointerCurrentRaycast.gameObject.name;
            for (int i = 0; i < LevelController.rulesList.Count; i++)
            {
                if (LevelController.rulesList[i].ToString().Equals(rule))
                {
                    LevelController.rulesList.Remove(LevelController.rulesList[i]);
                }
            }
            LevelController.rulesIndex--;
           

            DestroyImmediate(gameObject);
            GameObject rulesListObj = GameObject.Find("RuleList");
            Transform parent = rulesListObj.transform;
            int childInd = 1;
            foreach (Transform child in parent)
            {
                    Debug.Log(child.GetComponent<TextMeshProUGUI>().text);
                    child.GetComponent<TextMeshProUGUI>().text = childInd + "";
                    Debug.Log("New name:" + child.GetComponent<TextMeshProUGUI>().text + " with name " + child.name.ToString());
                    childInd++;
                
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject gameObject = GameObject.Find("ruleTxt");
        Text ruleText = gameObject.GetComponent<Text>();

        ruleText.text = "";
    }
}
