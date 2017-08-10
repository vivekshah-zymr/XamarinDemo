using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DemoApp.Models;
using Xamarin.Forms;

namespace DemoApp.Utils
{
    public class Utility
    {
        public Utility()
        {
        }

        public static bool isValidEmail(string emailID)
        {
            bool invalid = false;
            if (String.IsNullOrEmpty(emailID))
                return false;

            if (invalid)
                return false;

            // Return true if emailID is in valid e-mail format.
            try
            {
                return Regex.IsMatch(emailID,
                      "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static async Task setApplicationProperty<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }

        public static T getApplicationProperty<T>(string key)
        {
            return (T)Application.Current.Properties[key];
        }

        public static async Task clearAllApplicationProperty()
        {
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
        }

        public static async Task setUserDetails(User user)
        {
            await setApplicationProperty(Constants.USER_ID, user.UserID);
            await setApplicationProperty(Constants.USER_FNAME, user.FirstName);
            await setApplicationProperty(Constants.USER_EMAIL, user.Email);
            await setApplicationProperty(Constants.USER_PIC, user.ProfilePicURL);
        }

        public static User getUserDetails()
        {
            User user = new User();
            if (Application.Current.Properties.ContainsKey(Constants.USER_ID))
            {
                user.UserID = getApplicationProperty<int>(Constants.USER_ID);
                user.FirstName = getApplicationProperty<string>(Constants.USER_FNAME);
                user.Email = getApplicationProperty<string>(Constants.USER_EMAIL);
                user.ProfilePicURL = getApplicationProperty<string>(Constants.USER_PIC);
            }
            return user;
        }
    }
}
