using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yak.Web.Utils
{
    public class UsernameExtractor
    {
        public static string ExtractUsername(string username)
        {
            var splittedUsername = username.Split('\\');
            return splittedUsername.Length > 1 ? splittedUsername[1] : splittedUsername[0];
        }
    }
}