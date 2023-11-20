using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class Tablet : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private PlayerInput pInput;
    private void Start()
    {
        pInput.enabled = false;

        field.ActivateInputField();
    }
}
