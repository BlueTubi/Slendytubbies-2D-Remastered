using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	internal class IOSBannerClient : IGoogleMobileAdsBannerClient
	{
		internal delegate void GADUAdViewDidReceiveAdCallback(IntPtr bannerClient);

		internal delegate void GADUAdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error);

		internal delegate void GADUAdViewWillPresentScreenCallback(IntPtr bannerClient);

		internal delegate void GADUAdViewWillDismissScreenCallback(IntPtr bannerClient);

		internal delegate void GADUAdViewDidDismissScreenCallback(IntPtr bannerClient);

		internal delegate void GADUAdViewWillLeaveApplicationCallback(IntPtr bannerClient);

		private IAdListener listener;

		private IntPtr bannerViewPtr;

		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		private IntPtr BannerViewPtr
		{
			get
			{
				return bannerViewPtr;
			}
			set
			{
				Externs.GADURelease(bannerViewPtr);
				bannerViewPtr = value;
			}
		}

		public IOSBannerClient(IAdListener listener)
		{
			this.listener = listener;
		}

		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			IntPtr bannerClient = (IntPtr)GCHandle.Alloc(this);
			if (adSize.IsSmartBanner)
			{
				BannerViewPtr = Externs.GADUCreateSmartBannerView(bannerClient, adUnitId, (int)position);
			}
			else
			{
				BannerViewPtr = Externs.GADUCreateBannerView(bannerClient, adUnitId, adSize.Width, adSize.Height, (int)position);
			}
			Externs.GADUSetBannerCallbacks(BannerViewPtr, AdViewDidReceiveAdCallback, AdViewDidFailToReceiveAdWithErrorCallback, AdViewWillPresentScreenCallback, AdViewWillDismissScreenCallback, AdViewDidDismissScreenCallback, AdViewWillLeaveApplicationCallback);
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
			Externs.GADURequestBannerAd(BannerViewPtr, intPtr);
			Externs.GADURelease(intPtr);
		}

		public void ShowBannerView()
		{
			Externs.GADUShowBannerView(BannerViewPtr);
		}

		public void HideBannerView()
		{
			Externs.GADUHideBannerView(BannerViewPtr);
		}

		public void DestroyBannerView()
		{
			Externs.GADURemoveBannerView(BannerViewPtr);
			BannerViewPtr = IntPtr.Zero;
		}

		[MonoPInvokeCallback(typeof(GADUAdViewDidReceiveAdCallback))]
		private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdLoaded();
		}

		[MonoPInvokeCallback(typeof(GADUAdViewDidFailToReceiveAdWithErrorCallback))]
		private static void AdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdFailedToLoad(error);
		}

		[MonoPInvokeCallback(typeof(GADUAdViewWillPresentScreenCallback))]
		private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdOpened();
		}

		[MonoPInvokeCallback(typeof(GADUAdViewWillDismissScreenCallback))]
		private static void AdViewWillDismissScreenCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdClosing();
		}

		[MonoPInvokeCallback(typeof(GADUAdViewDidDismissScreenCallback))]
		private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdClosed();
		}

		[MonoPInvokeCallback(typeof(GADUAdViewWillLeaveApplicationCallback))]
		private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
		{
			IntPtrToBannerClient(bannerClient).listener.FireAdLeftApplication();
		}

		private static IOSBannerClient IntPtrToBannerClient(IntPtr bannerClient)
		{
			return ((GCHandle)bannerClient).Target as IOSBannerClient;
		}
	}
}
