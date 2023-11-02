using System;
using System.Runtime.CompilerServices;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class BannerView : IAdListener
	{
		private IGoogleMobileAdsBannerClient client;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cache7;

		[CompilerGenerated]
		private static EventHandler<AdFailedToLoadEventArgs> _003C_003Ef__am_0024cache8;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cache9;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheA;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheB;

		[CompilerGenerated]
		private static EventHandler<EventArgs> _003C_003Ef__am_0024cacheC;

		public event EventHandler<EventArgs> AdLoaded;

		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad;

		public event EventHandler<EventArgs> AdOpened;

		public event EventHandler<EventArgs> AdClosing;

		public event EventHandler<EventArgs> AdClosed;

		public event EventHandler<EventArgs> AdLeftApplication;

		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			if (_003C_003Ef__am_0024cache7 == null)
			{
				_003C_003Ef__am_0024cache7 = _003CAdLoaded_003Em__0;
			}
			this.AdLoaded = _003C_003Ef__am_0024cache7;
			if (_003C_003Ef__am_0024cache8 == null)
			{
				_003C_003Ef__am_0024cache8 = _003CAdFailedToLoad_003Em__1;
			}
			this.AdFailedToLoad = _003C_003Ef__am_0024cache8;
			if (_003C_003Ef__am_0024cache9 == null)
			{
				_003C_003Ef__am_0024cache9 = _003CAdOpened_003Em__2;
			}
			this.AdOpened = _003C_003Ef__am_0024cache9;
			if (_003C_003Ef__am_0024cacheA == null)
			{
				_003C_003Ef__am_0024cacheA = _003CAdClosing_003Em__3;
			}
			this.AdClosing = _003C_003Ef__am_0024cacheA;
			if (_003C_003Ef__am_0024cacheB == null)
			{
				_003C_003Ef__am_0024cacheB = _003CAdClosed_003Em__4;
			}
			this.AdClosed = _003C_003Ef__am_0024cacheB;
			if (_003C_003Ef__am_0024cacheC == null)
			{
				_003C_003Ef__am_0024cacheC = _003CAdLeftApplication_003Em__5;
			}
			this.AdLeftApplication = _003C_003Ef__am_0024cacheC;
			client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsBannerClient(this);
			client.CreateBannerView(adUnitId, adSize, position);
		}

		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
			adFailedToLoadEventArgs.Message = message;
			this.AdFailedToLoad(this, adFailedToLoadEventArgs);
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

		public void LoadAd(AdRequest request)
		{
			client.LoadAd(request);
		}

		public void Hide()
		{
			client.HideBannerView();
		}

		public void Show()
		{
			client.ShowBannerView();
		}

		public void Destroy()
		{
			client.DestroyBannerView();
		}

		[CompilerGenerated]
		private static void _003CAdLoaded_003Em__0(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdFailedToLoad_003Em__1(object P_0, AdFailedToLoadEventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdOpened_003Em__2(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdClosing_003Em__3(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdClosed_003Em__4(object P_0, EventArgs P_1)
		{
		}

		[CompilerGenerated]
		private static void _003CAdLeftApplication_003Em__5(object P_0, EventArgs P_1)
		{
		}
	}
}
