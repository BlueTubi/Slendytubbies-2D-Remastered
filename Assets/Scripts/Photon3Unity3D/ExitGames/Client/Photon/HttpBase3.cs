#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace ExitGames.Client.Photon
{
	internal class HttpBase3 : PeerBase
	{
		public delegate int GetLocalMsTimestampDelegate();

		internal enum MessageType
		{
			CONNECT = 0,
			DISCONNECT = 1,
			NORMAL = 2
		}

		private class AsyncRequestState
		{
			private Stopwatch Watch;

			public MessageType type;

			public int rerequested = 0;

			public bool restarting = false;

			public int id = 0;

			private int ElapsedTime
			{
				get
				{
					return (int)Watch.ElapsedMilliseconds;
				}
			}

			public WWW Request { get; set; }

			public byte[] OutgoingData { get; set; }

			public bool IsDisconnect
			{
				get
				{
					return type == MessageType.DISCONNECT;
				}
			}

			public bool IsTimedOut
			{
				get
				{
					return ElapsedTime > 10000;
				}
			}

			public AsyncRequestState()
			{
				Watch = new Stopwatch();
				Watch.Start();
			}

			public void Abort()
			{
				Request.Dispose();
				Request = null;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7
		{
			public BinaryReader br;

			public HttpBase3 _003C_003E4__this;

			public byte[] inBuff;

			public void _003CReceiveIncomingCommands_003Eb__6()
			{
				_003C_003E4__this._parseMessage(inBuff, br);
			}
		}

		internal const int TCP_HEADER_LEN = 7;

		private const int MAX_GROSS_ERRORS = 3;

		private static readonly byte[] pingData = new byte[5] { 240, 0, 0, 0, 0 };

		private string HttpPeerID;

		private int _challengId = 0;

		private string UrlParameters;

		private long lastPingTimeStamp;

		private List<byte[]> incomingList = new List<byte[]>();

		private int _lastSendAck = 0;

		private bool _sendAck = false;

		private InvocationCache _invocationCache = new InvocationCache();

		private int _minConnectionsCount = 2;

		private int _maxConnectionsCount = 4;

		private static System.Random _rnd = new System.Random();

		private LinkedList<AsyncRequestState> _requestCache = new LinkedList<AsyncRequestState>();

		private int _stateIdBase = -1;

		private MemoryStream outgoingStream = null;

		private int _grossErrorCount = 0;

		internal GetLocalMsTimestampDelegate GetLocalMsTimestamp;

		internal static readonly byte[] messageHeader = new byte[9] { 251, 0, 0, 0, 0, 0, 0, 243, 2 };

		[CompilerGenerated]
		private static GetLocalMsTimestampDelegate CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;

		[CompilerGenerated]
		private static Func<AsyncRequestState, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;

		public override string PeerID
		{
			get
			{
				return HttpPeerID;
			}
		}

		public bool UseGet { get; set; }

		public int currentRequestCount
		{
			get
			{
				return _requestCache.Count;
			}
		}

		public int totalRequestCount
		{
			get
			{
				return _stateIdBase;
			}
		}

		internal override int QueuedIncomingCommandsCount
		{
			get
			{
				return 0;
			}
		}

		internal override int QueuedOutgoingCommandsCount
		{
			get
			{
				return 0;
			}
		}

		internal HttpBase3()
		{
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 = _003C_002Ector_003Eb__0;
			}
			GetLocalMsTimestamp = CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;
			PeerBase.peerCount++;
			_challengId = _rnd.Next();
			InitOnce();
		}

		internal HttpBase3(IPhotonPeerListener listener)
			: this()
		{
			base.Listener = listener;
		}

		internal void Request(byte[] data, string urlParamter)
		{
			Request(data, urlParamter, MessageType.NORMAL);
		}

		internal void Request(byte[] data, string urlParameter, MessageType type)
		{
			int id = Interlocked.Increment(ref _stateIdBase);
			_addAckId(ref urlParameter);
			AsyncRequestState asyncRequestState = new AsyncRequestState();
			asyncRequestState.OutgoingData = data;
			asyncRequestState.type = type;
			asyncRequestState.id = id;
			AsyncRequestState asyncRequestState2 = asyncRequestState;
			_requestCache.AddLast(asyncRequestState2);
			Request(asyncRequestState2, urlParameter);
		}

		private void Request(AsyncRequestState state, string urlParamter)
		{
			urlParamter = ((!UseGet) ? (urlParamter + string.Format("&seq={0}", state.id)) : (urlParamter + string.Format("&seq={0}&data={1}", state.id, Convert.ToBase64String(state.OutgoingData, Base64FormattingOptions.None))));
			if (state.type == MessageType.DISCONNECT)
			{
				urlParamter += "&dis";
			}
			if ((int)debugOut >= 5)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, string.Format("url for request is {0}", urlParamter));
			}
			string url = base.ServerAddress + urlParamter + base.HttpUrlParameters;
			if (UseGet)
			{
				state.Request = new WWW(url);
			}
			else
			{
				state.Request = new WWW(url, GetPOSTRequestData(state));
			}
		}

		private void _addAckId(ref string urlParamter)
		{
			if (_sendAck)
			{
				_sendAck = false;
				urlParamter = urlParamter + "&ack=" + _lastSendAck;
				if ((int)debugOut >= 5)
				{
					base.Listener.DebugReturn(DebugLevel.ALL, string.Format("ack sent for id {0}, pid={1}, cid={2}", _lastSendAck, HttpPeerID, _challengId));
				}
			}
		}

		private static void writeLength(byte[] target, int value, int targetOffset)
		{
			target[targetOffset++] = (byte)(value >> 24);
			target[targetOffset++] = (byte)(value >> 16);
			target[targetOffset++] = (byte)(value >> 8);
			target[targetOffset++] = (byte)value;
		}

		private byte[] GetPOSTRequestData(AsyncRequestState state)
		{
			return state.OutgoingData;
		}

		private static int _getStatusCodeFromResponse(byte[] response, HttpBase3 peer)
		{
			int num = 0;
			if (response.Length >= 4)
			{
				num |= response[0] << 24;
				num |= response[1] << 16;
				num |= response[2] << 8;
				num |= response[3];
			}
			return num;
		}

		private void _webExceptionHandler(AsyncRequestState state, WWW request)
		{
			if (state.IsDisconnect)
			{
				return;
			}
			if ((peerConnectionState != ConnectionStateValue.Disconnecting || peerConnectionState != 0) && !state.restarting && (int)debugOut >= 1)
			{
				base.Listener.DebugReturn(DebugLevel.ERROR, string.Format("Request {0} for pid={1} cid={2} failed with error: {3}", state.id, HttpPeerID, _challengId, request.error));
			}
			if (peerConnectionState == ConnectionStateValue.Connecting)
			{
				EnqueueErrorDisconnect(StatusCode.ExceptionOnConnect);
			}
			else
			{
				if (peerConnectionState != ConnectionStateValue.Connected)
				{
					return;
				}
				int responseStatus = getResponseStatus(request);
				if (gotIgnoreStatus(responseStatus))
				{
					if ((int)debugOut >= 5)
					{
						base.Listener.DebugReturn(DebugLevel.ALL, "got statues which we ignore");
					}
				}
				else
				{
					EnqueueErrorDisconnect(StatusCode.DisconnectByServer);
				}
			}
		}

		private static bool gotIgnoreStatus(int responseStatus)
		{
			return responseStatus == 404;
		}

		private int getResponseStatus(WWW request)
		{
			int result = -1;
			if (request.error != null)
			{
				int num = request.error.IndexOf(' ');
				if (num != -1)
				{
					string text = request.error.Substring(0, num + 1);
					text.Trim();
					int.TryParse(text, out result);
				}
				return result;
			}
			base.Listener.DebugReturn(DebugLevel.WARNING, string.Format("failed to parse status {0}", result));
			return -1;
		}

		private void GetResponse(AsyncRequestState state)
		{
			WWW request = state.Request;
			if (request.error != null)
			{
				_webExceptionHandler(state, request);
				return;
			}
			try
			{
				if (request.bytes != null && request.bytes.Length > 0)
				{
					ReceiveIncomingCommands(request.bytes, request.bytes.Length);
				}
				Interlocked.Exchange(ref _grossErrorCount, 0);
			}
			catch (Exception ex)
			{
				base.Listener.DebugReturn(DebugLevel.ERROR, string.Format("exception: msg - {0}\n Stack\n{1}", ex.StackTrace));
				return;
			}
			_checkAckCondition();
			state.Request.Dispose();
			state.OutgoingData = null;
		}

		private void _checkAckCondition()
		{
			if (_requestCache.Count != 0 && _stateIdBase - _maxConnectionsCount - _lastSendAck > 5)
			{
				LinkedList<AsyncRequestState> requestCache = _requestCache;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 = _003C_checkAckCondition_003Eb__3;
				}
				int num = requestCache.Min(CS_0024_003C_003E9__CachedAnonymousMethodDelegate4);
				if (num != int.MaxValue && num - _lastSendAck > 5)
				{
					_lastSendAck = num - 2;
					_sendAck = true;
				}
			}
		}

		private void CancelRequests()
		{
			foreach (AsyncRequestState item in _requestCache)
			{
				item.Abort();
			}
			_requestCache.Clear();
		}

		private void Reset()
		{
			_lastSendAck = 0;
			_stateIdBase = -1;
			_sendAck = false;
			_invocationCache.Reset();
			HttpPeerID = "";
			_requestCache.Clear();
		}

		internal override bool Connect(string serverAddress, string appID)
		{
			if (peerConnectionState != 0)
			{
				if ((int)debugOut >= 2)
				{
					base.Listener.DebugReturn(DebugLevel.WARNING, "Connect() called while peerConnectionState != Disconnected. Nothing done.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(serverAddress) || !serverAddress.StartsWith("http", true, null))
			{
				base.Listener.DebugReturn(DebugLevel.ERROR, "Connect() with RHTTP failed. ServerAddress must include 'http://' or 'https://' prefix. Was: " + serverAddress);
				return false;
			}
			outgoingStream = new MemoryStream(PeerBase.outgoingStreamBufferSize);
			Reset();
			peerConnectionState = ConnectionStateValue.Connecting;
			base.ServerAddress = serverAddress;
			UrlParameters = "?init&cid=";
			UrlParameters += _challengId;
			if (appID == null)
			{
				appID = "NUnit";
			}
			UrlParameters = UrlParameters + "&app=" + appID;
			UrlParameters += "&clientversion=3.2.2.2";
			UrlParameters += "&protocol=GpBinaryV16";
			lastPingTimeStamp = GetLocalMsTimestamp();
			Request(null, UrlParameters, MessageType.CONNECT);
			return true;
		}

		internal override void Disconnect()
		{
			if (peerConnectionState != 0 && peerConnectionState != ConnectionStateValue.Disconnecting)
			{
				peerConnectionState = ConnectionStateValue.Disconnecting;
				CancelRequests();
				Request(null, UrlParameters, MessageType.DISCONNECT);
				Disconnected();
			}
		}

		private void EnqueueErrorDisconnect(StatusCode statusCode)
		{
			lock (this)
			{
				if (peerConnectionState != ConnectionStateValue.Connected && peerConnectionState != ConnectionStateValue.Connecting)
				{
					return;
				}
				peerConnectionState = ConnectionStateValue.Disconnecting;
			}
			if ((int)debugOut >= 3)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, string.Format("pid={0} cid={1} is disconnected", HttpPeerID, _challengId));
			}
			EnqueueStatusCallback(statusCode);
			EnqueueActionForDispatch(_003CEnqueueErrorDisconnect_003Eb__5);
		}

		internal void Disconnected()
		{
			InitPeerBase();
			base.Listener.OnStatusChanged(StatusCode.Disconnect);
		}

		internal override void InitPeerBase()
		{
			peerConnectionState = ConnectionStateValue.Disconnected;
		}

		internal override void FetchServerTimestamp()
		{
		}

		internal override bool DispatchIncomingCommands()
		{
			CheckRequests();
			return DispatchIncommingActions();
		}

		private bool CheckRequests()
		{
			LinkedListNode<AsyncRequestState> linkedListNode = _requestCache.First;
			while (linkedListNode != null)
			{
				LinkedListNode<AsyncRequestState> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Request.isDone)
				{
					GetResponse(linkedListNode2.Value);
					_requestCache.Remove(linkedListNode2);
				}
				else if (linkedListNode2.Value.IsTimedOut)
				{
					AsyncRequestState value = linkedListNode2.Value;
					value.Abort();
					if ((int)debugOut >= 2)
					{
						base.Listener.DebugReturn(DebugLevel.WARNING, string.Format("Request {0} for pid={1} cid={2} aborted by timeout", value.id, HttpPeerID, _challengId));
					}
					EnqueueErrorDisconnect(StatusCode.TimeoutDisconnect);
					_requestCache.Remove(linkedListNode2);
					return true;
				}
			}
			return true;
		}

		private bool DispatchIncommingActions()
		{
			lock (ActionQueue)
			{
				while (ActionQueue.Count > 0)
				{
					MyAction myAction = ActionQueue.Dequeue();
					myAction();
				}
			}
			byte[] array;
			lock (incomingList)
			{
				if (incomingList.Count <= 0)
				{
					return false;
				}
				array = incomingList[0];
				incomingList.RemoveAt(0);
			}
			ByteCountCurrentDispatch = array.Length + 3;
			if (array.Length < 2)
			{
				base.Listener.DebugReturn(DebugLevel.WARNING, string.Format("message has length less then 2. data {0}", SupportClass.ByteArrayToString(array)));
			}
			return DeserializeMessageAndCallback(array);
		}

		private void _sendPing()
		{
			if (GetLocalMsTimestamp() - lastPingTimeStamp > timePingInterval)
			{
				lastPingTimeStamp = GetLocalMsTimestamp();
				int targetOffset = 1;
				Protocol.Serialize(SupportClass.GetTickCount(), pingData, ref targetOffset);
				Request(pingData, UrlParameters, MessageType.NORMAL);
			}
			int num = _minConnectionsCount - _requestCache.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					Request(null, UrlParameters, MessageType.NORMAL);
				}
			}
		}

		internal override bool SendOutgoingCommands()
		{
			if (peerConnectionState != ConnectionStateValue.Connected)
			{
				return false;
			}
			if (_requestCache.Count >= _maxConnectionsCount)
			{
				return false;
			}
			if (outgoingStream.Length > 0)
			{
				Request(outgoingStream.ToArray(), UrlParameters);
				outgoingStream.SetLength(0L);
				outgoingStream.Position = 0L;
			}
			_sendPing();
			return false;
		}

		private static int _readMessageHeader(BinaryReader br)
		{
			int result = (br.ReadByte() << 24) | (br.ReadByte() << 16) | (br.ReadByte() << 8) | br.ReadByte();
			byte b = br.ReadByte();
			byte b2 = br.ReadByte();
			return result;
		}

		internal override void ReceiveIncomingCommands(byte[] inBuff, int dataLen)
		{
			_003C_003Ec__DisplayClass7 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass7();
			_003C_003Ec__DisplayClass.inBuff = inBuff;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (peerConnectionState == ConnectionStateValue.Connecting)
			{
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(_003C_003Ec__DisplayClass.inBuff, 4, 4);
				}
				HttpPeerID = BitConverter.ToInt32(_003C_003Ec__DisplayClass.inBuff, 4).ToString();
				UrlParameters = "?pid=" + HttpPeerID + "&cid=" + _challengId;
				peerConnectionState = ConnectionStateValue.Connected;
				EnqueueActionForDispatch(base.InitCallback);
			}
			else
			{
				timestampOfLastReceive = GetLocalMsTimestamp();
				bytesIn += _003C_003Ec__DisplayClass.inBuff.Length + 7;
				Array.Reverse(_003C_003Ec__DisplayClass.inBuff, 0, 4);
				_003C_003Ec__DisplayClass.br = new BinaryReader(new MemoryStream(_003C_003Ec__DisplayClass.inBuff));
				int invocationId = _003C_003Ec__DisplayClass.br.ReadInt32();
				_invocationCache.Invoke(invocationId, _003C_003Ec__DisplayClass._003CReceiveIncomingCommands_003Eb__6);
			}
		}

		private void _parseMessage(byte[] inBuff, BinaryReader br)
		{
			int num = inBuff.Length;
			using (br)
			{
				using (Stream stream = br.BaseStream)
				{
					while (stream.Position != stream.Length)
					{
						switch (br.ReadByte())
						{
						case 251:
						{
							int num2 = _readMessageHeader(br) - 7;
							if (num2 == -1)
							{
								if ((int)debugOut >= 1)
								{
									base.Listener.DebugReturn(DebugLevel.ERROR, string.Format("Invalid message header for pid={0} cid={1} and message {2}", HttpPeerID, _challengId, SupportClass.ByteArrayToString(inBuff)));
								}
								return;
							}
							System.Diagnostics.Debug.Assert(num2 >= 2);
							byte[] item = br.ReadBytes(num2);
							if (num2 < 2)
							{
								base.Listener.DebugReturn(DebugLevel.WARNING, string.Format("data len is to small. data {0}", SupportClass.ByteArrayToString(inBuff)));
							}
							lock (incomingList)
							{
								incomingList.Add(item);
								if (incomingList.Count % warningSize == 0)
								{
									EnqueueStatusCallback(StatusCode.QueueIncomingReliableWarning);
								}
							}
							break;
						}
						case 240:
							ReadPingResponse(br);
							break;
						default:
							base.Listener.DebugReturn(DebugLevel.WARNING, "Unknow response from server");
							break;
						}
					}
				}
			}
		}

		private void ReadPingResponse(BinaryReader br)
		{
			int num = (br.ReadByte() << 24) | (br.ReadByte() << 16) | (br.ReadByte() << 8) | br.ReadByte();
			int num2 = (br.ReadByte() << 24) | (br.ReadByte() << 16) | (br.ReadByte() << 8) | br.ReadByte();
			lastRoundTripTime = SupportClass.GetTickCount() - num2;
			if (!serverTimeOffsetIsAvailable)
			{
				roundTripTime = lastRoundTripTime;
			}
			UpdateRoundTripTimeAndVariance(lastRoundTripTime);
			if (!serverTimeOffsetIsAvailable)
			{
				serverTimeOffset = num + (lastRoundTripTime >> 1) - SupportClass.GetTickCount();
				serverTimeOffsetIsAvailable = true;
			}
		}

		internal override bool EnqueueOperation(Dictionary<byte, object> parameters, byte opCode, bool sendReliable, byte channelId, bool encrypted, EgMessageType messageType)
		{
			if (peerConnectionState != ConnectionStateValue.Connected)
			{
				if ((int)debugOut >= 1)
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: Not connected. PeerState: " + peerConnectionState);
				}
				base.Listener.OnStatusChanged(StatusCode.SendError);
				return false;
			}
			byte[] array = SerializeOperationToMessage(opCode, parameters, messageType, encrypted);
			if (array == null)
			{
				return false;
			}
			outgoingStream.Write(array, 0, array.Length);
			return true;
		}

		internal override byte[] SerializeOperationToMessage(byte opCode, Dictionary<byte, object> parameters, EgMessageType messageType, bool encrypt)
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
				Protocol.SerializeOperationRequest(SerializeMemStream, opCode, parameters, false);
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
			if (messageType != EgMessageType.Operation)
			{
				array[messageHeader.Length - 1] = (byte)messageType;
			}
			if (encrypt)
			{
				array[messageHeader.Length - 1] = (byte)(array[messageHeader.Length - 1] | 0x80u);
			}
			int targetOffset = 1;
			Protocol.Serialize(array.Length, array, ref targetOffset);
			return array;
		}

		internal override void StopConnection()
		{
			throw new NotImplementedException();
		}

		[CompilerGenerated]
		private static int _003C_002Ector_003Eb__0()
		{
			return Environment.TickCount;
		}

		[CompilerGenerated]
		private static int _003C_checkAckCondition_003Eb__3(AsyncRequestState request)
		{
			return request.id;
		}

		[CompilerGenerated]
		private void _003CEnqueueErrorDisconnect_003Eb__5()
		{
			Disconnected();
		}
	}
}
