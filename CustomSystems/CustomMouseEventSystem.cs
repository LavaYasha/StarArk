using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomMouseEventSystem : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent LeftMouseButtonReleased;
    public UnityEvent RightMouseButtonReleased;
    public UnityEvent MiddleMouseButtonReleased;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Pressed left click.
            LeftMouseButtonReleased.Invoke();
            //gameObject.GetComponent<Image>().color = Color.green;
            //Debug.Log("left");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Pressed right click.
            RightMouseButtonReleased.Invoke();
            //gameObject.GetComponent<Image>().color = Color.red;
            //Debug.Log("right");
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            //Pressed middle click.
            MiddleMouseButtonReleased.Invoke();
            //gameObject.GetComponent<Image>().color = Color.magenta;
            //Debug.Log("middle");
        }
    }

}
