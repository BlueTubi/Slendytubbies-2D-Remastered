using System;
using System.Runtime.CompilerServices;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class InterstitialAd : IAdListener, IInAppPurchaseListener
	{
		private IGoogleMobileAdsInterstitialClient client;

		private IInAppPurchaseHandler handler;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cache8;

		[CompilerGenerated]
		private static EventHandler<AdFailedToLoadEventArgs> _003C_003Ef__am_0024cache9;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheA;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheB;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheC;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheD;

		public event EventHandler<EventArgs> AdLoaded;

		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad;

		public event EventHandler<EventArgs> AdOpened;

		public event EventHandler<EventArgs> AdClosing;

		public event EventHandler<EventArgs> AdClosed;

		public event EventHandler<EventArgs> AdLeftApplication;

		public InterstitialAd(string adUnitId)
		{
			if (_003C_003Ef__am_0024cache8 == null)
			{
				_003C_003Ef__am_0024cache8 = _003CAdLoaded_003Em__6;
			}
			this.AdLoaded = _003C_003Ef__am_0024cache8;
			if (_003C_003Ef__am_0024cache9 == null)
			{
				_003C_003Ef__am_0024cache9 = _003CAdFailedToLoad_003Em__7;
			}
			this.AdFailedToLoad = _003C_003Ef__am_0024cache9;
			if (_003C_003Ef__am_0024cacheA == null)
			{
				_003C_003Ef__am_0024cacheA = _003CAdOpened_003Em__8;
			}
			this.AdOpened = _003C_003Ef__am_0024cacheA;
			if (_003C_003Ef__am_0024cacheB == null)
			{
				_003C_003Ef__am_0024cacheB = _003CAdClosing_003Em__9;
			}
			this.AdClosing = _003C_003Ef__am_0024cacheB;
			if (_003C_003Ef__am_0024cacheC == null)
			{
				_003C_003Ef__am_0024cacheC = _003CAdClosed_003Em__A;
			}
			this.AdClosed = _003C_003Ef__am_0024cacheC;
			if (_003C_003Ef__am_0024cacheD == null)
			{
				_003C_003Ef__am_0024cacheD = _003CAdLeftApplication_003Em__B;
			}
			this.AdLeftApplication = _003C_003Ef__am_0024cacheD;
			client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsInterstitialClient(this);
			client.CreateInterstitialAd(adUnitId);
		}

		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
			adFailedToLoadEventArgs.Message = message;
			AdFailedToLoadEventArgs e = adFailedToLoadEventArgs;
			this.AdFailedToLoad(this, e);
		}

		void IAdListener.FireAdOpened()
		{
			this.AdOpened(this, EventArgs.Empty);
		}

		void IAdListener.FireAdClosing()
		{
			this.AdClosing(this, EventArgs.Empty);
		}

		void IAdListener.FireAdClosed()
		{
			this.AdClosed(this, EventArgs.Empty);
		}

		void IAdListener.FireAdLeftApplication()
		{
			this.AdLeftApplication(this, EventArgs.Empty);
		}

		bool IInAppPurchaseListener.FireIsValidPurchase(string sku)
		{
			if (handler != null)
			{
				return handler.IsValidPurchase(sku);
			}
			return false;
		}

		void IInAppPurchaseListener.FireOnInAppPurchaseFinished(IInAppPurchaseResult result)
		{
			if (handler != null)
			{
				handler.OnInAppPurchaseFinished(result);
			}
		}

		public void LoadAd(AdRequest request)
		{
			client.LoadAd(request);
		}

		public bool IsLoaded()
		{
			return client.IsLoaded();
		}

		public void Show()
		{
			client.ShowInterstitial();
		}

		public void Destroy()
		{
			client.DestroyInterstitial();
		}

		public void SetInAppPurchaseHandler(IInAppPurchaseHandler handler)
		{
			this.handler = handler;
			client.SetInAppPurchaseParams(this, handler.AndroidPublicKey);
		}

		[CompilerGenerated]
		private static void _003CAdLoaded_003Em__6(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdFailedToLoad_003Em__7(object P_0, AdFailedToLoadEventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdOpened_003Em__8(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdClosing_003Em__9(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdClosed_003Em__A(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdLeftApplication_003Em__B(object P_0, EventArgs P_1)
		{
		}
	}
}
