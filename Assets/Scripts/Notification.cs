using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Transform hideTransform;
    public Transform showTransform;
    public Messages messages;
    public Text textMessage;
    
    private float journeyLength;
    private bool isShowing;

    private void Start()
    {
        this.isShowing = false;
        this.textMessage.text = "Hello! I am \"The Game\"!";
    }

    // This method should be called inside an Update
    public void StartShowNotificationCoroutine(float duration)
    {
        if (!this.isShowing)
            this.StartCoroutine(this.ShowNotificationCoroutine(duration, 5F));
    }

    private IEnumerator ShowNotificationCoroutine(float duration, float waitTime)
    {
        this.isShowing = true;
        yield return new WaitForSeconds(waitTime);

        float elapsedTime = 0F;
        while (elapsedTime < duration)
        {
            this.transform.position = Vector3.Lerp(hideTransform.position, showTransform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(waitTime);
        this.StartCoroutine(this.HideNotificationCoroutine(duration));

        yield return new WaitForSeconds(waitTime * 6);
        this.RandomizeMessage();
        this.isShowing = false;
    }

    private IEnumerator HideNotificationCoroutine(float duration)
    {
        float elapsedTime = 0F;
        while (elapsedTime < duration)
        {
            this.transform.position = Vector3.Lerp(showTransform.position, hideTransform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void RandomizeMessage()
    {
        this.textMessage.text = messages.messages[Random.Range(0, messages.messages.Length)];
    }
}
