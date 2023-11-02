using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon
{
	public abstract class IPhotonSocket
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3
		{
			public IPhotonSocket _003C_003E4__this;

			public byte[] inBuffer;

			public int length;

			public void _003CHandleReceivedDatagram_003Eb__1()
			{
				_003C_003E4__this.peerBase.ReceiveIncomingCommands(inBuffer, length);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5
		{
			public _003C_003Ec__DisplayClass3 CS_0024_003C_003E8__locals4;

			public byte[] inBufferCopy;

			public void _003CHandleReceivedDatagram_003Eb__0()
			{
				CS_0024_003C_003E8__locals4._003C_003E4__this.peerBase.ReceiveIncomingCommands(inBufferCopy, CS_0024_003C_003E8__locals4.length);
			}
		}

		internal PeerBase peerBase;

		public bool PollReceive;

		protected IPhotonPeerListener Listener
		{
			get
			{
				return peerBase.Listener;
			}
		}

		public ConnectionProtocol Protocol { get; protected set; }

		public PhotonSocketState State { get; protected set; }

		public string ServerAddress { get; protected set; }

		public int ServerPort { get; protected set; }

		public bool Connected
		{
			get
			{
				return State == PhotonSocketState.Connected;
			}
		}

		public int MTU
		{
			get
			{
				return peerBase.mtu;
			}
		}

		public IPhotonSocket(PeerBase peerBase)
		{
			if (peerBase == null)
			{
				throw new Exception("Can't init without peer");
			}
			this.peerBase = peerBase;
		}

		public virtual bool Connect()
		{
			if (State != 0)
			{
				if ((int)peerBase.debugOut >= 1)
				{
					peerBase.Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: connection in State: " + State);
				}
				return false;
			}
			if (peerBase == null || Protocol != peerBase.usedProtocol)
			{
				return false;
			}
			string address;
			short port;
			if (!TryParseAddress(peerBase.ServerAddress, out address, out port))
			{
				if ((int)peerBase.debugOut >= 1)
				{
					peerBase.Listener.DebugReturn(DebugLevel.ERROR, "Failed parsing address: " + peerBase.ServerAddress);
				}
				return false;
			}
			ServerAddress = address;
			ServerPort = port;
			return true;
		}

		public abstract bool Disconnect();

		public abstract PhotonSocketError Send(byte[] data, int length);

		public abstract PhotonSocketError Receive(out byte[] data);

		public void HandleReceivedDatagram(byte[] inBuffer, int length, bool willBeReused)
		{
			PeerBase.MyAction myAction = null;
			_003C_003Ec__DisplayClass3 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass3();
			_003C_003Ec__DisplayClass.inBuffer = inBuffer;
			_003C_003Ec__DisplayClass.length = length;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (peerBase.NetworkSimulationSettings.IsSimulationEnabled)
			{
				if (willBeReused)
				{
					_003C_003Ec__DisplayClass5 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass5();
					_003C_003Ec__DisplayClass2.CS_0024_003C_003E8__locals4 = _003C_003Ec__DisplayClass;
					_003C_003Ec__DisplayClass2.inBufferCopy = new byte[_003C_003Ec__DisplayClass.length];
					Buffer.BlockCopy(_003C_003Ec__DisplayClass.inBuffer, 0, _003C_003Ec__DisplayClass2.inBufferCopy, 0, _003C_003Ec__DisplayClass.length);
					peerBase.ReceiveNetworkSimulated(_003C_003Ec__DisplayClass2._003CHandleReceivedDatagram_003Eb__0);
				}
				else
				{
					PeerBase obj = peerBase;
					if (myAction == null)
					{
						myAction = _003C_003Ec__DisplayClass._003CHandleReceivedDatagram_003Eb__1;
					}
					obj.ReceiveNetworkSimulated(myAction);
				}
			}
			else
			{
				peerBase.ReceiveIncomingCommands(_003C_003Ec__DisplayClass.inBuffer, _003C_003Ec__DisplayClass.length);
			}
		}

		public bool ReportDebugOfLevel(DebugLevel levelOfMessage)
		{
			return (int)peerBase.debugOut >= (int)levelOfMessage;
		}

		public void EnqueueDebugReturn(DebugLevel debugLevel, string message)
		{
			peerBase.EnqueueDebugReturn(debugLevel, message);
		}

		protected internal void HandleException(StatusCode statusCode)
		{
			State = PhotonSocketState.Disconnecting;
			peerBase.EnqueueStatusCallback(statusCode);
			peerBase.EnqueueActionForDispatch(_003CHandleException_003Eb__7);
		}

		protected internal bool TryParseAddress(string addressAndPort, out string address, out short port)
		{
			address = string.Empty;
			port = 0;
			if (string.IsNullOrEmpty(addressAndPort))
			{
				return false;
			}
			string[] array = addressAndPort.Split(':');
			if (array.Length != 2)
			{
				return false;
			}
			address = array[0];
			return short.TryParse(array[1], out port);
		}

		protected internal static IPAddress GetIpAddress(string serverIp)
		{
			IPAddress address = null;
			if (IPAddress.TryParse(serverIp, out address))
			{
				return address;
			}
			IPHostEntry hostEntry = Dns.GetHostEntry(serverIp);
			IPAddress[] addressList = hostEntry.AddressList;
			IPAddress[] array = addressList;
			foreach (IPAddress iPAddress in array)
			{
				if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					return iPAddress;
				}
			}
			return null;
		}

		[CompilerGenerated]
		private void _003CHandleException_003Eb__7()
		{
			peerBase.Disconnect();
		}
	}
}
