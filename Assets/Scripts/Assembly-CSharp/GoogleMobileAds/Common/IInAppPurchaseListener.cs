using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	internal interface IInAppPurchaseListener
	{
		void FireOnInAppPurchaseFinished(IInAppPurchaseResult result);

		bool FireIsValidPurchase(string sku);
	}
}
