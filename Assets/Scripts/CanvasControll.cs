using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasControll : MonoBehaviour
{
    private CanvasScaler scaler;

    private void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
        scaler.referenceResolution =  new Vector2(Screen.width, Screen.height);
    }
}
