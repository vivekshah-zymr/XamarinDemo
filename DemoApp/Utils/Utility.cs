﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

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

            // Return true if strIn is in valid e-mail format.
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
    }
}
