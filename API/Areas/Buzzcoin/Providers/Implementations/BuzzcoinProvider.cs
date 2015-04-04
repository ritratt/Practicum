using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        public String GetEarned(String apiKey)
        {
            /*
             * Get all of todays swipes
             * Extract eligible swipes
             * Calculate coins earned
            */

            var user = db.BuzzcoinUsers.FirstOrDefault(u => u.ApiKey == apiKey);
            var gtid = "310620"; //user.GtId;

            //FIX THIS AND MAKE IT WORK LIKE A NNORMAL PERSON!!
            var webrequest = WebRequest.Create("http://localhost:55944/api/DoorSwipes/GetDoorSwipeByCustId/" + gtid);
            webrequest.Headers.Add("apikey", apiKey);
            var respStream = webrequest.GetResponse().GetResponseStream();
            byte[] piggy = new byte[8192];
            int count = respStream.Read(piggy, 0, piggy.Length);
            var ffs = Encoding.ASCII.GetString(piggy, 0, count);
            return ffs;
        }

        public String AddCoins(String old, String add)
        {
            var oldInt = Int16.Parse(old);
            var newBalance = oldInt + Int16.Parse(add);
            return newBalance.ToString();
        }
    }
}