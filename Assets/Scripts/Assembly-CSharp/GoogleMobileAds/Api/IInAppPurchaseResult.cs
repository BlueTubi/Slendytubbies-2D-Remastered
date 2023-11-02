namespace GoogleMobileAds.Api
{
	public interface IInAppPurchaseResult
	{
		string ProductId { get; }

		void FinishPurchase();
	}
}
