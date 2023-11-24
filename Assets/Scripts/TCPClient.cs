using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class TCPClient : MonoBehaviour
{
	public string m_Ip = "127.0.0.1";
	public int m_Port = 50001;
	private TcpClient m_Client;
	private Thread m_ThrdClientReceive;

	public bool isConnected = false;
	Playertransform pt = new Playertransform();

	private void Awake()
	{
		if (!isConnected) ConnectToTcpServer();
		DestroyCheck();
	}

	void OnApplicationQuit()
	{
		m_ThrdClientReceive.Abort();

		if (m_Client != null)
		{
			m_Client.Close();
			m_Client = null;
		}
	}
	public void DestroyCheck()
	{
		var obj = FindObjectsOfType<TCPClient>();
		if (obj.Length == 1)
		{
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	public void ConnectToTcpServer()
	{
		try
		{
			isConnected = true;
			m_ThrdClientReceive = new Thread(new ThreadStart(ListenForData));
			m_ThrdClientReceive.IsBackground = true;
			m_ThrdClientReceive.Start();
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
	}

	void ListenForData()
	{
		try
		{
			m_Client = new TcpClient(m_Ip, m_Port);

			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (NetworkStream stream = m_Client.GetStream())
				{
					int length;

					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						DefaultPacket packet = (DefaultPacket)DefaultPacket.Deserialize(bytes);

						switch (packet.packet_Type)
						{
							case (int)PacketType.PlayerPos:
								Playertransform pt = (Playertransform)DefaultPacket.Deserialize(bytes);
								Debug.Log(pt.Px + ":" + pt.Py + ":" + pt.Pz);
								break;
						}
					}
				}
			}

		}

		catch (SocketException ex)
		{
			Debug.Log(ex);
		}
	}

	public void PacketTransfer(byte[] clientMessageAsByteArray)
	{
		if (m_Client == null)
		{
			return;
		}
		try
		{
			NetworkStream stream = m_Client.GetStream();

			if (stream.CanWrite)
			{
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
			}
		}
		catch (SocketException ex)
		{
			Debug.Log(ex);
		}
	}
}

