namespace GoogleMobileAds.Common
{
	internal interface IAdListener
	{
		void FireAdLoaded();

		void FireAdFailedToLoad(string message);

		void FireAdOpened();

		void FireAdClosing();

		void FireAdClosed();

		void FireAdLeftApplication();
	}
}
