using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	internal interface IGoogleMobileAdsBannerClient
	{
		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		void LoadAd(AdRequest request);

		void ShowBannerView();

		void HideBannerView();

		void DestroyBannerView();
	}
}
