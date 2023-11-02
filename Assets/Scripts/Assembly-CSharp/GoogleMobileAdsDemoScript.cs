using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
	private BannerView bannerView;

	private InterstitialAd interstitial;

	private static string outputMessage = string.Empty;

	public static string OutputMessage
	{
		set
		{
			outputMessage = value;
		}
	}

	private void OnGUI()
	{
		GUI.skin.button.fontSize = (int)(0.05f * (float)Screen.height);
		GUI.skin.label.fontSize = (int)(0.025f * (float)Screen.height);
		Rect position = new Rect(0.1f * (float)Screen.width, 0.05f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position, "Request Banner"))
		{
			RequestBanner();
		}
		Rect position2 = new Rect(0.1f * (float)Screen.width, 0.175f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position2, "Show Banner"))
		{
			bannerView.Show();
		}
		Rect position3 = new Rect(0.1f * (float)Screen.width, 0.3f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position3, "Hide Banner"))
		{
			bannerView.Hide();
		}
		Rect position4 = new Rect(0.1f * (float)Screen.width, 0.425f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position4, "Destroy Banner"))
		{
			bannerView.Destroy();
		}
		Rect position5 = new Rect(0.1f * (float)Screen.width, 0.55f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position5, "Request Interstitial"))
		{
			RequestInterstitial();
		}
		Rect position6 = new Rect(0.1f * (float)Screen.width, 0.675f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position6, "Show Interstitial"))
		{
			ShowInterstitial();
		}
		Rect position7 = new Rect(0.1f * (float)Screen.width, 0.8f * (float)Screen.height, 0.8f * (float)Screen.width, 0.1f * (float)Screen.height);
		if (GUI.Button(position7, "Destroy Interstitial"))
		{
			interstitial.Destroy();
		}
		Rect position8 = new Rect(0.1f * (float)Screen.width, 0.925f * (float)Screen.height, 0.8f * (float)Screen.width, 0.05f * (float)Screen.height);
		GUI.Label(position8, outputMessage);
	}

	private void RequestBanner()
	{
		string adUnitId = "unexpected_platform";
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		bannerView.LoadAd(createAdRequest());
	}

	private void RequestInterstitial()
	{
		string adUnitId = "unexpected_platform";
		interstitial = new InterstitialAd(adUnitId);
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		GoogleMobileAdsDemoHandler inAppPurchaseHandler = new GoogleMobileAdsDemoHandler();
		interstitial.SetInAppPurchaseHandler(inAppPurchaseHandler);
		interstitial.LoadAd(createAdRequest());
	}

	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game")
			.SetGender(Gender.Male)
			.SetBirthday(new DateTime(1985, 1, 1))
			.TagForChildDirectedTreatment(false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			MonoBehaviour.print("Interstitial is not ready yet.");
		}
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received.");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	private void HandleAdClosing(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosing event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received.");
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialOpened event received");
	}

	private void HandleInterstitialClosing(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosing event received");
	}

	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}
}
