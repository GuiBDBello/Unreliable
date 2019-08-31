using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowHowToPlay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject commands;
    public GameObject enemies;

    private Color originalButtonColor;
    private Color originalTextColor;

    // Start is called before the first frame update
    void Start()
    {
        this.originalButtonColor = this.gameObject.GetComponent<Image>().color;
        this.originalButtonColor = this.gameObject.GetComponentInChildren<Text>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0F);
        this.gameObject.GetComponentInChildren<Text>().color = new Color(0F, 0F, 0F, 0F);

        commands.SetActive(true);
        enemies.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = Color.white;
        this.gameObject.GetComponentInChildren<Text>().color = new Color(0F, 0F, 0F, 0.4F);

        commands.SetActive(false);
        enemies.SetActive(false);
    }
}
