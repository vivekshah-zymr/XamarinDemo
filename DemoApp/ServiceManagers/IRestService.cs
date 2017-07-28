using System;
using DemoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.ServiceManagers
{
    public interface IRestService
    {
        Task<User> DoLoginWithCredentials(User user);
    }
}
