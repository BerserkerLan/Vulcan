using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBreifingButton : MonoBehaviour {

    public GameObject breifingBox;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(hideHint);
    }

    void hideHint()
    {
        breifingBox.SetActive(false);
    }
}
