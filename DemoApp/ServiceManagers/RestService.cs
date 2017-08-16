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
using System.Linq;

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

        #region Login Related API 

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
                    user = JsonConvert.DeserializeObject<User>(resultDict["user"].ToString());
					IEnumerable<string> values;
					if (response.Headers.TryGetValues("authorization", out values))
					{
                        user.Authorization = values.First();
					}
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
            string authKey = user.Authorization;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization",authKey);

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
                        user = JsonConvert.DeserializeObject<User>(resultDict["user"].ToString());
                        user.Authorization = authKey;
						// set data in App settings
						await Utility.setUserDetails(user);
                    }
                }
            }
            return user;
        }

        #endregion

        #region News Related API

        public async Task<List<NewsModel>> GetNewsList(int pgNumber)
        {
            var uri = new Uri(Constants.BASE_URL + "nms/ws/news/all?PageSize=10&PageNumber=" + pgNumber);
            List<NewsModel> newsList = new List<NewsModel>();
            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJSON = response.Content.ReadAsStringAsync().Result;
                    var resultDict = JObject.Parse(responseJSON);
                    newsList = JsonConvert.DeserializeObject<List<NewsModel>>((resultDict["data"])["news"].ToString());
                    if(newsList == null){
                        newsList = new List<NewsModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR {0}", ex.Message);
            }
            return newsList;
        }

        #endregion
    }
}