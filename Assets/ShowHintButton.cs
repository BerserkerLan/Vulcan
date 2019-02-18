using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHintButton : MonoBehaviour {

    public GameObject hintBox;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(hideHint);
    }

    void hideHint()
    {
        hintBox.SetActive(true);
    }
}
