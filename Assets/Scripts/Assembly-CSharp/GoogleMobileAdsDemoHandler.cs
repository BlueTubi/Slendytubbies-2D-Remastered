using GoogleMobileAds.Api;

public class GoogleMobileAdsDemoHandler : IInAppPurchaseHandler
{
	private readonly string[] validSkus = new string[1] { "android.test.purchased" };

	public string AndroidPublicKey
	{
		get
		{
			return null;
		}
	}

	public void OnInAppPurchaseFinished(IInAppPurchaseResult result)
	{
		result.FinishPurchase();
		GoogleMobileAdsDemoScript.OutputMessage = "Purchase Succeeded! Credit user here.";
	}

	public bool IsValidPurchase(string sku)
	{
		string[] array = validSkus;
		foreach (string text in array)
		{
			if (sku == text)
			{
				return true;
			}
		}
		return false;
	}
}
