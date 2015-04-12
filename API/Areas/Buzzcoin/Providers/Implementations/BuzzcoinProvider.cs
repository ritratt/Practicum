using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using API.Areas.Buzzcoin.Models;
using API.Models;

namespace API.Areas.Buzzcoin.Providers.Implementations
{
    public class BuzzcoinProvider : IBuzzcoinProvider
    {
        private readonly String _url;
        private readonly BuzzcoinContext db;

        public BuzzcoinProvider(String url)
        {
            db = new BuzzcoinContext();
            _url = url;
        }

        public String Url
        {
            get { return _url; }
            set { }
        }

        public String GetEarned(String gtid, String apiKey)
        {
            /*
             * Get all of todays swipes
             * Extract eligible swipes
             * Calculate coins earned
            */
            
            var json = RequestAPI(gtid, apiKey);

            return json;
        }

        public String AddCoins(String old, String add)
        {
            var oldInt = Int16.Parse(old);
            var newBalance = oldInt + Int16.Parse(add);
            return newBalance.ToString();
        }

        public string CustIdResolver(string gtid)
        {
            switch (gtid)
            {
                case "gtadmin":
                    return "310620";

                case "rsatpute3":
                    return "291192";

                case "gburdell":
                    return "3114433";
            }

            return "315741";
        }

        #region Util

        public String RequestAPI(String gtid, String apiKey)
        {
            var custId = CustIdResolver(gtid);
            var webrequest = WebRequest.Create("http://localhost:55944/api/DoorSwipes/GetDoorSwipeByCustId/" + custId);
            webrequest.Headers.Add("apikey", apiKey);
            webrequest.ContentType = "application/json; charset=utf-8";
            var respStream = webrequest.GetResponse().GetResponseStream();
            
            byte[] responseBytes = new byte[8192];
            int count = respStream.Read(responseBytes, 0, responseBytes.Length);
            var rawData = Encoding.ASCII.GetString(responseBytes, 0, count);
            var json = Json.Decode(rawData);

            var earned = CalculateEarned("284", "343", json);

            return earned.ToString();
        }

        private int CalculateEarned(String d1, String d2, dynamic json)
        {
            var currDoor = "";
            var lastTime = 0.0;
            var earned = 0;

            foreach (var swipe in json)
            {
                var d = swipe["DoorId"];
                if (d == d1 || d == d2 && d != currDoor)
                {
                    if (currDoor == "")
                    {
                        currDoor = d;
                        lastTime = float.Parse(swipe["ActualDateTime"]);
                        continue;
                    }

                    var currTime = float.Parse(swipe["ActualDateTime"]);
                    if (currTime - lastTime < 0.1)
                    {
                        earned++;   
                    }
                    lastTime = currTime;
                    currDoor = d;
                }
            }
            return earned;
        }

        #endregion
    }
}