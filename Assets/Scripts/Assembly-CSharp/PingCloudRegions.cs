using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class PingCloudRegions : MonoBehaviour
{
	private const string PlayerPrefsKey = "PUNCloudBestRegion";

	public static string ClosestRegion;

	public static PingCloudRegions Instance;

	private bool isPinging;

	private int lowestRegionAverage = -1;

	public static bool ClosestRegionAvailable
	{
		get
		{
			return !string.IsNullOrEmpty(ClosestRegion);
		}
	}

	public void Awake()
	{
		Instance = this;
		if (!LoadRegion(out ClosestRegion) && PhotonNetwork.PhotonServerSettings.PingCloudServersOnAwake)
		{
			RefreshCloudServerRating();
		}
	}

	public static void RefreshCloudServerRating()
	{
		if (Instance != null)
		{
			if (Instance.isPinging)
			{
				Debug.Log("RefreshCloudServerRating already in process. Skipping this call.");
			}
			else
			{
				Instance.StartCoroutine(Instance.PingAllRegions());
			}
		}
	}

	private IEnumerator PingAllRegions()
	{
		ServerSettings settings = (ServerSettings)Resources.Load("PhotonServerSettings", typeof(ServerSettings));
		if (settings.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			yield break;
		}
		ClosestRegion = null;
		isPinging = true;
		lowestRegionAverage = -1;
		foreach (int region in Enum.GetValues(typeof(CloudServerRegion)))
		{
			yield return StartCoroutine(PingRegion((CloudServerRegion)region));
		}
		isPinging = false;
	}

	private IEnumerator PingRegion(CloudServerRegion region)
	{
		string hostname = ServerSettings.FindServerAddressForRegion(region);
		string regionIp = ResolveHost(hostname);
		if (string.IsNullOrEmpty(regionIp))
		{
			Debug.LogError("Could not resolve host: " + hostname);
			yield break;
		}
		int pingSum = 0;
		int tries = 3;
		int skipped = 0;
		float timeout = 0.7f;
		for (int i = 0; i < tries; i++)
		{
			float startTime = Time.time;
			Ping ping = new Ping(regionIp);
			while (!ping.isDone && Time.time < startTime + timeout)
			{
				yield return 0;
			}
			if (ping.time == -1)
			{
				if (skipped > 5)
				{
					pingSum += (int)(timeout * 1000f) * tries;
					break;
				}
				i--;
				skipped++;
			}
			else
			{
				pingSum += ping.time;
			}
		}
		int pingAverage = pingSum / tries;
		if (pingAverage < lowestRegionAverage || lowestRegionAverage == -1)
		{
			lowestRegionAverage = pingAverage;
			SaveAndSetRegion(region.ToString());
		}
	}

	public static void ConnectToBestRegion()
	{
		Instance.StartCoroutine(Instance.ConnectToBestRegionInternal());
	}

	private IEnumerator ConnectToBestRegionInternal()
	{
		CloudServerRegion bestRegion;
		if (!ClosestRegionAvailable || !ServerSettings.TryParseCloudServerRegion(ClosestRegion, out bestRegion))
		{
			RefreshCloudServerRating();
		}
		while (isPinging)
		{
			yield return 0;
		}
		ServerSettings.TryParseCloudServerRegion(ClosestRegion, out bestRegion);
		string bestServerAddress = ServerSettings.FindServerAddressForRegion(bestRegion);
		string bestServerFullAddress = bestServerAddress + ":" + 5055;
		PhotonNetwork.networkingPeer.MasterServerAddress = bestServerFullAddress;
		PhotonNetwork.networkingPeer.Connect(bestServerFullAddress, ServerConnection.MasterServer);
	}

	private static bool LoadRegion(out string region)
	{
		region = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
		return !string.IsNullOrEmpty(region);
	}

	private static void SaveAndSetRegion(string region)
	{
		ClosestRegion = region;
		PlayerPrefs.SetString("PUNCloudBestRegion", region);
	}

	public static void DeleteRegionPref()
	{
		if (Instance != null && Instance.isPinging)
		{
			Debug.LogWarning("DeleteRegionPref can't delete region while pining is going on.");
			return;
		}
		ClosestRegion = null;
		PlayerPrefs.DeleteKey("PUNCloudBestRegion");
	}

	public static void OverrideRegion(CloudServerRegion region)
	{
		SaveAndSetRegion(region.ToString());
	}

	public static string ResolveHost(string hostName)
	{
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
			if (hostAddresses.Length == 1)
			{
				return hostAddresses[0].ToString();
			}
			foreach (IPAddress iPAddress in hostAddresses)
			{
				if (iPAddress != null)
				{
					string text = iPAddress.ToString();
					if (text.IndexOf('.') >= 0)
					{
						return text;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Exception caught! " + ex.Source + " Message: " + ex.Message);
		}
		return string.Empty;
	}
}
