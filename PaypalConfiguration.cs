using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore
{
	public static class PaypalConfiguration
	{
		public readonly static string ClientId;
		public readonly static string ClientSecret;

		static PaypalConfiguration()
		{
			var config = GetConfig();
			ClientId = config["clientId"];
			ClientSecret = config["clientSecret"];
		}

		private static Dictionary<string, string> GetConfig()
		{
			return PayPal.Api.ConfigManager.Instance.GetProperties();
		}

		private static string GetAccessToken()
		{
			string accessToken = new OAuthTokenCredential
		(ClientId, ClientSecret, GetConfig()).GetAccessToken();

			return accessToken;
		}

		public static APIContext GetAPIContext()
		{
			APIContext apicontext = new APIContext(GetAccessToken());
			apicontext.Config = GetConfig();
			return apicontext;
		}
	}
}