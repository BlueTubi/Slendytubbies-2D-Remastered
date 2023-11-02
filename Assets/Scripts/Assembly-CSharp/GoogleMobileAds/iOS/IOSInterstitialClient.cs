using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	internal class IOSInterstitialClient : IGoogleMobileAdsInterstitialClient
	{
		internal delegate void GADUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);

		internal delegate void GADUInterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error);

		internal delegate void GADUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);

		internal delegate void GADUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);

		internal delegate void GADUInterstitialDidDismissScreenCallback(IntPtr interstitialClient);

		internal delegate void GADUInterstitialWillLeaveApplicationCallback(IntPtr interstitialClient);

		private IAdListener listener;

		private IntPtr interstitialPtr;

		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		private IntPtr InterstitialPtr
		{
			get
			{
				return interstitialPtr;
			}
			set
			{
				Externs.GADURelease(interstitialPtr);
				interstitialPtr = value;
			}
		}

		public IOSInterstitialClient(IAdListener listener)
		{
			this.listener = listener;
		}

		public void CreateInterstitialAd(string adUnitId)
		{
			IntPtr interstitialClient = (IntPtr)GCHandle.Alloc(this);
			InterstitialPtr = Externs.GADUCreateInterstitial(interstitialClient, adUnitId);
			Externs.GADUSetInterstitialCallbacks(InterstitialPtr, InterstitialDidReceiveAdCallback, InterstitialDidFailToReceiveAdWithErrorCallback, InterstitialWillPresentScreenCallback, InterstitialWillDismissScreenCallback, InterstitialDidDismissScreenCallback, InterstitialWillLeaveApplicationCallback);
		}

		public void LoadAd(AdRequest request)
		{
			IntPtr intPtr = Externs.GADUCreateRequest();
			foreach (string keyword in request.Keywords)
			{
				Externs.GADUAddKeyword(intPtr, keyword);
			}
			foreach (string testDevice in request.TestDevices)
			{
				Externs.GADUAddTestDevice(intPtr, testDevice);
			}
			if (request.Birthday.HasValue)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				Externs.GADUSetBirthday(intPtr, valueOrDefault.Year, valueOrDefault.Month, valueOrDefault.Day);
			}
			if (request.Gender.HasValue)
			{
				Externs.GADUSetGender(intPtr, (int)request.Gender.GetValueOrDefault());
			}
			if (request.TagForChildDirectedTreatment.HasValue)
			{
				Externs.GADUTagForChildDirectedTreatment(intPtr, request.TagForChildDirectedTreatment.GetValueOrDefault());
			}
			foreach (KeyValuePair<string, string> extra in request.Extras)
			{
				Externs.GADUSetExtra(intPtr, extra.Key, extra.Value);
			}
			Externs.GADUSetExtra(intPtr, "unity", "1");
			Externs.GADURequestInterstitial(InterstitialPtr, intPtr);
			Externs.GADURelease(intPtr);
		}

		public bool IsLoaded()
		{
			return Externs.GADUInterstitialReady(InterstitialPtr);
		}

		public void ShowInterstitial()
		{
			Externs.GADUShowInterstitial(InterstitialPtr);
		}

		public void DestroyInterstitial()
		{
			InterstitialPtr = IntPtr.Zero;
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialDidReceiveAdCallback))]
		private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialDidFailToReceiveAdWithErrorCallback))]
		private static void InterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(error);
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialWillPresentScreenCallback))]
		private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialWillDismissScreenCallback))]
		private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialDidDismissScreenCallback))]
		private static void InterstitialDidDismissScreenCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosed();
		}

		[MonoPInvokeCallback(typeof(GADUInterstitialWillLeaveApplicationCallback))]
		private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
		{
			IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
		}

		private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
		{
			return ((GCHandle)interstitialClient).Target as IOSInterstitialClient;
		}

		public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string publicKey)
		{
		}
	}
}
