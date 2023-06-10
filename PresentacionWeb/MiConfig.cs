using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PresentacionWeb
{
    public static class MiConfig
    {
        public static string GetCxString
        {
            get {
                return ConfigurationManager.AppSettings["cadConn"];
            }
        }
    }
}