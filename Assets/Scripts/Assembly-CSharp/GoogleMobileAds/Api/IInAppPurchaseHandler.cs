namespace GoogleMobileAds.Api
{
	public interface IInAppPurchaseHandler
	{
		string AndroidPublicKey { get; }

		void OnInAppPurchaseFinished(IInAppPurchaseResult result);

		bool IsValidPurchase(string sku);
	}
}
