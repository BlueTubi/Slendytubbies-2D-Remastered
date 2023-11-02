using System.Collections.Generic;

namespace ExitGames.Client.Photon.Lite
{
	public class LitePeer : PhotonPeer
	{
		public LitePeer(IPhotonPeerListener listener)
			: base(listener, ConnectionProtocol.Udp)
		{
		}

		protected LitePeer()
			: base(ConnectionProtocol.Udp)
		{
		}

		protected LitePeer(ConnectionProtocol protocolType)
			: base(protocolType)
		{
		}

		public LitePeer(IPhotonPeerListener listener, ConnectionProtocol protocolType)
			: base(listener, protocolType)
		{
		}

		public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
		{
			if ((int)base.DebugOut >= 5)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (groupsToRemove != null)
			{
				dictionary[239] = groupsToRemove;
			}
			if (groupsToAdd != null)
			{
				dictionary[238] = groupsToAdd;
			}
			return OpCustom(248, dictionary, true, 0);
		}

		public virtual bool OpRaiseEvent(byte eventCode, bool sendReliable, object customEventContent)
		{
			return OpRaiseEvent(eventCode, sendReliable, customEventContent, 0, EventCaching.DoNotCache, null, ReceiverGroup.Others, 0);
		}

		public virtual bool OpRaiseEvent(byte eventCode, bool sendReliable, object customEventContent, byte channelId, EventCaching cache, int[] targetActors, ReceiverGroup receivers, byte interestGroup)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[244] = eventCode;
			if (customEventContent != null)
			{
				dictionary[245] = customEventContent;
			}
			if (cache != 0)
			{
				dictionary[247] = (byte)cache;
			}
			if (receivers != 0)
			{
				dictionary[246] = (byte)receivers;
			}
			if (interestGroup != 0)
			{
				dictionary[240] = interestGroup;
			}
			if (targetActors != null)
			{
				dictionary[252] = targetActors;
			}
			return OpCustom(253, dictionary, sendReliable, channelId, false);
		}

		public virtual bool OpRaiseEvent(byte eventCode, byte interestGroup, Hashtable customEventContent, bool sendReliable)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[245] = customEventContent;
			dictionary[244] = eventCode;
			if (interestGroup != 0)
			{
				dictionary[240] = interestGroup;
			}
			return OpCustom(253, dictionary, sendReliable, 0);
		}

		public virtual bool OpRaiseEvent(byte eventCode, Hashtable customEventContent, bool sendReliable)
		{
			return OpRaiseEvent(eventCode, customEventContent, sendReliable, 0);
		}

		public virtual bool OpRaiseEvent(byte eventCode, Hashtable customEventContent, bool sendReliable, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[245] = customEventContent;
			dictionary[244] = eventCode;
			return OpCustom(253, dictionary, sendReliable, channelId);
		}

		public virtual bool OpRaiseEvent(byte eventCode, Hashtable customEventContent, bool sendReliable, byte channelId, int[] targetActors)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[245] = customEventContent;
			dictionary[244] = eventCode;
			if (targetActors != null)
			{
				dictionary[252] = targetActors;
			}
			return OpCustom(253, dictionary, sendReliable, channelId);
		}

		public virtual bool OpRaiseEvent(byte eventCode, Hashtable customEventContent, bool sendReliable, byte channelId, EventCaching cache, ReceiverGroup receivers)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[245] = customEventContent;
			dictionary[244] = eventCode;
			if (cache != 0)
			{
				dictionary[247] = (byte)cache;
			}
			if (receivers != 0)
			{
				dictionary[246] = (byte)receivers;
			}
			return OpCustom(253, dictionary, sendReliable, channelId, false);
		}

		public virtual bool OpSetPropertiesOfActor(int actorNr, Hashtable properties, bool broadcast, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, properties);
			dictionary.Add(254, actorNr);
			if (broadcast)
			{
				dictionary.Add(250, broadcast);
			}
			return OpCustom(252, dictionary, true, channelId);
		}

		public virtual bool OpSetPropertiesOfGame(Hashtable properties, bool broadcast, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, properties);
			if (broadcast)
			{
				dictionary.Add(250, broadcast);
			}
			return OpCustom(252, dictionary, true, channelId);
		}

		public virtual bool OpGetProperties(byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, (byte)3);
			return OpCustom(251, dictionary, true, channelId);
		}

		public virtual bool OpGetPropertiesOfActor(int[] actorNrList, string[] properties, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, LitePropertyTypes.Actor);
			if (properties != null)
			{
				dictionary.Add(249, properties);
			}
			if (actorNrList != null)
			{
				dictionary.Add(252, actorNrList);
			}
			return OpCustom(251, dictionary, true, channelId);
		}

		public virtual bool OpGetPropertiesOfActor(int[] actorNrList, byte[] properties, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, LitePropertyTypes.Actor);
			if (properties != null)
			{
				dictionary.Add(249, properties);
			}
			if (actorNrList != null)
			{
				dictionary.Add(252, actorNrList);
			}
			return OpCustom(251, dictionary, true, channelId);
		}

		public virtual bool OpGetPropertiesOfGame(string[] properties, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, LitePropertyTypes.Game);
			if (properties != null)
			{
				dictionary.Add(248, properties);
			}
			return OpCustom(251, dictionary, true, channelId);
		}

		public virtual bool OpGetPropertiesOfGame(byte[] properties, byte channelId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, LitePropertyTypes.Game);
			if (properties != null)
			{
				dictionary.Add(248, properties);
			}
			return OpCustom(251, dictionary, true, channelId);
		}

		public virtual bool OpJoin(string gameName)
		{
			return OpJoin(gameName, null, null, false);
		}

		public virtual bool OpJoin(string gameName, Hashtable gameProperties, Hashtable actorProperties, bool broadcastActorProperties)
		{
			if ((int)base.DebugOut >= 5)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "OpJoin(" + gameName + ")");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[byte.MaxValue] = gameName;
			if (actorProperties != null)
			{
				dictionary[249] = actorProperties;
			}
			if (gameProperties != null)
			{
				dictionary[248] = gameProperties;
			}
			if (broadcastActorProperties)
			{
				dictionary[250] = broadcastActorProperties;
			}
			return OpCustom(byte.MaxValue, dictionary, true, 0, false);
		}

		public virtual bool OpLeave()
		{
			if ((int)base.DebugOut >= 5)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "OpLeave()");
			}
			return OpCustom(254, null, true, 0);
		}
	}
}
