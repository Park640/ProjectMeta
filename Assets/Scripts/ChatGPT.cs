using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.IO;


namespace OpenAI 
{
    public class ChatGPT : MonoBehaviour
    {
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private InstructionList iList;
        private SetTextToSpeech tts = new SetTextToSpeech();
        private int EOI;

        private string url;
        private AudioSource audioS;

        public string fromGPT;
        public string fromTTS;

        public CanvasControll canvasC;
        [SerializeField] private Animator anim;

        private void Awake()
        {
            audioS = GetComponent<AudioSource>();
            InputInstruction();
            GetTTSKey();
        }
        private void GetTTSKey()
        {
            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var authPath = $"{userPath}/.openai/tts.json";

            if (File.Exists(authPath))
            {
                string s = System.IO.File.ReadAllText(authPath);
                KeyTTS apiKey = JsonUtility.FromJson<KeyTTS>(s);
                url = apiKey.api_key;

            }
        }
        private void InputInstruction()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Instruction");
            iList = JsonUtility.FromJson<InstructionList>(textAsset.text);

            foreach (Instruction ins in iList.Instruction)
            {
                var newMessage = new ChatMessage()
                {
                    Role = "assistant",
                    Content = ins.Question
                };
                messages.Add(newMessage);
            }
            EOI = messages.Count;
        }
        public async Task TextToGPT(string s)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = s
            };

            messages.Add(newMessage);
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                fromGPT = message.Content;
                messages.RemoveAt(EOI);
            }
            StartCoroutine(TextToSpeak());
        }
        public async void Talk(string s)
        {
            anim.SetBool("Roll_Anim", true);
            await TextToGPT(s);
        }
        IEnumerator TextToSpeak()
        {
            AudioSetting(fromGPT);
            string str = JsonUtility.ToJson(tts);
            UploadHandlerRaw uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(str));
            DownloadHandler downloadHandler = new DownloadHandlerBuffer();
            using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST, downloadHandler, uploadHandler))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.uploadHandler.contentType = "application/json";
                yield return request.SendWebRequest();
                fromTTS = request.downloadHandler.text;
                request.Dispose();
            }
            anim.SetBool("Roll_Anim", false);
            GetContent info = JsonUtility.FromJson<GetContent>(fromTTS);
            var bytes = Convert.FromBase64String(info.audioContent);

            var f = ConvertByteToFloat(bytes);
            AudioClip audioClip = AudioClip.Create("audioContent", f.Length, 1, 44100, false);
            audioClip.SetData(f, 0);

            audioS.clip = audioClip;
            audioS.Play();
            canvasC.StartTwinkle(fromGPT);
        }
        public void CreateAudio()
        {
            var str = fromTTS;
            GetContent info = JsonUtility.FromJson<GetContent>(str);
            var bytes = Convert.FromBase64String(info.audioContent);

            var f = ConvertByteToFloat(bytes);
            AudioClip audioClip = AudioClip.Create("audioContent", f.Length, 1, 44100, false);
            audioClip.SetData(f, 0);

            audioS.clip = audioClip;
            audioS.Play();
        }
        public void AudioSetting(string text)
        {
            SetInput si = new SetInput();
            si.text = text;
            tts.input = si;

            SetVoice sv = new SetVoice();
            sv.languageCode = "ko-KR";
            sv.name = "ko-KR-Neural2-C";
            tts.voice = sv;

            SetAudioConfig sa = new SetAudioConfig();
            sa.audioEncoding = "LINEAR16";
            sa.speakingRate = 1;
            sa.pitch = 1;
            sa.volumeGainDb = 0;
            tts.audioConfig = sa;
        }
        private static float[] ConvertByteToFloat(byte[] array)
        {
            float[] floatArr = new float[array.Length / 2];
            for (int i = 0; i < floatArr.Length; i++)
            {
                floatArr[i] = BitConverter.ToInt16(array, i * 2) / 32768.0f;
            }
            return floatArr;
        }
    }
}


