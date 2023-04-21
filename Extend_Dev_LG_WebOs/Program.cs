using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Extend_Dev_LG_WebOs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string token = ConfigurationManager.AppSettings["token"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    Log("Token Vazio no App.config");
                    return;
                }

                string checkURL = $"https://developer.lge.com/secure/CheckDevModeSession.dev?sessionToken={token}";
                GetResponseSite(checkURL);

                string resetDev = $"https://developer.lge.com/secure/ResetDevModeSession.dev?sessionToken={token}";
                GetResponseSite(resetDev);
            }
            catch (Exception e)
            {
                Log(e.Message);
                Log(e.StackTrace);
            }
        }

        static void GetResponseSite(string url)
        {

            WebRequest wrGETURL = WebRequest.Create(url);
            using (Stream objStream = wrGETURL.GetResponse().GetResponseStream())
            {
                using (StreamReader objReader = new StreamReader(objStream))
                {
                    string sLine = objReader.ReadLine();
                    if (sLine != null)
                        Log($"{DateTime.Now} -  {sLine}");
                }
            }
        }

        static void Log(string str)
        {
            Console.WriteLine(str);
            File.WriteAllText("log.txt", str);
        }
    }
}
