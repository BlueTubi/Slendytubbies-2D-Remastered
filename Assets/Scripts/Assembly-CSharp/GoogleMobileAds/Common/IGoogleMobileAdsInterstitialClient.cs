using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	internal interface IGoogleMobileAdsInterstitialClient
	{
		void CreateInterstitialAd(string adUnitId);

		void LoadAd(AdRequest request);

		bool IsLoaded();

		void ShowInterstitial();

		void DestroyInterstitial();

		void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey);
	}
}
