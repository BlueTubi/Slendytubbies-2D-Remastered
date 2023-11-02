using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerSettings : ScriptableObject
{
	public enum HostingOption
	{
		NotSet = 0,
		PhotonCloud = 1,
		SelfHosted = 2,
		OfflineMode = 3
	}

	public const string DefaultCloudServerUrl = "app-eu.exitgamescloud.com";

	public const string DefaultServerAddress = "127.0.0.1";

	public const int DefaultMasterPort = 5055;

	public const int DefaultNameServerPort = 5058;

	public const string DefaultAppID = "Master";

	public static readonly string[] CloudServerRegionPrefixes = new string[4] { "app-eu", "app-us", "app-asia", "app-jp" };

	public HostingOption HostType;

	public string ServerAddress = "127.0.0.1";

	public int ServerPort = 5055;

	public string AppID = string.Empty;

	public bool PingCloudServersOnAwake;

	public List<string> RpcList;

	[HideInInspector]
	public bool DisableAutoOpenWizard;

	public string Region
	{
		get
		{
			return ExtractRegionFromAddress(ServerAddress);
		}
	}

	public static int FindRegionForServerAddress(string server)
	{
		int result = 0;
		for (int i = 0; i < CloudServerRegionPrefixes.Length; i++)
		{
			if (server.StartsWith(CloudServerRegionPrefixes[i]))
			{
				return i;
			}
		}
		return result;
	}

	public static string ExtractRegionFromAddress(string address)
	{
		if (address == null)
		{
			return null;
		}
		int num = address.IndexOf('.');
		if (num < 5)
		{
			return null;
		}
		string text = address.Substring(4, num - 4);
		Debug.Log("Extracted region: " + text);
		return text;
	}

	public static string FindServerAddressForRegion(int regionIndex)
	{
		return "app-eu.exitgamescloud.com".Replace("app-eu", CloudServerRegionPrefixes[regionIndex]);
	}

	public static string FindServerAddressForRegion(CloudServerRegion regionIndex)
	{
		return "app-eu.exitgamescloud.com".Replace("app-eu", CloudServerRegionPrefixes[(int)regionIndex]);
	}

	public static bool TryParseCloudServerRegion(string regionShortcut, out CloudServerRegion region)
	{
		//Discarded unreachable code: IL_0028
		region = CloudServerRegion.US;
		try
		{
			region = (CloudServerRegion)(int)Enum.Parse(typeof(CloudServerRegion), regionShortcut, true);
		}
		catch
		{
			return false;
		}
		return true;
	}

	public void UseCloud(string cloudAppid, int regionIndex)
	{
		HostType = HostingOption.PhotonCloud;
		AppID = cloudAppid;
		ServerAddress = FindServerAddressForRegion(regionIndex);
		ServerPort = 5055;
	}

	public void UseMyServer(string serverAddress, int serverPort, string application)
	{
		HostType = HostingOption.SelfHosted;
		AppID = ((application == null) ? "Master" : application);
		ServerAddress = serverAddress;
		ServerPort = serverPort;
	}

	public override string ToString()
	{
		return string.Concat("ServerSettings: ", HostType, " ", ServerAddress);
	}
}
