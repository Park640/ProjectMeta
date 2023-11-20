using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace OpenAI
{
    public class PlayerControll : MonoBehaviour
    {
        [SerializeField] private GameObject[] perspective;
        [SerializeField] private GameObject tablet;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private ChatGPT gpt;

        private PlayerInput pInput;

        private int pState = 0;
        private int pCount;
        private void Awake()
        {
            pCount = perspective.Length;
            pInput = GetComponent<PlayerInput>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                perspective[pState].SetActive(false);
                pState = (pState + 1) % pCount;
                perspective[pState].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                bool b = tablet.activeSelf;
                tablet.SetActive(!b);
                pInput.enabled = b;
                if (!b) inputField.ActivateInputField();
            }
            else if (tablet.activeSelf && Input.GetKeyDown(KeyCode.U))
            {
                inputField.ActivateInputField();
            }
            else if (tablet.activeSelf && Input.GetKeyDown(KeyCode.Return))
            {
                gpt.Talk(inputField.text);
                inputField.text = null;
                tablet.SetActive(false);
                pInput.enabled = true;
            }
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Interactive") && Input.GetKeyUp(KeyCode.E))
            {
                Animator anim = hit.gameObject.GetComponentInParent<Animator>();
                bool b = !anim.GetBool("boolB");
                anim.SetBool("boolB", b);
            }
        }
    }
}
