using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasControll : MonoBehaviour
{
    private CanvasScaler scaler;
    [SerializeField] TextMeshPro boardText;
    private void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
        scaler.referenceResolution =  new Vector2(Screen.width, Screen.height);
    }
    IEnumerator TextPrint(string answer)
    {
        int count = 0;
        boardText.text = null;
        while (count != answer.Length)
        {
            if (count < answer.Length)
            {
                boardText.text += answer[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    public void StartTwinkle(string a)
    {
        StartCoroutine(TextPrint(a));
    }
}
