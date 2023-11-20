using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct ToClientPacket
{
    [MarshalAs(UnmanagedType.Bool)]
    public bool m_BoolVariable;
    public int m_IntVariable;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_IntArray;
    public float m_FloatlVariable;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_StringlVariable;
}


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct ToServerPacket
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_IntArray;
    public float m_FloatlVariable;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_StringlVariable;
    [MarshalAs(UnmanagedType.Bool)]
    public bool m_BoolVariable;
    public int m_IntVariable;
}
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct PlayerInfo
{
    public float Px;
    public float Py;
    public float Pz;
    public float Rx;
    public float Ry;
    public float Rz;

    public float Speed;
    public float MotionSpeed;

    [MarshalAs(UnmanagedType.Bool)]
    public bool Jump;
    [MarshalAs(UnmanagedType.Bool)]
    public bool Grounded;
    [MarshalAs(UnmanagedType.Bool)]
    public bool FreeFall;

    [MarshalAs(UnmanagedType.Bool)]
    public bool Roll_Anim;
    [MarshalAs(UnmanagedType.Bool)]
    public bool Walk_Anim;
    [MarshalAs(UnmanagedType.Bool)]
    public bool Open_Anim;
}