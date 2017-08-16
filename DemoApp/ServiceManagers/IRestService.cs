using System;
using DemoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.ServiceManagers
{
    public interface IRestService
    {
        Task<User> DoLoginWithCredentials(User user);
        Task<User> DoImageUpload(User user);
        Task<List<NewsModel>> GetNewsList(int pgNumber);
    }
}
