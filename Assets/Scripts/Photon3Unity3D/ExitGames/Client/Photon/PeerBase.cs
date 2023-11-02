using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Photon.SocketServer.Security;

namespace ExitGames.Client.Photon
{
	public abstract class PeerBase
	{
		internal delegate void MyAction();

		public enum ConnectionStateValue : byte
		{
			Disconnected = 0,
			Connecting = 1,
			Connected = 3,
			Disconnecting = 4,
			AcknowledgingDisconnect = 5,
			Zombie = 6
		}

		internal enum EgMessageType : byte
		{
			Init = 0,
			InitResponse = 1,
			Operation = 2,
			OperationResponse = 3,
			Event = 4,
			InternalOperationRequest = 6,
			InternalOperationResponse = 7,
			Message = 8,
			RawMessage = 9
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2
		{
			public PeerBase _003C_003E4__this;

			public DebugLevel level;

			public string debugReturn;

			public void _003CEnqueueDebugReturn_003Eb__0()
			{
				_003C_003E4__this.Listener.DebugReturn(level, debugReturn);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public PeerBase _003C_003E4__this;

			public StatusCode statusValue;

			public void _003CEnqueueStatusCallback_003Eb__4()
			{
				_003C_003E4__this.Listener.OnStatusChanged(statusValue);
			}
		}

		public const string ClientVersion = "3.2.2.2";

		internal const int ENET_PEER_PACKET_LOSS_SCALE = 65536;

		internal const int ENET_PEER_DEFAULT_ROUND_TRIP_TIME = 300;

		internal const int ENET_PEER_PACKET_THROTTLE_INTERVAL = 5000;

		protected internal Type SocketImplementation = null;

		internal IPhotonSocket rt;

		public int ByteCountLastOperation;

		public int ByteCountCurrentDispatch;

		internal int TrafficPackageHeaderSize;

		public TrafficStats TrafficStatsIncoming;

		public TrafficStats TrafficStatsOutgoing;

		public TrafficStatsGameLevel TrafficStatsGameLevel;

		private Stopwatch trafficStatsStopwatch;

		private bool trafficStatsEnabled = false;

		internal ConnectionProtocol usedProtocol;

		internal bool crcEnabled = false;

		internal int packetLossByCrc;

		internal DebugLevel debugOut = DebugLevel.ERROR;

		internal readonly Queue<MyAction> ActionQueue = new Queue<MyAction>();

		internal short peerID = -1;

		internal ConnectionStateValue peerConnectionState;

		internal int serverTimeOffset;

		internal bool serverTimeOffsetIsAvailable;

		internal int roundTripTime;

		internal int roundTripTimeVariance;

		internal int lastRoundTripTime;

		internal int lowestRoundTripTime;

		internal int lastRoundTripTimeVariance;

		internal int highestRoundTripTimeVariance;

		internal int timestampOfLastReceive;

		internal int packetThrottleInterval;

		internal static short peerCount;

		internal long bytesOut;

		internal long bytesIn;

		internal int commandBufferSize = 100;

		internal int warningSize = 100;

		internal int sentCountAllowance = 5;

		internal int DisconnectTimeout = 10000;

		internal int timePingInterval = 1000;

		internal byte ChannelCount = 2;

		internal int limitOfUnreliableCommands = 0;

		public DiffieHellmanCryptoProvider CryptoProvider;

		private readonly Random lagRandomizer = new Random();

		internal readonly LinkedList<SimulationItem> NetSimListOutgoing = new LinkedList<SimulationItem>();

		internal readonly LinkedList<SimulationItem> NetSimListIncoming = new LinkedList<SimulationItem>();

		private readonly NetworkSimulationSet networkSimulationSettings = new NetworkSimulationSet();

		internal byte[] INIT_BYTES = new byte[41];

		internal int timeBase;

		internal int timeInt;

		internal int timeoutInt;

		internal int timeLastAckReceive;

		internal bool ApplicationIsInitialized;

		internal bool isEncryptionAvailable;

		internal static int outgoingStreamBufferSize = 1200;

		internal int outgoingCommandsInStream = 0;

		internal int mtu = 1200;

		protected MemoryStream SerializeMemStream = new MemoryStream();

		public long TrafficStatsEnabledTime
		{
			get
			{
				return (trafficStatsStopwatch != null) ? trafficStatsStopwatch.ElapsedMilliseconds : 0;
			}
		}

		public bool TrafficStatsEnabled
		{
			get
			{
				return trafficStatsEnabled;
			}
			set
			{
				trafficStatsEnabled = value;
				if (value)
				{
					if (trafficStatsStopwatch == null)
					{
						InitializeTrafficStats();
					}
					trafficStatsStopwatch.Start();
				}
				else
				{
					trafficStatsStopwatch.Stop();
				}
			}
		}

		internal string ServerAddress { get; set; }

		internal string HttpUrlParameters { get; set; }

		internal IPhotonPeerListener Listener { get; set; }

		public NetworkSimulationSet NetworkSimulationSettings
		{
			get
			{
				return networkSimulationSettings;
			}
		}

		internal long BytesOut
		{
			get
			{
				return bytesOut;
			}
		}

		internal long BytesIn
		{
			get
			{
				return bytesIn;
			}
		}

		internal abstract int QueuedIncomingCommandsCount { get; }

		internal abstract int QueuedOutgoingCommandsCount { get; }

		public virtual string PeerID
		{
			get
			{
				return peerID.ToString();
			}
		}

		protected internal byte[] TcpConnectionPrefix { get; set; }

		internal bool IsSendingOnlyAcks { get; set; }

		internal void InitOnce()
		{
			networkSimulationSettings.peerBase = this;
			INIT_BYTES[0] = 243;
			INIT_BYTES[1] = 0;
			INIT_BYTES[2] = 1;
			INIT_BYTES[3] = 6;
			INIT_BYTES[4] = 1;
			INIT_BYTES[5] = 3;
			INIT_BYTES[6] = 2;
			INIT_BYTES[7] = 1;
			INIT_BYTES[8] = 7;
		}

		internal abstract bool Connect(string serverAddress, string appID);

		private string GetHttpKeyValueString(Dictionary<string, string> dic)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> item in dic)
			{
				stringBuilder.Append(item.Key).Append("=").Append(item.Value)
					.Append("&");
			}
			return stringBuilder.ToString();
		}

		internal abstract void Disconnect();

		internal abstract void StopConnection();

		internal abstract void FetchServerTimestamp();

		internal bool EnqueueOperation(Dictionary<byte, object> parameters, byte opCode, bool sendReliable, byte channelId, bool encrypted)
		{
			return EnqueueOperation(parameters, opCode, sendReliable, channelId, encrypted, EgMessageType.Operation);
		}

		internal abstract bool EnqueueOperation(Dictionary<byte, object> parameters, byte opCode, bool sendReliable, byte channelId, bool encrypted, EgMessageType messageType);

		internal abstract bool DispatchIncomingCommands();

		internal abstract bool SendOutgoingCommands();

		internal virtual bool SendAcksOnly()
		{
			return false;
		}

		internal byte[] SerializeMessageToMessage(object message, bool encrypt, byte[] messageHeader, bool writeLength = true)
		{
			byte[] array;
			lock (SerializeMemStream)
			{
				SerializeMemStream.Position = 0L;
				SerializeMemStream.SetLength(0L);
				if (!encrypt)
				{
					SerializeMemStream.Write(messageHeader, 0, messageHeader.Length);
				}
				Protocol.SerializeMessage(SerializeMemStream, message);
				if (encrypt)
				{
					byte[] data = SerializeMemStream.ToArray();
					data = CryptoProvider.Encrypt(data);
					SerializeMemStream.Position = 0L;
					SerializeMemStream.SetLength(0L);
					SerializeMemStream.Write(messageHeader, 0, messageHeader.Length);
					SerializeMemStream.Write(data, 0, data.Length);
				}
				array = SerializeMemStream.ToArray();
			}
			array[messageHeader.Length - 1] = 8;
			if (encrypt)
			{
				array[messageHeader.Length - 1] = (byte)(array[messageHeader.Length - 1] | 0x80u);
			}
			if (writeLength)
			{
				int targetOffset = 1;
				Protocol.Serialize(array.Length, array, ref targetOffset);
			}
			return array;
		}

		internal byte[] SerializeRawMessageToMessage(byte[] data, bool encrypt, byte[] messageHeader, bool writeLength = true)
		{
			byte[] array;
			lock (SerializeMemStream)
			{
				SerializeMemStream.Position = 0L;
				SerializeMemStream.SetLength(0L);
				if (!encrypt)
				{
					SerializeMemStream.Write(messageHeader, 0, messageHeader.Length);
				}
				SerializeMemStream.Write(data, 0, data.Length);
				if (encrypt)
				{
					byte[] data2 = SerializeMemStream.ToArray();
					data2 = CryptoProvider.Encrypt(data2);
					SerializeMemStream.Position = 0L;
					SerializeMemStream.SetLength(0L);
					SerializeMemStream.Write(messageHeader, 0, messageHeader.Length);
					SerializeMemStream.Write(data2, 0, data2.Length);
				}
				array = SerializeMemStream.ToArray();
			}
			array[messageHeader.Length - 1] = 9;
			if (encrypt)
			{
				array[messageHeader.Length - 1] = (byte)(array[messageHeader.Length - 1] | 0x80u);
			}
			if (writeLength)
			{
				int targetOffset = 1;
				Protocol.Serialize(array.Length, array, ref targetOffset);
			}
			return array;
		}

		internal abstract byte[] SerializeOperationToMessage(byte opCode, Dictionary<byte, object> parameters, EgMessageType messageType, bool encrypt);

		internal abstract void ReceiveIncomingCommands(byte[] inBuff, int dataLength);

		internal void InitCallback()
		{
			if (peerConnectionState == ConnectionStateValue.Connecting)
			{
				peerConnectionState = ConnectionStateValue.Connected;
			}
			ApplicationIsInitialized = true;
			FetchServerTimestamp();
			Listener.OnStatusChanged(StatusCode.Connect);
		}

		internal bool ExchangeKeysForEncryption()
		{
			isEncryptionAvailable = false;
			CryptoProvider = new DiffieHellmanCryptoProvider();
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>(1);
			dictionary[PhotonCodes.ClientKey] = CryptoProvider.PublicKey;
			return EnqueueOperation(dictionary, PhotonCodes.InitEncryption, true, 0, false, EgMessageType.InternalOperationRequest);
		}

		internal void DeriveSharedKey(OperationResponse operationResponse)
		{
			if (operationResponse.ReturnCode != 0)
			{
				EnqueueDebugReturn(DebugLevel.ERROR, "Establishing encryption keys failed. " + operationResponse.ToStringFull());
				EnqueueStatusCallback(StatusCode.EncryptionFailedToEstablish);
				return;
			}
			byte[] array = (byte[])operationResponse[PhotonCodes.ServerKey];
			if (array == null || array.Length == 0)
			{
				EnqueueDebugReturn(DebugLevel.ERROR, "Establishing encryption keys failed. Server's public key is null or empty. " + operationResponse.ToStringFull());
				EnqueueStatusCallback(StatusCode.EncryptionFailedToEstablish);
			}
			else
			{
				CryptoProvider.DeriveSharedKey(array);
				isEncryptionAvailable = true;
				EnqueueStatusCallback(StatusCode.EncryptionEstablished);
			}
		}

		internal void EnqueueActionForDispatch(MyAction action)
		{
			lock (ActionQueue)
			{
				ActionQueue.Enqueue(action);
			}
		}

		internal void EnqueueDebugReturn(DebugLevel level, string debugReturn)
		{
			MyAction myAction = null;
			_003C_003Ec__DisplayClass2 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass2();
			_003C_003Ec__DisplayClass.level = level;
			_003C_003Ec__DisplayClass.debugReturn = debugReturn;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			lock (ActionQueue)
			{
				Queue<MyAction> actionQueue = ActionQueue;
				if (myAction == null)
				{
					myAction = _003C_003Ec__DisplayClass._003CEnqueueDebugReturn_003Eb__0;
				}
				actionQueue.Enqueue(myAction);
			}
		}

		internal void EnqueueStatusCallback(StatusCode statusValue)
		{
			MyAction myAction = null;
			_003C_003Ec__DisplayClass6 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass6();
			_003C_003Ec__DisplayClass.statusValue = statusValue;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			lock (ActionQueue)
			{
				Queue<MyAction> actionQueue = ActionQueue;
				if (myAction == null)
				{
					myAction = _003C_003Ec__DisplayClass._003CEnqueueStatusCallback_003Eb__4;
				}
				actionQueue.Enqueue(myAction);
			}
		}

		internal virtual void InitPeerBase()
		{
			TrafficStatsIncoming = new TrafficStats(TrafficPackageHeaderSize);
			TrafficStatsOutgoing = new TrafficStats(TrafficPackageHeaderSize);
			TrafficStatsGameLevel = new TrafficStatsGameLevel();
			ByteCountLastOperation = 0;
			ByteCountCurrentDispatch = 0;
			bytesIn = 0L;
			bytesOut = 0L;
			packetLossByCrc = 0;
			networkSimulationSettings.LostPackagesIn = 0;
			networkSimulationSettings.LostPackagesOut = 0;
			lock (NetSimListOutgoing)
			{
				NetSimListOutgoing.Clear();
			}
			lock (NetSimListIncoming)
			{
				NetSimListIncoming.Clear();
			}
			peerConnectionState = ConnectionStateValue.Disconnected;
			timeBase = SupportClass.GetTickCount();
			isEncryptionAvailable = false;
			ApplicationIsInitialized = false;
			roundTripTime = 300;
			roundTripTimeVariance = 0;
			packetThrottleInterval = 5000;
			serverTimeOffsetIsAvailable = false;
			serverTimeOffset = 0;
		}

		internal virtual bool DeserializeMessageAndCallback(byte[] inBuff)
		{
			if (inBuff.Length < 2)
			{
				if ((int)debugOut >= 1)
				{
					Listener.DebugReturn(DebugLevel.ERROR, "Incoming UDP data too short! " + inBuff.Length);
				}
				return false;
			}
			if (inBuff[0] != 243 && inBuff[0] != 253)
			{
				if ((int)debugOut >= 1)
				{
					Listener.DebugReturn(DebugLevel.ALL, "No regular operation UDP message: " + inBuff[0]);
				}
				return false;
			}
			byte b = (byte)(inBuff[1] & 0x7Fu);
			bool flag = (inBuff[1] & 0x80) > 0;
			MemoryStream memoryStream = null;
			if (b != 1)
			{
				try
				{
					if (flag)
					{
						inBuff = CryptoProvider.Decrypt(inBuff, 2, inBuff.Length - 2);
						memoryStream = new MemoryStream(inBuff);
					}
					else
					{
						memoryStream = new MemoryStream(inBuff);
						memoryStream.Seek(2L, SeekOrigin.Begin);
					}
				}
				catch (Exception ex)
				{
					if ((int)debugOut >= 1)
					{
						Listener.DebugReturn(DebugLevel.ERROR, ex.ToString());
					}
					SupportClass.WriteStackTrace(ex);
					return false;
				}
			}
			int num = 0;
			switch (b)
			{
			case 3:
			{
				OperationResponse operationResponse = Protocol.DeserializeOperationResponse(memoryStream);
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.CountResult(ByteCountCurrentDispatch);
					num = SupportClass.GetTickCount();
				}
				Listener.OnOperationResponse(operationResponse);
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.TimeForResponseCallback(operationResponse.OperationCode, SupportClass.GetTickCount() - num);
				}
				break;
			}
			case 4:
			{
				EventData eventData = Protocol.DeserializeEventData(memoryStream);
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.CountEvent(ByteCountCurrentDispatch);
					num = SupportClass.GetTickCount();
				}
				Listener.OnEvent(eventData);
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.TimeForEventCallback(eventData.Code, SupportClass.GetTickCount() - num);
				}
				break;
			}
			case 1:
				InitCallback();
				break;
			case 7:
			{
				OperationResponse operationResponse = Protocol.DeserializeOperationResponse(memoryStream);
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.CountResult(ByteCountCurrentDispatch);
					num = SupportClass.GetTickCount();
				}
				if (operationResponse.OperationCode == PhotonCodes.InitEncryption)
				{
					DeriveSharedKey(operationResponse);
				}
				else
				{
					EnqueueDebugReturn(DebugLevel.ERROR, "Received unknown internal operation. " + operationResponse.ToStringFull());
				}
				if (TrafficStatsEnabled)
				{
					TrafficStatsGameLevel.TimeForResponseCallback(operationResponse.OperationCode, SupportClass.GetTickCount() - num);
				}
				break;
			}
			default:
				EnqueueDebugReturn(DebugLevel.ERROR, "unexpected msgType " + b);
				break;
			}
			return true;
		}

		internal void SendNetworkSimulated(MyAction sendAction)
		{
			if (!NetworkSimulationSettings.IsSimulationEnabled)
			{
				sendAction();
				return;
			}
			if (usedProtocol == ConnectionProtocol.Udp && NetworkSimulationSettings.OutgoingLossPercentage > 0 && lagRandomizer.Next(101) < NetworkSimulationSettings.OutgoingLossPercentage)
			{
				networkSimulationSettings.LostPackagesOut++;
				return;
			}
			int num = ((networkSimulationSettings.OutgoingJitter > 0) ? (lagRandomizer.Next(networkSimulationSettings.OutgoingJitter * 2) - networkSimulationSettings.OutgoingJitter) : 0);
			int num2 = networkSimulationSettings.OutgoingLag + num;
			int num3 = SupportClass.GetTickCount() + num2;
			SimulationItem simulationItem = new SimulationItem();
			simulationItem.ActionToExecute = sendAction;
			simulationItem.TimeToExecute = num3;
			simulationItem.Delay = num2;
			SimulationItem value = simulationItem;
			lock (NetSimListOutgoing)
			{
				if (NetSimListOutgoing.Count == 0 || usedProtocol == ConnectionProtocol.Tcp)
				{
					NetSimListOutgoing.AddLast(value);
					return;
				}
				LinkedListNode<SimulationItem> linkedListNode = NetSimListOutgoing.First;
				while (linkedListNode != null && linkedListNode.Value.TimeToExecute < num3)
				{
					linkedListNode = linkedListNode.Next;
				}
				if (linkedListNode == null)
				{
					NetSimListOutgoing.AddLast(value);
				}
				else
				{
					NetSimListOutgoing.AddBefore(linkedListNode, value);
				}
			}
		}

		internal void ReceiveNetworkSimulated(MyAction receiveAction)
		{
			if (!networkSimulationSettings.IsSimulationEnabled)
			{
				receiveAction();
				return;
			}
			if (usedProtocol == ConnectionProtocol.Udp && networkSimulationSettings.IncomingLossPercentage > 0 && lagRandomizer.Next(101) < networkSimulationSettings.IncomingLossPercentage)
			{
				networkSimulationSettings.LostPackagesIn++;
				return;
			}
			int num = ((networkSimulationSettings.IncomingJitter > 0) ? (lagRandomizer.Next(networkSimulationSettings.IncomingJitter * 2) - networkSimulationSettings.IncomingJitter) : 0);
			int num2 = networkSimulationSettings.IncomingLag + num;
			int num3 = SupportClass.GetTickCount() + num2;
			SimulationItem simulationItem = new SimulationItem();
			simulationItem.ActionToExecute = receiveAction;
			simulationItem.TimeToExecute = num3;
			simulationItem.Delay = num2;
			SimulationItem value = simulationItem;
			lock (NetSimListIncoming)
			{
				if (NetSimListIncoming.Count == 0 || usedProtocol == ConnectionProtocol.Tcp)
				{
					NetSimListIncoming.AddLast(value);
					return;
				}
				LinkedListNode<SimulationItem> linkedListNode = NetSimListIncoming.First;
				while (linkedListNode != null && linkedListNode.Value.TimeToExecute < num3)
				{
					linkedListNode = linkedListNode.Next;
				}
				if (linkedListNode == null)
				{
					NetSimListIncoming.AddLast(value);
				}
				else
				{
					NetSimListIncoming.AddBefore(linkedListNode, value);
				}
			}
		}

		protected internal void NetworkSimRun()
		{
			while (true)
			{
				bool flag = true;
				bool flag2 = false;
				lock (networkSimulationSettings.NetSimManualResetEvent)
				{
					flag2 = networkSimulationSettings.IsSimulationEnabled;
				}
				if (!flag2)
				{
					networkSimulationSettings.NetSimManualResetEvent.WaitOne();
					continue;
				}
				lock (NetSimListIncoming)
				{
					SimulationItem simulationItem = null;
					while (NetSimListIncoming.First != null)
					{
						simulationItem = NetSimListIncoming.First.Value;
						if (simulationItem.stopw.ElapsedMilliseconds < simulationItem.Delay)
						{
							break;
						}
						simulationItem.ActionToExecute();
						NetSimListIncoming.RemoveFirst();
					}
				}
				lock (NetSimListOutgoing)
				{
					SimulationItem simulationItem = null;
					while (NetSimListOutgoing.First != null)
					{
						simulationItem = NetSimListOutgoing.First.Value;
						if (simulationItem.stopw.ElapsedMilliseconds < simulationItem.Delay)
						{
							break;
						}
						simulationItem.ActionToExecute();
						NetSimListOutgoing.RemoveFirst();
					}
				}
				Thread.Sleep(0);
			}
		}

		internal void UpdateRoundTripTimeAndVariance(int lastRoundtripTime)
		{
			if (lastRoundtripTime >= 0)
			{
				roundTripTimeVariance -= roundTripTimeVariance / 4;
				if (lastRoundtripTime >= roundTripTime)
				{
					roundTripTime += (lastRoundtripTime - roundTripTime) / 8;
					roundTripTimeVariance += (lastRoundtripTime - roundTripTime) / 4;
				}
				else
				{
					roundTripTime += (lastRoundtripTime - roundTripTime) / 8;
					roundTripTimeVariance -= (lastRoundtripTime - roundTripTime) / 4;
				}
				if (roundTripTime < lowestRoundTripTime)
				{
					lowestRoundTripTime = roundTripTime;
				}
				if (roundTripTimeVariance > highestRoundTripTimeVariance)
				{
					highestRoundTripTimeVariance = roundTripTimeVariance;
				}
			}
		}

		internal void InitializeTrafficStats()
		{
			TrafficStatsIncoming = new TrafficStats(TrafficPackageHeaderSize);
			TrafficStatsOutgoing = new TrafficStats(TrafficPackageHeaderSize);
			TrafficStatsGameLevel = new TrafficStatsGameLevel();
			trafficStatsStopwatch = new Stopwatch();
		}
	}
}
