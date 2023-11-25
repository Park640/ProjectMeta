using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

namespace OpenAI
{
    public class PlayerControll : MonoBehaviour
    {
        [SerializeField] private GameObject[] perspective;
        [SerializeField] private GameObject tablet;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private ChatGPT gpt;
        [SerializeField] private TCPClient tcpClient;
        [SerializeField] private Animator anim;
        [SerializeField] private Canvas canvas;

        public Transform nicknamePos;
        public CinemachineVirtualCamera[] cameras;
        public PhotonView photon;

        public GameObject aboveUI;

        private TextMeshProUGUI nameTxt;
        private PlayerInput pInput;
        private int pState = 0;
        private int pCount;
        private void Awake()
        {
            aboveUI = FindObjectOfType<AboveUI>().gameObject;
            gpt = FindObjectOfType<ChatGPT>();
            canvas = FindAnyObjectByType<Canvas>();
            tablet = canvas.GetComponentInChildren<TabletUI>(true).gameObject;
            inputField = tablet.GetComponentInChildren<TMP_InputField>();
            //tablet = FindObjectOfType<TabletUI>(true).gameObject;
            anim = GetComponent<Animator>();
            tcpClient = FindObjectOfType<TCPClient>();
            pCount = perspective.Length;
            pInput = GetComponent<PlayerInput>();
        }

        public void AboveUIPosition()
        {
            aboveUI.transform.position = Camera.main.WorldToScreenPoint(nicknamePos.position);
        }
        private void Update()
        {
            if (!photon.IsMine)
            {
                pInput.enabled = false;
                cameras[0].enabled = false;
                cameras[1].enabled = false;
                return;
            }
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
            AboveUIPosition();

        }
        private void LateUpdate()
        {

        }
        private void PlayerPositionTransfer()
        {
            Playertransform pf = new Playertransform();
           /* pf.Px = transform.position.x;
            pf.Py = transform.position.y;
            pf.Pz = transform.position.z;

            pf.Ry = transform.rotation.y;

            pf.Speed = anim.GetFloat("Speed");
            pf.MotionSpeed = anim.GetFloat("MotionSpeed");

            pf.Jump = anim.GetBool("Jump");
            pf.Grounded = anim.GetBool("Grounded");
            pf.FreeFall = anim.GetBool("FreeFall");*/

            tcpClient.PacketTransfer(DefaultPacket.Serialize(pf));

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





