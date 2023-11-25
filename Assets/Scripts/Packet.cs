using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum PacketType : int
{
    PlayerInfo,
    PlayerPos,
    Interactive,
    ChatGPT,
    Chatting
}

[Serializable]
public class DefaultPacket
{
    public int packet_Type;

    public DefaultPacket()
    {
        this.packet_Type = 0;
    }

    public static byte[] Serialize(Object data)
    {
        try
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, data);
            return ms.ToArray();
        }
        catch
        {
            return null;
        }
    }

    public static Object Deserialize(byte[] data)
    {
        try
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            ms.Write(data, 0, data.Length);

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
        catch
        {
            return null;
        }
    }
}

[Serializable]
public class PlayerInfo : DefaultPacket
{
    public string Pname;
    public int classRoom;
}

[Serializable]
public class Playertransform : DefaultPacket
{
    public float Px;
    public float Py;
    public float Pz;
    public float Rx;
    public float Ry;
    public float Rz;

    public float Speed;
    public float MotionSpeed;

    public bool Jump;
    public bool Grounded;
    public bool FreeFall;

    public Playertransform()
    {
        this.packet_Type = (int)PacketType.PlayerPos;
    }
}


