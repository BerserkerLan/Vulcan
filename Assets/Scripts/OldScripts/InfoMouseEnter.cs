using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoMouseEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        string name = eventData.pointerCurrentRaycast.gameObject.name;
        Debug.Log(name);
        name = name.Substring(0, name.IndexOf('B'));
        Debug.Log(name);
        foreach (GameObject infoObject in LevelController.stat_infoObjects)
        {
            if (infoObject.gameObject.name == name)
            {
                infoObject.gameObject.SetActive(true);
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        foreach (GameObject infoObject in LevelController.stat_infoObjects)
        {
           
                infoObject.gameObject.SetActive(false);
            
        }
    }
}
