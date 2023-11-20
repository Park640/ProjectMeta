using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Instruction
{
    public string Question;
}
public class InstructionList
{
    public List<Instruction> Instruction;
}

public class KeyTTS
{
    public string api_key;
}

[System.Serializable]
public class SetTextToSpeech
{
    public SetInput input;
    public SetVoice voice;
    public SetAudioConfig audioConfig;
}
[System.Serializable]
public class SetInput
{
    public string text;
}
[System.Serializable]
public class SetVoice
{
    public string languageCode;
    public string name;
}
[System.Serializable]
public class SetAudioConfig
{
    public string audioEncoding;
    public float speakingRate;
    public int pitch;
    public int volumeGainDb;
}
[System.Serializable]
public class GetContent
{
    public string audioContent;
}