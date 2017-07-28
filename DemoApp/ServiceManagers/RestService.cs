using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using DemoApp.Models;
using Newtonsoft.Json.Linq;
using DemoApp.ServiceManagers;

namespace DemoApp
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
        }

		public async Task<User> DoLoginWithCredentials(User user)
		{
			var uri = new Uri(Constants.BASE_URL + "ums/ws/user/login");

			Dictionary<string, string> userDic = new Dictionary<string, string>();
			userDic.Add("Email", user.Email);
			userDic.Add("Password", user.Password);
			userDic.Add("Environment", "ios");
			Dictionary<string, Dictionary<string, string>> serviceInputs = new Dictionary<string, Dictionary<string, string>>();
			serviceInputs.Add("user", userDic);

			try
			{
				var json = JsonConvert.SerializeObject(serviceInputs);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = null;
				response = await client.PostAsync(uri, content);
				if (response.IsSuccessStatusCode)
				{
					var responseJSON = response.Content.ReadAsStringAsync().Result;
					var resultDict = JObject.Parse(responseJSON);
					var val = (resultDict["user"])["UserID"].ToString();
					user.FirstName = (resultDict["user"])["FirstName"].ToString();
					user.Email = (resultDict["user"])["Email"].ToString();
					user.UserID = Convert.ToInt32((resultDict["user"])["UserID"].ToString());
					System.Diagnostics.Debug.WriteLine("successfully logged in , userID= {0}.", val);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("ERROR {0}", ex.Message);
			}
			return user;
		}
    }
}
