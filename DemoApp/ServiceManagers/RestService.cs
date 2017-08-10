using System;
using System.Net;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Globalization;
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
using DemoApp.Utils;

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
                    user.ProfilePicURL = (resultDict["user"])["ProfileImage"].ToString();
                    user.UserID = Convert.ToInt32((resultDict["user"])["UserID"].ToString());
                    System.Diagnostics.Debug.WriteLine("successfully logged in , userID= {0}.", val);
                    // set data in App settings
                    await Utility.setUserDetails(user);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR {0}", ex.Message);
            }
            return user;
        }

        public async Task<User> DoImageUpload(User user)
        {
            var uri = new Uri(Constants.BASE_URL + "ums/ws/user/profile");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ2aXZlay5zaGFoQHp5bXIuY29tIiwidXNlcklkIjozMjAwNSwidXNlck5hbWUiOiJWaXZlayIsImp3dFRva2VuQ3JlYXRldGltZSI6MTUwMjI4MDY2ODg3OCwic291cmNlIjoiaW9zIiwiZXhwIjoxNTAyMzY1MjY4fQ.9l3nWR3uHrHegHbzS9VX1uJOo3HqmltGkvgykE558CpKOl5jsJvZXAQe0R15C-e5jiARqU-DnCAaBo_Gkaj0yQ");

                var fileContent = new ByteArrayContent(user.ProfilePicData);
				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
				fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
				{
					Name = "profile",
					FileName = "photo.png"
				};

                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(fileContent);
                 
                HttpResponseMessage response = null;
                using (response = await client.PostAsync(uri, content))
                {
                    var responseJSON = await response.Content.ReadAsStringAsync();
                    var resultDict = JObject.Parse(responseJSON);
                    if (response.IsSuccessStatusCode)
                    {
						var val = (resultDict["user"])["UserID"].ToString();
						user.FirstName = (resultDict["user"])["FirstName"].ToString();
						user.Email = (resultDict["user"])["Email"].ToString();
						user.ProfilePicURL = (resultDict["user"])["ProfileImage"].ToString();
						user.UserID = Convert.ToInt32((resultDict["user"])["UserID"].ToString());
						System.Diagnostics.Debug.WriteLine("successfully pic uploaded for , userID= {0}.", val);
						// set data in App settings
						await Utility.setUserDetails(user);
                    }
                }
            }
            return user;
        }
    }
}
