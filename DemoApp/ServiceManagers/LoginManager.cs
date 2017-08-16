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


namespace DemoApp.ServiceManagers
{
    public class LoginManager
    {
        IRestService restService;

		public LoginManager(IRestService service)
		{
			restService = service;
		}
		public Task<User> makeLoginAPICall(User user)
		{
            return restService.DoLoginWithCredentials(user);
		}
        public Task<User> updateProfilePicAPICall(User user)
		{
			return restService.DoImageUpload(user);
		}
		public Task<List<NewsModel>> getNewsAPICall(int pageNumber)
		{
			return restService.GetNewsList(pageNumber);
		}
    }
}
