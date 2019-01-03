using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour {

    public Sprite[] tutImages;
    int imagesIndex;
    public Button nextButton;

	void Start () {
        imagesIndex = 0;
        nextButton.onClick.AddListener(loadNextImage);
	}
	
    void loadNextImage() 
    {
        if (imagesIndex == tutImages.Length - 1)
        {
            hideUITutorial();
            return;
        }
        imagesIndex++;
        gameObject.GetComponent<Image>().sprite = tutImages[imagesIndex];
    }

    void hideUITutorial()
    {
        gameObject.SetActive(false);
    }

}
